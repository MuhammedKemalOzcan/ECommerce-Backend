import type { AddItem, Cart } from "../types/cart";
import { methods } from "./apiClient";

export const cartApi = {
  add: (cartItem: AddItem) =>
    methods.post<AddItem, Cart>("/Cart/items", cartItem),
};
