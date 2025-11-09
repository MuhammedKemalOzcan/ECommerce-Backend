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
  getById: (id: string | null) => Promise<Products | undefined>;
  deleteProduct: (id: string) => Promise<void>;
  createProduct: (newProduct: AddProduct) => Promise<Products>;
  patchProduct: (
    id: string | null,
    data: AddProduct
  ) => Promise<Products | undefined>;
  refreshById: (id: string | null) => Promise<void>;
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
  getById: async (id: string | null) => {
    set({ loading: true, currentProduct: null });
    if (!id) return;
    try {
      const { product } = await productsApi.detail(id);
      set({ currentProduct: product, loading: false });
      console.log(product);
      return product;
    } catch (error) {
      set({
        loading: false,
        error: error instanceof Error ? error.message : "Ürün yüklenemedi",
      });
    }
  },
  createProduct: async (newProduct: AddProduct) => {
    set({ loading: true });
    try {
      const created = await productsApi.add(newProduct);
      console.log(created);
      set((state) => ({ products: [...state.products, created] }));
      return created;
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
  patchProduct: async (id: string | null, data: AddProduct) => {
    if (!id) {
      console.error("❌ Product ID is required for update");
      throw new Error("Product ID is required");
    }
    set({ loading: true });
    try {
      const updated = await productsApi.update(id, data);
      set((state) => ({
        products: state.products.map((p) =>
          p.id === id
            ? {
                ...p, // Eski değerleri koru
                ...updated, // Backend'den gelenleri üzerine yaz
              }
            : p
        ),
        currentProduct: {
          ...state.currentProduct,
          ...updated,
        },
      }));
      console.log(updated);
      return updated;
    } catch (err) {
      console.error("Update error:", err);
      throw err;
    } finally {
      set({ loading: false });
    }
  },
  refreshById: async (id: string | null) => {
    if (!id) return;

    try {
      const { product: refresh } = await productsApi.detail(id);

      set((state) => {
        const exist = state.products.some((p) => p.id === id);
        console.log(exist);

        //Ürün var mı? varsa id'si uyuşuyor mu? eğer uyuşuyorsa refreshi yerine koy.
        const products = exist
          ? state.products.map((p) => (p.id === id ? refresh : p))
          : state.products;

        const currentProduct =
          state.currentProduct?.id === id ? refresh : state.currentProduct;

        return { products, currentProduct };
      });
    } catch (error) {
      console.error("refreshById error:", error);
    }
  },
}));
