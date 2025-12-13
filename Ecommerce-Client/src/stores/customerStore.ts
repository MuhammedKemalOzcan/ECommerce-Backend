import { create } from "zustand";
import type { AddAdress, AddCustomer, Customer } from "../types/customer";
import { customerApi } from "../api/customerApi";

type customerState = {
  customer: Customer | null;
  createCustomer: (newCustomer: AddCustomer) => Promise<void>;
  getCustomer: () => Promise<void>;
  updateCustomer: (data: Customer) => Promise<void>;
  loading: boolean;
  error: string | null;
  addAddress: (data: AddAdress) => Promise<void>;
};

export const useCustomerStore = create<customerState>((set) => ({
  customer: null,
  loading: false,
  error: null,
  createCustomer: async (newCustomer: AddCustomer) => {
    set({ loading: true });
    try {
      const created = await customerApi.add(newCustomer);
      console.log(created);
      set({ customer: created });
    } catch (error: any) {
      set({
        error: error,
        loading: false,
      });
    } finally {
      set({ loading: false });
    }
  },
  getCustomer: async () => {
    set({ loading: true });
    try {
      const response = await customerApi.get();
      if (response.data) set({ customer: response.data });
    } catch (error: any) {
      set({
        error: error,
        loading: false,
      });
    } finally {
      set({ loading: false });
    }
  },
  updateCustomer: async (data: AddCustomer) => {
    set({ loading: true });
    try {
      const response = await customerApi.update(data);
      set({ customer: response.data });
      console.log(response);
    } catch (error: any) {
      set({
        error: error,
        loading: false,
      });
    } finally {
      set({ loading: false });
    }
  },
  addAddress: async (data: AddAdress) => {
    set({ loading: true });
    try {
      const response = await customerApi.addAddress(data);
      console.log(response);
    } catch (error: any) {
      set({ error: error, loading: false });
    } finally {
      set({ loading: false });
    }
  },
}));
