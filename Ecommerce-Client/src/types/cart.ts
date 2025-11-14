export type Cart = {
  cartItems: CartItems[];
  totalAmount: number;
  totalItemCount: number;
};

export type CartItems = {
  productName: string;
  productImageUrl: string;
  quantity: number;
  unitPrice: number;
  TotalPrice: number;
  stock: number;
};
