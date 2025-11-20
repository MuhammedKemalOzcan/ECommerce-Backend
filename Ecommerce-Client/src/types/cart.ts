export type Cart = {
  id: string;
  userId: string | null;
  cartItems: CartItem[];
  totalAmount: number;
  totalItemCount: number;
  createdDate?: string;
  lastModifiedDate?: string | null;
};

export type CartItem = {
   id: string;
  productId: string;
  productName: string;
  productImageUrl: string | null;
  quantity: number;
  stock: number | null;
  totalPrice: number;
  unitPrice: number;
};

export type AddItem = {
  productId: string | undefined;
  quantity: number;
};
