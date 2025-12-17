import { create } from "zustand";
import type { AddItem, Cart, CartItem, UpdateCartItem } from "../types/cart";
import { cartApi } from "../api/cart";
import { toast } from "react-toastify";

type cartProps = {
  cart: Cart | null;
  cartItems: CartItem[];
  addItemToCart: (cartItem: AddItem) => Promise<void>;
  listCart: () => Promise<void>;
  clearCart: () => Promise<void>;
  deleteCartItem: (itemId: string | null) => Promise<void>;
  updateCartItem: (cartItem: UpdateCartItem) => Promise<void>;
  error: CartError | null;
  isLoading: boolean;
  mergeCart: () => Promise<void>;
};

type CartError = {
  message: string;
  code?: string;
  field?: string;
};

const handleError = (error: any, context: string): CartError => {
  console.error(`❌ Cart Error [${context}]:`, error);

  let errorMessage = "Bir hata oluştu";
  let errorCode = "UNKNOWN_ERROR";

  // Axios error structure
  if (error?.message) {
    errorMessage = error.response.message;
  }

  if (error?.code) {
    errorCode = error.code;
  }

  // Backend'den gelen spesifik hata mesajları
  if (error?.data?.message) {
    errorMessage = error.response.data.message;
  }

  return {
    message: errorMessage,
    code: errorCode,
  };
};

export const useCartStore = create<cartProps>((set) => ({
  cart: null,
  cartItems: [],
  error: null,
  isLoading: false,
  listCart: async () => {
    try {
      set({ isLoading: true, error: null });
      const response = await cartApi.list();
      if (!response?.data) {
        throw new Error("Sepet verisi alınamadı.");
      }
      set({
        cart: response.data,
        cartItems: response.data.cartItems || [],
        isLoading: false,
      });
    } catch (error) {
      const cartError = handleError(error, "ListCart");
      set({
        error: cartError,
        isLoading: false,
        cart: null,
        cartItems: [],
      });
    }
  },
  addItemToCart: async (cartItem: AddItem) => {
    set({ isLoading: true });
    try {
      const res = await cartApi.add(cartItem);
      console.log(res);

      set({ cart: res.data });
      toast.success(res.message);
    } catch (error) {
      const cartError = handleError(error, "Add Item Error");
      set({
        error: cartError,
        isLoading: false,
      });
    } finally {
      set({ isLoading: false });
    }
  },
  updateCartItem: async (cartItem: UpdateCartItem) => {
    set({ isLoading: true });
    try {
      const res = await cartApi.update(cartItem);
      set({ cart: res.data, isLoading: false });
      console.log(res.data);
    } catch (error: any) {
      console.log(error.response?.data?.message);
      const cartError = handleError(error, "Update Item Error");
      set({
        error: error.response?.data,
        isLoading: false,
      });
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
  mergeCart: async () => {
    try {
      const response = await cartApi.merge();
      console.log(response.cartDto);

      set({ cart: response.cartDto });
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
