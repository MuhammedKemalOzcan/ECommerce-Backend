import { create } from "zustand";
import type { AddAdress, Adress, Customer } from "../types/customer";
import { customerApi } from "../api/customerApi";

type CustomerState = {
  customer: Customer | null;
  loading: boolean;
  error: string | null;
  getCustomer: () => Promise<void>;
  AddAddress: (address: AddAdress) => Promise<void>;
  removeAddress: (customerAddressid: string | null) => Promise<void>;
  setPrimaryAddress: (id: string | null) => Promise<void>;
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
      console.log(address);

      if (!response) return;
      const newId = response;

      const newAddress: Adress = { ...address, id: newId };

      set((state) => {
        // Eğer müşteri datası henüz yoksa işlem yapma
        if (!state.customer) return state;

        let currentAddresses = state.customer.addresses || [];

        // ⚡ KRİTİK HAMLE:
        // Eğer yeni eklenen adres "Primary" ise, mevcut listedeki herkesi "Primary: false" yap.
        if (newAddress.isPrimary) {
          currentAddresses = currentAddresses.map((addr) => ({
            ...addr,
            isPrimary: false,
          }));
        }

        return {
          customer: {
            ...state.customer,
            addresses: [...currentAddresses, newAddress], // Güncellenmiş liste + Yeni Adres
          },
        };
      });
    } catch (error) {
      console.log(error);
    } finally {
      set({ loading: false });
    }
  },
  removeAddress: async (customerAddressId: string | null) => {
    set({ loading: true, error: null });
    if (!customerAddressId) return;
    try {
      await customerApi.removeAddress(customerAddressId);
      set((state) => ({
        customer: state.customer
          ? {
              ...state.customer,
              addresses: state.customer.addresses?.filter(
                (a) => a.id !== customerAddressId
              ),
            }
          : state.customer,
      }));
    } catch (error) {
      console.log(error);
    } finally {
      set({ loading: false });
    }
  },
  setPrimaryAddress: async (customerAddressId: string | null) => {
    set({ loading: true, error: null });
    if (!customerAddressId) return;
    try {
      await customerApi.setPrimaryAddress(customerAddressId);
    } catch (error) {
      console.log(error);
    } finally {
      set({ loading: false });
    }
  },
}));
