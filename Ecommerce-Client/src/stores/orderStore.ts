import { create } from "zustand";
import type { CreateOrder, Order } from "../types/Order";
import { orderApi } from "../api/orderApi";
import { toast } from "react-toastify";

interface OrderProps {
  orders: Order[] | null;
  isLoading: boolean;
  error: Error | null;
  createOrder: (payload: CreateOrder) => Promise<string | undefined>;
  getOrders: () => Promise<void>;
}

type Error = {
  message: string;
  code?: string;
  field?: string;
};

export const useOrderStore = create<OrderProps>((set) => ({
  orders: null,
  error: null,
  isLoading: false,
  createOrder: async (payload: CreateOrder) => {
    set({ isLoading: true });
    if (!payload) return;

    try {
      const response = await orderApi.createOrder(payload);
      toast.success(`Order ${response} created successfully `);
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
      console.log(response);
    } catch (error: any) {
      set({ error: error, isLoading: false });
      console.log(error);
    } finally {
      set({ isLoading: false });
    }
  },
}));
