import { create } from "zustand";
import type { CreateOrder, Order, OrderInfo } from "../types/Order";
import { orderApi } from "../api/orderApi";

interface OrderProps {
  orders: Order[] | null;
  order: Order | null;
  isLoading: boolean;
  error: Error | null;
  orderInfo: OrderInfo | null;
  createOrder: (payload: CreateOrder) => Promise<string | undefined>;
  getOrders: () => Promise<void>;
  getOneOrder: (orderId: string | null) => Promise<void>;
  getAllOrders: (
    pageIndex: number,
    pageSize: number,
    searchTerm: string,
  ) => Promise<void>;
  shipOrder: (orderId: string) => Promise<void>;
  deliverOrder: (orderId: string) => Promise<void>;
}

type Error = {
  message: string;
  code?: string;
  field?: string;
};

export const useOrderStore = create<OrderProps>((set) => ({
  orders: null,
  orderInfo: null,
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
  getAllOrders: async (
    pageIndex: number,
    pageSize: number,
    searchTerm: string | null,
  ) => {
    set({ isLoading: true });
    try {
      const response = await orderApi.getAllOrders(
        pageIndex,
        pageSize,
        searchTerm,
      );
      set({ orderInfo: response });
    } catch (error) {
      console.log(error);
    } finally {
      set({ isLoading: false });
    }
  },
  shipOrder: async (orderId: string) => {
    set({ isLoading: true });
    try {
      await orderApi.shipOrder(orderId);
      set((state) => ({
        order: state.order?.id === orderId ? { ...state.order, deliveryStatus: "2", updatedDate: new Date().toISOString() } : state.order
      }))
    } catch (error) {
      console.log(error);
    } finally {
      set({ isLoading: false });
    }
  },
  deliverOrder: async (orderId: string) => {
    set({ isLoading: true });
    try {
      await orderApi.deliverOrder(orderId);
      set((state) => ({
        order: state.order?.id === orderId ? { ...state.order, deliveryStatus: "3", updatedDate: new Date().toISOString() } : state.order
      }))
    } catch (error) {
      console.log(error);
    } finally {
      set({ isLoading: false });
    }
  },
}));
