import type { AddAdress, AddCustomer, Customer } from "../types/customer";
import { methods } from "./apiClient";

export const customerApi = {
  add: (customer: AddCustomer) =>
    methods.post<AddCustomer, Customer>("/Customer", customer),
  get: () => methods.get<{ data: Customer }>("/Customer"),
  update: (updateCustomer: AddCustomer) =>
    methods.put<AddCustomer, { data: Customer }>("/Customer", updateCustomer),
  addAddress: (address: AddAdress) =>
    methods.post<AddAdress, string>("/Customer/AddAddress", address),
};
