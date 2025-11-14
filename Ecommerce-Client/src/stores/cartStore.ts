import { create } from "zustand";
import type { Cart, CartItems } from "../types/cart";

type cartProps = {
  cart: Cart[];
  cartItem: CartItems | null;
};

export const useCartStore = create<cartProps>((set) => ({
  cart: [],
  cartItem: null,
}));
