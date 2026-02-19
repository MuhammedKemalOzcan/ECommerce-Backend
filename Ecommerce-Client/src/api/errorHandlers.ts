import { toast } from "react-toastify";

interface ApiErrorResponse {
    message?: string;
    errors?: Record<string, string[]>;
    title?: string;
    status?: number;
}

const showToast = (message: string) => toast.error(message);

const strategies: Record<number, (error: ApiErrorResponse) => void> = {
    400: (error: ApiErrorResponse) => {
        if (error?.errors && typeof error.errors === "object") {
            Object.entries(error.errors).forEach(([field, messages]) => {
                if (Array.isArray(messages)) {
                    messages.forEach((msg) => toast.error(`${field}: ${msg}`));
                }
            });
        } else {
            showToast(error?.message || "Hatalı istek");
        }
    },
    401: () => { /* 401 interceptor içinde özel olarak yönetiliyor, burası boş kalabilir veya loglanabilir */ },
    403: () => showToast("Bu işlem için yetkiniz bulunmuyor."),
    404: (data) => showToast(data?.message || "İstenen kaynak bulunamadı."),
    422: (data) => {
        if (data?.errors) {
            Object.entries(data.errors).forEach(([_, messages]) => {
                messages.forEach((msg) => showToast(msg));
            });
        }
    },
    429: () => showToast("Çok fazla istek gönderdiniz, lütfen bekleyiniz."),
    500: () => showToast("Sunucu hatası oluştu."),
}

export const handleApiError = (status: number, data: any) => {
    const handler = strategies[status];
    if (handler) {
        handler(data);
    } else {
        showToast(data?.message || "Beklenmedik bir hata oluştu.");
    }
}