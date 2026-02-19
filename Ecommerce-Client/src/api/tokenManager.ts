
interface FailedRequest {
    resolve: (token: string | null) => void;
    reject: (error: any) => void;
}

//gelen diğer 401 hatalarını beklemeye alıyoruz
let isRefreshing = false;
let failedQueue: FailedRequest[] = [];

// Kuyruktaki bekleyen her şeyi işleme alan yardımcı fonksiyon
export const processQueue = (error: any, token: string | null) => {
    failedQueue.forEach((prom) => {
        if (error)
            prom.reject(error); //Bekleyen isteğe hata aldık de
        else prom.resolve(token); //Bekleyen isteğe yeni token geldi devam et de
    });
    failedQueue = [];
};

export const getIsRefreshing = () => isRefreshing;
export const setIsRefreshing = (status: boolean) => { isRefreshing = status; };

export const addToQueue = (callback: (token: string | null) => void, reject: (error: any) => void) => {
    failedQueue.push({ resolve: callback, reject });
};