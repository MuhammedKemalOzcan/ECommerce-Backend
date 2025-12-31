import { create } from "zustand";
import type { Products } from "../types/Products";
import { productsApi } from "../api/products";
import type { ProductBoxes } from "../types/ProductBox";

type productsState = {
  products: Products[];
  currentProduct: Products | null;
  loading: boolean;
  error: string | null;
  getAll: () => Promise<void>;
  getById: (id: string | null) => Promise<void>;
  addBoxToProduct: (
    productId: string | null,
    boxData: ProductBoxes
  ) => Promise<void>;
  removeBoxFromProduct: (boxId: string | null, productId: string | null) => Promise<void>
  // createProduct: (newProduct: AddProduct) => Promise<Products>;
  // patchProduct: (
  //   id: string | null,
  //   data: AddProduct
  // ) => Promise<Products | undefined>;
  // refreshById: (id: string | null) => Promise<void>;
};

export const useProductStore = create<productsState>((set) => ({
  products: [],
  currentProduct: null,
  loading: false,
  error: null,
  getAll: async () => {
    set({ loading: true });
    try {
      const response = await productsApi.list();
      set({ products: response });
    } catch (error) {
      console.log(error);
    } finally {
      set({ loading: false });
    }
  },
  getById: async (id: string | null) => {
    set({ loading: true, currentProduct: null });
    if (!id) return;
    try {
      const response = await productsApi.detail(id);
      set({ currentProduct: response, loading: false });
      console.log(response);
    } catch (error) {
      set({
        loading: false,
        error: error instanceof Error ? error.message : "Ürün yüklenemedi",
      });
    }
  },
  addBoxToProduct: async (productId: string | null, boxData: ProductBoxes) => {
    set({ loading: true, error: null });
    if (!productId) return;
    try {
      // Backend artık direkt GUID (string) dönecek
      const response = await productsApi.addBox(productId, boxData);

      if (!response) return;

      const newBoxItem = {
        ...boxData,
        id: response,
      };

      set((state) => {
        const updatedProducts = state.products.map((product) => {
          if (product.id === productId) {
            const existingBoxes = product.productBoxes || [];
            return {
              ...product,
              productBoxes: [...existingBoxes, newBoxItem],
            };
          }
          return product;
        });

        return {
          products: updatedProducts,
        };
      });

      console.log("State güncellendi:", newBoxItem);
    } catch (error) {
      console.log(error);
    } finally {
      set({ loading: false });
    }
  },
  removeBoxFromProduct: async (
    boxId: string | null,
    productId: string | null
  ) => {
    set({ loading: true });
    if (!boxId) return;
    try {
      await productsApi.removeBox(boxId, productId);
      set((state) => {
        const updatedProducts = state.products.map((product) => {
          if (!product.productBoxes) return product;

          const filteredBoxes = product.productBoxes.filter(
            (box) => box.id !== boxId
          );

          return {
            ...product,
            productBoxes: filteredBoxes,
          };
        });
        return {
          products: updatedProducts,
        };
      });
    } catch (error) {
      console.log(error);
    } finally {
      set({ loading: false });
    }
  },
  // createProduct: async (newProduct: AddProduct) => {
  //   set({ loading: true });
  //   try {
  //     const created = await productsApi.add(newProduct);
  //     console.log(created);
  //     set((state) => ({ products: [...state.products, created] }));
  //     return created;
  //   } catch (error) {
  //     set({ error: "Ürün eklenemedi." });
  //     throw error;
  //   } finally {
  //     set({ loading: false });
  //   }
  // },
  // deleteProduct: async (id: string) => {
  //   set({ loading: true });
  //   const prev = get().products;
  //   set((s) => ({ products: s.products.filter((p) => p.id !== id) }));
  //   try {
  //     await productsApi.remove(id);
  //     if (get().currentProduct?.id === id) set({ currentProduct: null });
  //     toast.success("Ürün başarıyla silindi");
  //   } catch (error) {
  //     set({ products: prev });
  //     console.log(error);
  //   } finally {
  //     set({ loading: false });
  //   }
  // },
  // patchProduct: async (id: string | null, data: AddProduct) => {
  //   if (!id) {
  //     console.error("❌ Product ID is required for update");
  //     throw new Error("Product ID is required");
  //   }
  //   set({ loading: true });
  //   try {
  //     const updated = await productsApi.update(id, data);
  //     set((state) => ({
  //       products: state.products.map((p) =>
  //         p.id === id
  //           ? {
  //               ...p, // Eski değerleri koru
  //               ...updated, // Backend'den gelenleri üzerine yaz
  //             }
  //           : p
  //       ),
  //       currentProduct: {
  //         ...state.currentProduct,
  //         ...updated,
  //       },
  //     }));
  //     console.log(updated);
  //     return updated;
  //   } catch (err) {
  //     console.error("Update error:", err);
  //     throw err;
  //   } finally {
  //     set({ loading: false });
  //   }
  // },
  // refreshById: async (id: string | null) => {
  //   if (!id) return;

  //   try {
  //     const { product: refresh } = await productsApi.detail(id);

  //     set((state) => {
  //       const exist = state.products.some((p) => p.id === id);
  //       console.log(exist);

  //       //Ürün var mı? varsa id'si uyuşuyor mu? eğer uyuşuyorsa refreshi yerine koy.
  //       const products = exist
  //         ? state.products.map((p) => (p.id === id ? refresh : p))
  //         : state.products;

  //       const currentProduct =
  //         state.currentProduct?.id === id ? refresh : state.currentProduct;

  //       return { products, currentProduct };
  //     });
  //   } catch (error) {
  //     console.error("refreshById error:", error);
  //   }
  // },
}));
