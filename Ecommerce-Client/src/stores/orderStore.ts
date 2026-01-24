import { create } from "zustand";
import type { CreateOrder, Order } from "../types/Order";
import { orderApi } from "../api/orderApi";
import { toast } from "react-toastify";

interface OrderProps {
  orders: Order[] | null;
  order: Order | null;
  isLoading: boolean;
  error: Error | null;
  createOrder: (payload: CreateOrder) => Promise<string | undefined>;
  getOrders: () => Promise<void>;
  getOneOrder: (orderId: string | null) => Promise<void>;
}

type Error = {
  message: string;
  code?: string;
  field?: string;
};

export const useOrderStore = create<OrderProps>((set) => ({
  orders: null,
  order: null,
  error: null,
  isLoading: false,
  createOrder: async (payload: CreateOrder) => {
    set({ isLoading: true });
    if (!payload) return;
    try {
      const response = await orderApi.createOrder(payload);
      console.log(response);
      return response;
    } catch (error: any) {
      set({ error: error, isLoading: false });
      console.log(error);
    } finally {
      set({ isLoading: false });
    }
  },
  getOrders: async () => {
    set({ isLoading: true });
    try {
      const response = await orderApi.getOrders();
      set({ orders: response });
    } catch (error: any) {
      set({ error: error, isLoading: false });
      console.log(error);
    } finally {
      set({ isLoading: false });
    }
  },
  getOneOrder: async (orderId: string | null) => {
    set({ isLoading: true, order: null });
    if (orderId === null) return;
    try {
      const response = await orderApi.getOneOrder(orderId);
      console.log(response);
      set({ order: response });
    } catch (error) {
      console.log(error);
    } finally {
      set({ isLoading: false });
    }
  },
}));
