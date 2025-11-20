import { create } from "zustand";
import type { AddItem, Cart, CartItem } from "../types/cart";
import { cartApi } from "../api/cart";
import { toast } from "react-toastify";


type cartProps = {
  cart: Cart | null;
  cartItems: CartItem[];
  addItemToCart: (cartItem: AddItem) => Promise<void>;
  listCart: () => Promise<void>;
  clearCart: () => Promise<void>;
  deleteCartItem: (itemId: string | null) => Promise<void>;
};

export const useCartStore = create<cartProps>((set) => ({
  cart: null,
  cartItems: [],
  listCart: async () => {
    try {
      const response = await cartApi.list();
      set({ cart: response.data });
      set((s) => ({ cartItems: s.cart?.cartItems }));
    } catch (error) {
      console.log(error);
    }
  },
  addItemToCart: async (cartItem: AddItem) => {
    try {
      const res = await cartApi.add(cartItem);
      console.log(res);
      set({ cart: res.data });
    } catch (error) {
      console.log(error);
    }
  },
  clearCart: async () => {
    try {
      const response = await cartApi.clear();
      console.log(response);

      set({ cart: null });
      toast.success("Sepetteki bütün ürünler kaldırıldı");
    } catch (error) {
      console.log(error);
    }
  },
  deleteCartItem: async (itemId: string | null) => {
    if (!itemId) return;

    try {
      await cartApi.delete(itemId);

      set((state) => {
        // cart yoksa state'i hiç değiştirme
        if (!state.cart) return state;

        const cart = state.cart; // Artık Cart, null değil
        console.log("Cart:", cart);

        const cartItems = cart.cartItems.filter((item) => item.id !== itemId);
        console.log("cartItems:", cartItems);

        const updatedCart: Cart = {
          ...cart,
          cartItems,
          totalItemCount: cartItems.length, // istersen backend'den al
          totalAmount: cartItems.reduce(
            (sum, item) => sum + item.totalPrice,
            0
          ),
        };

        console.log("updatedCart:", updatedCart);

        return {
          ...state,
          cart: updatedCart,
        };
      });
    } catch (error) {
      console.log(error);
    }
  },
}));
