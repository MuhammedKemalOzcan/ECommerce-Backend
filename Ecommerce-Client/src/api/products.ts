import type { ProductBoxes } from "../types/ProductBox";
import type { AddProduct, Products } from "../types/Products";
import { methods } from "./apiClient";

export const productsApi = {
  list: () => methods.get<{ products: Products[] }>("Product"),

  detail: (id: string) => methods.get<{ product: Products }>(`Product/${id}`),

  add: (product: AddProduct) =>
    methods.post<AddProduct, Products>("Product", product),

  update: (id: string, patch: Partial<AddProduct>) =>
    methods.put<Partial<AddProduct>, Products>(`Product/${id}`, patch),

  remove: (id: string) => methods.delete<Products>(`Product/${id}`),
};

export const productBoxApi = {
  list: (productId: string) =>
    methods.get<{ productBox: ProductBoxes[] }>(`Product/${productId}/boxes`),

  update: (boxId: string, patch: Partial<ProductBoxes>) =>
    methods.put<Partial<ProductBoxes>, ProductBoxes>(
      `Product/Boxes/${boxId}`,
      patch
    ),

  remove: (id: string) => methods.delete<Products>(`Product/${id}`),
};
