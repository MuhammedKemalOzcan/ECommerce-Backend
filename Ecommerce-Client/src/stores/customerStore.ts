import { create } from "zustand";
import type { AddCustomer, Customer } from "../types/customer";
import { customerApi } from "../api/cart";

type customerState = {
  customer: Customer | null;
  createCustomer: (newCustomer: AddCustomer) => Promise<void>;
};

export const useCustomerStore = create<customerState>((set, get) => ({
  customer: null,

  createCustomer: async (newCustomer: AddCustomer) => {
    try {
      const created = await customerApi.add(newCustomer);
      console.log(created);
      set({ customer: created });
    } catch (error) {
      console.log(error);
    }
  },
}));
