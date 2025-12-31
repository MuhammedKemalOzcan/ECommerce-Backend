import type { ProductBoxes } from "../types/ProductBox";
import { methods } from "./apiClient";


export const productBoxApi = {
  list: (productId: string) =>
    methods.get<{ productBox: ProductBoxes[] }>(`Product/${productId}/boxes`),

  update: (boxId: string, patch: Partial<ProductBoxes>) =>
    methods.put<Partial<ProductBoxes>, ProductBoxes>(
      `Product/Boxes/${boxId}`,
      patch
    ),

  
};
