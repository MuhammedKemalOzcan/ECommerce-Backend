import type { AddItem, Cart, CartItem } from "../types/cart";
import { methods } from "./apiClient";

export const cartApi = {
  add: (cartItem: AddItem) =>
    methods.post<AddItem, { data: Cart; message?: string }>(
      "/Cart/items",
      cartItem
    ),
  list: () => methods.get<{ data: Cart }>("/Cart"),
  clear: () => methods.delete("/Cart"),
  delete: (itemId: string) => methods.delete<CartItem>(`Cart/items/${itemId}`),
};
