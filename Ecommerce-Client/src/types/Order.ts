import type { Customer, Location } from "./customer";
import type { Products } from "./Products";

export interface CreateOrder {
  shippingCost: number;
  shippingAddress: Location;
  billingAddress: Location;
  paymentInfo: PaymentInfo;
  installment: number;
}

export interface Order {
  id:string
  orderDate: string;
  grandTotal: number;
  orderCode: string;
  deliveryStatus: string;
  products: Products[];
  shippingAddress: Location;
  customer: Customer;
  paymentInfo: PaymentInfo;
  orderItems: OrderItems[];
}

export interface PaymentInfo {
  cardNumber: string;
  expireMonth: string;
  expireYear: string;
  cvc: string;
  cardHolderName: string;
}

export interface OrderItems {
  productName: string;
  price: number;
  quantity: number;
  imageUrl: string;
}
