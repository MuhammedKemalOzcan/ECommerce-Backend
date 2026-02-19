import { create } from "zustand";
import * as signalR from "@microsoft/signalr";
import { toast } from "react-toastify";
import { useAuthStore } from "../auth/authStore";

interface NotificationStore {
  connection: signalR.HubConnection | null;
  messages: string[];
  startConnection: () => void;
  sendMessage: (user: string, message: string) => Promise<void>;
}

export const useNotificationStore = create<NotificationStore>((set, get) => ({
  connection: null,
  messages: [],

  startConnection: () => {
    // Eğer zaten bağlıysak veya bağlanıyorsak ikinci kez başlatma
    if (get().connection?.state === signalR.HubConnectionState.Connected)
      return;
    // 1. Bağlantıyı yapılandır.
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:7196/orders-hub", {
        accessTokenFactory: () => {
          const token = useAuthStore.getState().token;
          return token || "";
        },
      })
      .withAutomaticReconnect() //Bağlantı koparsa otomatik bağlan
      .build();

    newConnection
      .start()
      .then(() => {
        newConnection.on("ReceiveMessage", (message: string) => {
          toast.info(`🔔 ${message}`, {
            position: "bottom-right",
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            theme: "colored",
          });
          set((state) => ({ messages: [...state.messages, message] }));
        });

        set({ connection: newConnection });
      })
      .catch((err) => console.error("Bağlantı Hatası: ", err));
  },
  sendMessage: async (message) => {
    const conn = get().connection;
    if (conn) {
      await conn.invoke("SendMessage", message);
    }
  },
}));
