import axios, { AxiosError, type InternalAxiosRequestConfig } from "axios";
import { toast } from "react-toastify";
import { useAuthStore } from "../auth/authStore";

axios.defaults.baseURL = "https://localhost:7196/api/";
axios.defaults.withCredentials = true;
// axios.defaults.timeout = 15000;

axios.interceptors.request.use((config) => {
  const token = useAuthStore.getState().token;

  if (token) config.headers.Authorization = `Bearer ${token}`;
  return config;
});

interface ApiErrorResponse {
  message?: string;
  errors?: Record<string, string[]>;
  title?: string;
  status?: number;
}

interface FailedRequest {
  resolve: (token: string | null) => void;
  reject: (error: any) => void;
}

const NO_RETRY_STATUS_CODES = [400, 401, 403, 403, 422];

//gelen diğer 401 hatalarını beklemeye alıyoruz
let isRefreshing = false;
let failedQueue: FailedRequest[] = [];

// Kuyruktaki bekleyen her şeyi işleme alan yardımcı fonksiyon
const processQueue = (error: any, token: string | null) => {
  failedQueue.forEach((prom) => {
    if (error)
      prom.reject(error); //Bekleyen isteğe hata aldık de
    else prom.resolve(token); //Bekleyen isteğe yeni token geldi devam et de
  });
  failedQueue = [];
};

// Daha sonra dönüş yap.
axios.interceptors.response.use(
  (response) => response,
  (error: AxiosError<ApiErrorResponse>) => {
    const originalRequest = error.config as InternalAxiosRequestConfig & {
      _retry?: boolean;
    };

    if (!error.response) {
      console.error("Network error:", error.message);
      toast.error("İnternet bağlantınızı kontrol edin.", {
        toastId: "network-error",
      });
      return Promise.reject({
        message: "Bağlantı hatası",
        code: "NETWORK_ERROR",
        originalError: error,
      });
    }
    const { data, status } = error.response;
    let errorMessage = "bir hata oluştu";
    if (typeof data === "string") {
      errorMessage = data;
    } else if (data?.message) {
      errorMessage = data.message;
    } else if (data?.title) {
      errorMessage = data.title;
    }

    switch (status) {
      case 400:
        console.warn("Bad Request", data);
        if (data?.errors && typeof data.errors === "object") {
          Object.entries(data.errors).forEach(([field, messages]) => {
            if (Array.isArray(messages)) {
              messages.forEach((msg) => toast.error(`${field}: ${msg}`));
            }
          });
        } else {
          toast.error(errorMessage);
        }
        break;
      case 403:
        console.warn("Forbidden:", data);
        toast.error("Bu işlem için yetkiniz bulunmuyor.");
        break;
      case 404:
        console.warn("Not Found", data);
        toast.error(errorMessage || "İstenen kaynak bulunamadı");
        break;
      case 422:
        console.warn("Validation Error:", data);

        if (data?.errors && typeof data.errors == "object") {
          Object.entries(data.errors).forEach(([field, messages]) => {
            if (Array.isArray(messages)) {
              messages.forEach((msg) => toast.error(`${field} : ${msg}`));
            }
          });
        }
        break;
      case 429:
        console.warn("Rate Limit Exceed");
        toast.error("Çok fazla istek gönderdinizü lütfen bir süre bekleyiniz.");
        break;

      case 500:
      case 502:
      case 503:
        console.warn("Server Error", status, data);
        toast.error("Sunucu hatası oluştu lütfen daha sonra tekrar deneyin.");
        if (
          !originalRequest._retry &&
          !NO_RETRY_STATUS_CODES.includes(status)
        ) {
          originalRequest._retry = true;

          return axios(originalRequest);
        }
        break;
      case 401:
        // 1. Eğer bu istek zaten bir kez denenmişse (retry), sonsuz döngüye girmemek için reddet
        if (originalRequest._retry) {
          console.log("debug 1");
          useAuthStore.getState().clearAuth(); // Refresh de işe yaramadıysa oturumu kapat
          console.log("debug 2");

          return Promise.reject(error);
        }
        console.log("debug 3");

        // 2. Eğer şu an halihazırda bir refresh işlemi yapılıyorsa
        if (isRefreshing) {
          console.log("debug 4");

          return new Promise((resolve, reject) => {
            console.log("debug 5");

            failedQueue.push({ resolve, reject });
          })
            .then((token) => {
              originalRequest.headers["Authorization"] = "Bearer " + token;
              return axios(originalRequest);
            })
            .catch((err) => {
              return Promise.reject(err);
            });
        }

        // 3. İLK DEFA 401 GELDİ: Refresh sürecini başlat
        originalRequest._retry = true;
        isRefreshing = true;

        return new Promise((resolve, reject) => {
          const currentRefreshToken = useAuthStore.getState().refreshToken;
          console.log("currentRefreshToken", currentRefreshToken);

          useAuthStore
            .getState()
            .refreshAccessToken(currentRefreshToken)
            .then((newToken: string | undefined) => {
              if (!newToken) return;
              const authHeader = `Bearer ${newToken}`;

              axios.defaults.headers.common["Authorization"] = authHeader;
              originalRequest.headers["Authorization"] = authHeader;

              processQueue(null, newToken);
              resolve(axios(originalRequest));
            })
            .catch((err: any) => {
              processQueue(err, null);
              useAuthStore.getState().clearAuth();
              reject(err);
            })
            .finally(() => {
              isRefreshing = false;
            });
        });
      default:
        console.error("❓ Unexpected Error:", status, data);
        toast.error(errorMessage);
    }
    return Promise.reject(error);
  },
);

export const methods = {
  get: async <T>(url: string) =>
    axios.get<T>(url).then((response) => response.data),

  post: async <TRequest, TResponse>(url: string, body: TRequest) =>
    axios.post<TResponse>(url, body).then((response) => response.data),

  put: async <TRequest, TResponse>(url: string, body: TRequest) =>
    axios.put<TResponse>(url, body).then((response) => response.data),

  delete: async <T>(url: string) =>
    axios.delete<T>(url).then((response) => response.data),
};
