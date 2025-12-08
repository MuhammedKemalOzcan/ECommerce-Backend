import type { AddItem, Cart, CartItem, UpdateCartItem } from "../types/cart";
import type { AddCustomer, Customer } from "../types/customer";
import { methods } from "./apiClient";

export const cartApi = {
  add: (cartItem: AddItem) =>
    methods.post<AddItem, { data: Cart; message?: string }>(
      "/Cart/items",
      cartItem
    ),
  update: (CartItem: UpdateCartItem) =>
    methods.put<UpdateCartItem, { data: Cart }>(
      `Cart/items/${CartItem.cartItemId}`,
      CartItem
    ),
  list: () => methods.get<{ data: Cart }>("/Cart"),
  clear: () => methods.delete("/Cart"),
  delete: (itemId: string) => methods.delete<CartItem>(`Cart/items/${itemId}`),
};

export const customerApi = {
  add: (customer: AddCustomer) =>
    methods.post<AddCustomer, Customer>("/Customer", customer),
};
