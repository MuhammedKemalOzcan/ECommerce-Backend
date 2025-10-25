import { create } from "zustand";
import type { AddProduct, Products } from "../types/Products";
import { productsApi } from "../api/products";
import { toast } from "react-toastify";

type productsState = {
  products: Products[];
  currentProduct: Products | null;
  loading: boolean;
  error: string | null;
  getAll: () => Promise<void>;
  getById: (id: string) => Promise<void>;
  createProduct: (newProduct: AddProduct) => Promise<void>;
  deleteProduct: (id: string) => Promise<void>;
};

export const useProductStore = create<productsState>((set, get) => ({
  products: [],
  currentProduct: null,
  loading: false,
  error: null,
  getAll: async () => {
    if (get().products.length > 0) return;
    set({ loading: true });
    try {
      const response = await productsApi.list();
      set({ products: response.products });
    } catch (e: any) {
      console.log(e);
    } finally {
      set({ loading: false });
    }
  },
  getById: async (id: string) => {
    set({ loading: true });
    try {
      const response = await productsApi.detail(id);
      set({ currentProduct: response.product });
    } catch (error) {
      console.log(error);
    } finally {
      set({ loading: false });
    }
  },
  createProduct: async (newProduct: AddProduct) => {
    set({ loading: true });
    try {
      const response = await productsApi.add(newProduct);
      set((state) => ({ products: [...state.products, response] }));
    } catch (error) {
      set({ error: "Ürün eklenemedi." });
      throw error;
    } finally {
      set({ loading: false });
    }
  },
  deleteProduct: async (id: string) => {
    set({ loading: true });
    const prev = get().products;
    set((s) => ({ products: s.products.filter((p) => p.id !== id) }));
    try {
      await productsApi.remove(id);
      if (get().currentProduct?.id === id) set({ currentProduct: null });
      toast.success("Ürün başarıyla silindi");
    } catch (error) {
      set({ products: prev });
      console.log(error);
    } finally {
      set({ loading: false });
    }
  },
}));
