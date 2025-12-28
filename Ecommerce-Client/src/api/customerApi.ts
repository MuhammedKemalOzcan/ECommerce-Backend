import type { AddAdress, Customer } from "../types/customer";
import { methods } from "./apiClient";

export const customerApi = {
  get: () => methods.get<Customer>("/Customer"),
  AddAddress: (address: AddAdress) =>
    methods.post<AddAdress, string>("/Customer/Address", address),
};
