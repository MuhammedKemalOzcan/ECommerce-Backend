import { create } from "zustand";
import type { ProductBoxes } from "../types/ProductBox";
import { toast } from "react-toastify";
import { productBoxApi } from "../api/productBoxApi";

type BoxProps = {
  productBoxes: ProductBoxes[];
  currentProductBox: ProductBoxes | null;
  getAllBoxes: (id: string | null) => Promise<void>;
  updateBoxItems: (id: string | null, data: ProductBoxes) => Promise<void>;
  loading: boolean;
  deleteBoxItem: (boxId: string | null) => Promise<void>;
  createBoxItem: (
    productId: string | null,
    data: ProductBoxes
  ) => Promise<void>;
};

export const useProductBoxStore = create<BoxProps>((set) => ({
  productBoxes: [],
  currentProductBox: null,
  loading: false,
  getAllBoxes: async (id: string | null) => {
    if (!id) return;
    try {
      const response = await productBoxApi.list(id);
      set({ productBoxes: response.productBox });
    } catch (error) {}
  },
  updateBoxItems: async (boxId: string | null, data: ProductBoxes) => {
    if (!boxId) {
      console.error("❌ Box ID is required for update");
      throw new Error("Box ID is required");
    }
    set({ loading: true });
    try {
      const updated = await productBoxApi.update(boxId, data);
      set((state) => ({
        productBoxes: state.productBoxes.map((boxes) =>
          boxes.id === boxId ? { ...boxes, ...updated } : boxes
        ),
        currentProductBox:
          state.currentProductBox?.id === boxId
            ? { ...state.currentProductBox, ...updated }
            : state.currentProductBox,
      }));
    } catch (error) {
      console.log(error);
    } finally {
      set({ loading: false });
    }
  },
  deleteBoxItem: async (boxId: string | null) => {
    if (!boxId) console.error("BoxId Required");
    try {
      const response = await productBoxApi.remove(boxId);
      console.log(response.message);

      set((state) => ({
        productBoxes: state.productBoxes.filter((box) => boxId !== box.id),
      }));
      toast.success(response?.message);
    } catch (error) {
      console.log(error);
    }
  },
  createBoxItem: async (productId: string | null, data: ProductBoxes) => {
    if (productId === null) toast.error("Ürün Id'si gerekli");
    try {
      const response = await productBoxApi.add(productId, data);
      set((state) => ({ productBoxes: [...state.productBoxes, response] }));
    } catch (error) {
      console.log(error);
    }
  },
}));
