import type { Products } from "../types/Products";
import { methods } from "./apiClient";

export const productGalleryApi = {
  add: (productId: string | null, formData: FormData) =>
    methods.post<typeof formData, Products>(
      `Product/Upload/${productId}`,
      formData,
    ),
  delete: (productId: string | null, imageId: string | null) =>
    methods.delete(`Product/DeleteProductImage/${productId}/${imageId}`),
};
