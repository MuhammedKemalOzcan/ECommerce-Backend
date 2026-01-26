import { useEffect } from "react";
import { useCartStore } from "../../stores/cartStore";
import CartTable from "../components/cart/CartTable";
import { useShallow } from "zustand/shallow";
import Loading from "../components/common/Loading";

export default function Cart() {
  const listCart = useCartStore((s) => s.listCart);
  const { cart, clearCart, isLoading } = useCartStore(
    useShallow((s) => ({
      cart: s.cart,
      clearCart: s.clearCart,
      isLoading: s.isLoading,
    })),
  );

  useEffect(() => {
    listCart();
  }, [listCart]);


  if (isLoading) return <Loading />;

  return (
    <div className="flex flex-col">
      <div className="flex flex-col h-auto lg:p-8">
        <div className="flex mb-4 justify-between">
          <h1>SHOPPING CART</h1>
          <button
            disabled={!cart || cart.totalItemCount === 0}
            onClick={clearCart}
            className="btn-1 p-2"
          >
            Clear Cart
          </button>
        </div>
        <CartTable cart={cart} />
      </div>
    </div>
  );
}
