import type { CartItems } from "../types/cart";
import { methods } from "./apiClient";

export const cartApi = {
  add: (cartItem: CartItems) =>
    methods.post<CartItems, CartItems>("/Cart/items", cartItem),
};
