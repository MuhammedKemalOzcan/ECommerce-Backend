import type { CreateOrder, Order } from "../types/Order";
import { methods } from "./apiClient";

export const orderApi = {
  createOrder: (payload: CreateOrder) =>
    methods.post<CreateOrder, string>("Order", payload),
  getOrders: () => methods.get<Order[]>("/Order"),
};
