import type { AddProduct, Products } from "../types/Products";
import { methods } from "./apiClient";

export const productsApi = {
  list: () => methods.get<{ products: Products[] }>("Product"),

  detail: (id: string) => methods.get<Products>(`Product/${id}`),

  add: (product: AddProduct) =>
    methods.post<AddProduct, Products>("Product", product),

  update: (id: string, patch: Partial<AddProduct>) =>
    methods.put<Partial<AddProduct>, Products>(`Product/${id}`, patch),

  remove: (id: string) => methods.delete<Products>(`Product/${id}`),
};
