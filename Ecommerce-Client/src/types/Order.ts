import type { Products } from "./Products";

export type Order = {
  id: string;
  orderCode: string;
  userId: string;
  date: string;
  status: string;
  totalAmount: number;
  currency: string;
  products: Products[];
};
