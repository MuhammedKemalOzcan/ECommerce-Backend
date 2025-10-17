import axios, { AxiosError } from "axios";
import { toast } from "react-toastify";

axios.defaults.baseURL = "https://localhost:7196/api/";

axios.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");

  if (token) config.headers.Authorization = `Bearer ${token}`;
  return config;
});

//Daha sonra dönüş yap.
axios.interceptors.response.use(
  (response) => response,
  (error: AxiosError<any>) => {
    if (!error.response) {
      console.error("Network/Unknown error:", error.message);
      return Promise.reject(error);
    }
    const { data, status } = error.response;
    const message = typeof data === "string" ? data : (data?.message as string);
    console.log("Error response data", data);
    console.log(message);
    switch (status) {
      case 400:
        console.warn("Bad Request", data);
        toast.error(message);
        break;
      case 404:
        console.warn("NotFound", data);
        toast.error(message);
        break;
      case 500:
        console.warn("Server Error", data);
        toast.error(message);
        break;
      case 401:
        window.location.href = "/login";
        break;
    }
    return Promise.reject(error);
  }
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
