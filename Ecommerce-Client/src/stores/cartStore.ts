import { create } from "zustand";
import type { AddItem, Cart, CartItems } from "../types/cart";
import { cartApi } from "../api/cart";

type cartProps = {
  cart: Cart[];
  cartItem: CartItems | null;
  addItemToCart: (cartItem: AddItem) => Promise<void>;
};

export const useCartStore = create<cartProps>((set) => ({
  cart: [],
  cartItem: null,
  addItemToCart: async (cartItem: AddItem) => {
    try {
      const res = await cartApi.add(cartItem);
      console.log(res);
      set((s) => ({ cart: [...s.cart, res] }));
    } catch (error) {
      console.log(error);
    }
  },
}));
