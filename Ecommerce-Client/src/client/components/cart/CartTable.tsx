import { Trash2Icon } from "lucide-react";
import type { Cart } from "../../../types/cart";
import { useCartStore } from "../../../stores/cartStore";
import { useShallow } from "zustand/shallow";

type CartProps = {
  cart: Cart | null;
};

export default function CartTable({ cart }: CartProps) {
  const { deleteCartItem, cartItems } = useCartStore(
    useShallow((s) => ({
      cartItems: s.cartItems,
      deleteCartItem: s.deleteCartItem,
    }))
  );

  const handleDelete = (itemId: string | null) => {
    if (!itemId) return;
    deleteCartItem(itemId);
  };
  return (
    <div className="flex flex-col gap-4 w-full">
      {/* Cart items */}
      <div className="flex flex-col gap-2 bg-white rounded-lg border border-gray-200 shadow-lg">
        {cart?.cartItems?.map((item, index) => (
          <div
            key={item.id}
            className={`flex flex-col gap-2 p-4 ${
              index % 2 === 1 ? "bg-gray-50" : "bg-white"
            }`}
          ></div>
        ))}
        {/* {cart?.cartItems.map((item, index) => (
          <div
            key={item.id}
            className={`flex flex-col gap-2 p-4 ${
              index % 2 === 1 ? "bg-gray-50" : "bg-white"
            }`}
          >
            <div className="flex items-start justify-between">
              {item.productImageUrl && (
                <img className="size-24" src={item.productImageUrl} />
              )}
              <h1 className="font-semibold text-gray-900">
                {item.productName}
              </h1>
              <button
                onClick={() => handleDelete(item.id)}
                className="text-gray-400 hover:text-red-500 transition"
              >
                <Trash2Icon size={18} />
              </button>
            </div>
            <div className="flex items-center justify-between">
              <div className="flex items-center gap-6">
                <div className="flex items-center gap-4">
                  <p className="text-lg font-medium">{item.quantity}</p>
                  <p>x</p>
                  <p className="text-lg font-medium text-gray-700">
                    ${item.unitPrice}
                  </p>
                </div>
              </div>

              <div className="text-right">
                <h2 className="text-sm font-semibold text-gray-900">
                  ${item.totalPrice}
                </h2>
              </div>
            </div>
          </div>
        ))} */}

        {(!cart || cart.cartItems.length === 0) && (
          <div className="p-4 text-sm text-gray-500 text-center">
            Sepetiniz boş.
          </div>
        )}
      </div>

      {/* Summary card */}
      <div className="w-full max-w-sm ml-auto bg-white rounded-lg border border-gray-200 shadow-lg p-4 flex flex-col gap-3">
        <div className="flex justify-between items-center">
          <h2 className="text-sm font-medium text-gray-500">TOTAL</h2>
          <p className="text-lg font-semibold text-gray-900">
            ${cart?.totalAmount ?? 0}
          </p>
        </div>
      </div>
    </div>
  );
}