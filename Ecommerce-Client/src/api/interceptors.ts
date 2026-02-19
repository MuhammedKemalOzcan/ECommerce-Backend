import type { AxiosError, AxiosInstance, InternalAxiosRequestConfig } from "axios";
import { useAuthStore } from "../auth/authStore";
import { toast } from "react-toastify";
import { addToQueue, getIsRefreshing, processQueue, setIsRefreshing } from "./tokenManager";
import { handleApiError } from "./errorHandlers";


export const setupInterceptors = (axiosInstance: AxiosInstance) => {
    axiosInstance.interceptors.request.use((config) => {
        const token = useAuthStore.getState().token;

        if (token) config.headers.Authorization = `Bearer ${token}`;
        return config;
    });

    axiosInstance.interceptors.response.use(
        (response) => response,
        async (error: AxiosError<any>) => {
            const originalRequest = error.config as InternalAxiosRequestConfig & { _retry?: boolean };

            // Network Error Kontrolü
            if (!error.response) {
                toast.error("İnternet bağlantınızı kontrol edin.");
                return Promise.reject(error);
            }

            const { status, data } = error.response;

            // 401 ve Token Refresh Mantığı
            if (status === 401 && !originalRequest._retry) {
                if (getIsRefreshing()) {
                    return new Promise((resolve, reject) => {
                        addToQueue(
                            (token) => {
                                originalRequest.headers.Authorization = `Bearer ${token}`;
                                resolve(axiosInstance(originalRequest));
                            },
                            reject
                        );
                    });
                }

                originalRequest._retry = true;
                setIsRefreshing(true);

                try {
                    const refreshToken = useAuthStore.getState().refreshToken;
                    // Not: refreshAccessToken'ın yeni token döndürdüğünü varsayıyoruz
                    const newToken = await useAuthStore.getState().refreshAccessToken(refreshToken);

                    if (newToken) {
                        processQueue(null, newToken);
                        originalRequest.headers.Authorization = `Bearer ${newToken}`;
                        return axiosInstance(originalRequest);
                    } else {
                        throw new Error("Token yenilenemedi");
                    }
                } catch (refreshError) {
                    processQueue(refreshError, null);
                    useAuthStore.getState().clearAuth();
                    return Promise.reject(refreshError);
                } finally {
                    setIsRefreshing(false);
                }
            }

            // Diğer Hataları Stratejiye Devret
            handleApiError(status, data);

            return Promise.reject(error);
        }
    );
};