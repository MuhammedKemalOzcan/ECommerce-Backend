import type { ProductBoxes } from "../types/ProductBox";
import { methods } from "./apiClient";

type ApiMessage = {
  message: string;
};

export const productBoxApi = {
  list: (productId: string) =>
    methods.get<{ productBox: ProductBoxes[] }>(`Product/${productId}/boxes`),

  update: (boxId: string, patch: Partial<ProductBoxes>) =>
    methods.put<Partial<ProductBoxes>, ProductBoxes>(
      `Product/Boxes/${boxId}`,
      patch
    ),

  add: (productId: string | null, boxItem: ProductBoxes) =>
    methods.post<ProductBoxes, ProductBoxes>(
      `Product/${productId}/boxes`,
      boxItem
    ),

  remove: (boxId: string | null) =>
    methods.delete<ApiMessage>(`Product/Boxes/${boxId}`),
};
