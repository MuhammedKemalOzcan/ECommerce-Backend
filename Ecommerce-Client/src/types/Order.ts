import type { Customer, Location } from "./customer";
import type { Products } from "./Products";

export interface CreateOrder {
  shippingCost: number;
  shippingAddress: Location;
  billingAddress: Location;
  installment: number;
}

export interface Order {
  id: string;
  orderDate: string;
  grandTotal: number;
  orderCode: string;
  deliveryStatus: string;
  products: Products[];
  shippingAddress: Location;
  customer: Customer;
  paymentInfo: PaymentInfo;
  orderItems: OrderItems[];
  subTotal: number;
  shippingCost: number;
  updatedDate: string
  shippedDate?: string;
  deliveredDate?: string;
}

export interface OrderInfo {
  hasNextPage: boolean;
  hasPreviousPage: boolean;
  items: Order[];
  pageIndex: number;
  totalPages: number;
  orderCount: number;
}

export interface PaymentInfo {
  cardAssociation: string;
  cardHolderName: string;
  cardLastFourDigits: string;
}

export interface OrderItems {
  productName: string;
  price: number;
  quantity: number;
  imageUrl: string;
}
