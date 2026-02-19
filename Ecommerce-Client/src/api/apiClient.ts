import axios, { type AxiosRequestConfig } from "axios";
import { setupInterceptors } from "./interceptors";


const apiClient = axios.create({
  baseURL: "https://localhost:7196/api/",
  withCredentials: true,
  // timeout: 15000,
});

setupInterceptors(apiClient);

export const methods = {
  get: async <T>(url: string, config?: AxiosRequestConfig) =>
    apiClient.get<T>(url, config).then((response) => response.data),

  post: async <TRequest, TResponse>(url: string, body: TRequest) =>
    apiClient.post<TResponse>(url, body).then((response) => response.data),

  put: async <TRequest, TResponse>(url: string, body?: TRequest) =>
    apiClient.put<TResponse>(url, body).then((response) => response.data),

  delete: async <T>(url: string) =>
    apiClient.delete<T>(url).then((response) => response.data),
};
