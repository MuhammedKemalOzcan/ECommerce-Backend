import type { ProductBoxes } from "../types/ProductBox";
import type { AddProduct, Products } from "../types/Products";
import { methods } from "./apiClient";

export const productsApi = {
  list: () => methods.get<Products[]>("Product"),

  detail: (id: string) => methods.get<Products>(`Product/${id}`),

  add: (product: AddProduct) =>
    methods.post<AddProduct, Products>("Product", product),

  addBox: (productId: string | null, boxItem: ProductBoxes) =>
    methods.post<ProductBoxes, string>(`Product/${productId}/boxes`, boxItem),

  update: (id: string, patch: Partial<AddProduct>) =>
    methods.put<Partial<AddProduct>, Products>(`Product/${id}`, patch),

  remove: (id: string) => methods.delete<Products>(`Product/${id}`),

  removeBox: (boxId: string | null, productId: string | null) =>
    methods.delete<null>(`Product/${productId}/boxes/${boxId}`),
};
