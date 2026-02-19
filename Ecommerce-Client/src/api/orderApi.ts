import type { CreateOrder, Order, OrderInfo } from "../types/Order";
import { methods } from "./apiClient";

export const orderApi = {
  createOrder: (payload: CreateOrder) =>
    methods.post<CreateOrder, string>("Order", payload),
  getOrders: () => methods.get<Order[]>("/Order"),
  getOneOrder: (orderId: string | null) =>
    methods.get<Order>(`/Order/${orderId}`),
  getAllOrders: (
    pageIndex: number,
    pageSize: number,
    searchTerm: string | null,
  ) =>
    methods.get<OrderInfo>("/Order/getAllOrder", {
      params: { pageIndex, pageSize, searchTerm },
    }),
  shipOrder: (orderId: string) =>
    methods.put<string, void>(`/Order/ShipOrder/${orderId}`),
  deliverOrder: (orderId: string) =>
    methods.put<string, void>(`/Order/DeliverOrder/${orderId}`),
};
