import type { ImageGalleries } from "../types/Gallery";
import { methods } from "./apiClient";

export const productGalleryApi = {
  add: (productId: string | null, formData: FormData) =>
    methods.post<typeof formData, ImageGalleries>(
      `Product/Upload/${productId}`,
      formData
    ),
};
