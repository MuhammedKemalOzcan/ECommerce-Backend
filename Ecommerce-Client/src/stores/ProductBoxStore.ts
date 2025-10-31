import { create } from "zustand";
import type { ProductBoxes } from "../types/ProductBox";
import { productBoxApi } from "../api/products";

type BoxProps = {
  productBoxes: ProductBoxes[];
  currentProductBox: ProductBoxes | null;
  getAllBoxes: (id: string | null) => Promise<void>;
  updateBoxItems: (id: string | null, data: ProductBoxes) => Promise<void>;
  loading: boolean;
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
}));
