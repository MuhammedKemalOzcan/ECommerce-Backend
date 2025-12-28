import { create } from "zustand";
import type { AddAdress, Adress, Customer } from "../types/customer";
import { customerApi } from "../api/customerApi";

type CustomerState = {
  customer: Customer | null;
  loading: boolean;
  error: string | null;
  getCustomer: () => Promise<void>;
  AddAddress: (address: AddAdress) => Promise<void>;
};

export const useCustomerStore = create<CustomerState>((set) => ({
  customer: null,
  loading: false,
  error: null,
  getCustomer: async () => {
    set({ loading: true, error: null });
    try {
      const response = await customerApi.get();
      if (response) set({ customer: response });
      console.log("Get:", response);
    } catch (error: any) {
      set({
        error: error,
        loading: false,
      });
    } finally {
      set({ loading: false });
    }
  },
  AddAddress: async (address: AddAdress) => {
    set({ loading: true, error: null });
    try {
      const response = await customerApi.AddAddress(address);

      if (!response) return;
      const newId = response;

      const newAddress: Adress = { ...address, id: newId };

      set((state) => ({
        customer: state.customer
          ? {
              ...state.customer,
              addresses: [...(state.customer.addresses || []), newAddress],
            }
          : state.customer,
      }));

      console.log(response);
    } catch (error) {
      console.log(error);
    } finally {
      set({ loading: false });
    }
  },
}));
