import { Minus, Plus, Trash2Icon } from "lucide-react";
import type { Cart } from "../../../types/cart";
import { useCartStore } from "../../../stores/cartStore";
import { useShallow } from "zustand/shallow";
import { BeatLoader } from "react-spinners";

type CartProps = {
  cart: Cart | null;
};

export default function CartTable({ cart }: CartProps) {
  const { deleteCartItem, updateCartItem, isLoading } = useCartStore(
    useShallow((s) => ({
      deleteCartItem: s.deleteCartItem,
      updateCartItem: s.updateCartItem,
      isLoading: s.isLoading,
      error: s.error,
    }))
  );

  const handleDelete = (itemId: string | null) => {
    if (!itemId) return;
    deleteCartItem(itemId);
  };

  const handleMinus = (cartItemId: string | null, quantity: number) => {
    if (!cartItemId) return;
    if (quantity <= 1) return;
    updateCartItem({ cartItemId, quantity: quantity - 1 });
  };
  const handlePlus = (cartItemId: string | null, quantity: number) => {
    if (!cartItemId) return;
    updateCartItem({ cartItemId, quantity: quantity + 1 });
  };

  return (
    <div className="flex flex-col gap-4 w-full">
      {/* Cart items */}
      <div className="flex flex-col gap-2 bg-white rounded-lg border border-gray-200 shadow-lg">
        {isLoading && (
          <div className="absolute inset-0 flex items-center justify-center bg-white/80 backdrop-blur-sm rounded-lg z-50">
            <div className="flex flex-col items-center gap-3">
              <BeatLoader color="#D87D4A" size={12} />
              <p className="text-sm text-gray-600 font-medium">
                Güncelleniyor...
              </p>
            </div>
          </div>
        )}
        {cart?.cartItems?.map((item, index) => (
          <div
            key={item.id}
            className={`flex gap-2 p-4 justify-between ${
              index % 2 === 1 ? "bg-gray-50" : "bg-white"
            }`}
          >
            <div className="flex gap-3 text-start">
              {item.productImageUrl !== null && (
                <img className="size-24" src={item.productImageUrl} />
              )}
              <div className="flex flex-col gap-2 my-auto justify-center ">
                <h1 className="font-semibold text-gray-900">
                  {item.productName}
                </h1>
                <div className="flex items-center gap-4">
                  <div className="flex justify-between text-lg font-medium bg-gray-300 w-20 p-2">
                    <button onClick={() => handleMinus(item.id, item.quantity)}>
                      <Minus size={16} />
                    </button>

                    <p>{item.quantity}</p>
                    <button onClick={() => handlePlus(item.id, item.quantity)}>
                      <Plus size={16} />
                    </button>
                  </div>
                  <p>x</p>
                  <p className="text-lg font-medium text-gray-700">
                    ${item.unitPrice}
                  </p>
                </div>
              </div>
            </div>
            <div className="flex flex-col gap-12 items-center justify-center">
              <button
                onClick={() => handleDelete(item.id)}
                className="text-gray-400 hover:text-red-500 transition"
              >
                <Trash2Icon size={18} />
              </button>
              <h2 className="text-sm font-semibold text-gray-900">
                ${item.totalPrice}
              </h2>
            </div>
          </div>
        ))}

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
