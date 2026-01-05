import type {
  AddAdress,
  Adress,
  Customer,
} from "../types/customer";
import { methods } from "./apiClient";

export const customerApi = {
  get: () => methods.get<Customer>("/Customer"),

  AddAddress: (address: AddAdress) =>
    methods.post<AddAdress, string>("/Customer/Address", address),

  updateCustomer: (customerData: Customer) =>
    methods.put<Customer, Customer>("/Customer", customerData),

  removeAddress: (customerAddressId: string | null) => {
    methods.delete<Adress>(`Customer/Address/${customerAddressId}`);
  },

  setPrimaryAddress: (customerAddressId: string) => {
    methods.put<null, null>(
      `Customer/SetPrimaryAddress/${customerAddressId}`,
      null
    );
  },
};
