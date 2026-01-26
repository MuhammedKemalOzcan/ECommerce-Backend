export type Cart = {
  id: string;
  userId: string | null;
  sessionId: string;
  cartItems: CartItem[];
  totalAmount: number;
  totalItemCount: number;
};

export type CartItem = {
  id: string;
  productId: string;
  productName: string;
  productImageUrl: string | null;
  quantity: number;
  unitPrice: number;
  totalPrice: number;
};

export type AddItem = {
  productId: string | undefined;
  quantity: number;
};
export type UpdateCartItem = {
  cartItemId: string;
  quantity: number;
};
