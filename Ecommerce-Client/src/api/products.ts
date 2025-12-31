import type { ProductBoxes } from "../types/Products";
import type { AddProduct, Products } from "../types/Products";
import { methods } from "./apiClient";

export const productsApi = {
  list: () => methods.get<Products[]>("Product"),

  detail: (id: string) => methods.get<Products>(`Product/${id}`),

  add: (product: AddProduct) =>
    methods.post<AddProduct, Products>("Product", product),

  addBox: (productId: string | null, boxItem: ProductBoxes) =>
    methods.post<ProductBoxes, string>(`Product/${productId}/boxes`, boxItem),

  update: (id: string, productPayload: Partial<AddProduct>) =>
    methods.put<Partial<AddProduct>, Products>(`Product/${id}`, productPayload),

  updateBoxItem: (productId: string, boxId: string, boxPayload: ProductBoxes) =>
    methods.put<Partial<ProductBoxes>, ProductBoxes>(
      `Product/${productId}/Boxes/${boxId}`,
      boxPayload
    ),

  remove: (productId: string) => methods.delete<Products>(`Product/${productId}`),

  removeBox: (boxId: string | null, productId: string | null) =>
    methods.delete<null>(`Product/${productId}/boxes/${boxId}`),
};
