import type { Products } from "./Products";


export interface Order {
  id: string;
  orderCode: string;
  userId: string;
  date: string;
  status: string;
  totalAmount: number;
  currency: string;
  products: Products[];
  customerName: string;
  shippingAddress: string;
  phoneNumber: string;
  cart: {
    cartNumber: string;
    name: string;
    lastDate: string;
  }[];
}