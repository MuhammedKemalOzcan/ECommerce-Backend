import { Minus, Plus, Trash2Icon } from "lucide-react";
import { BeatLoader } from "react-spinners";
import { useShallow } from "zustand/shallow";
import { useCartStore } from "../../../stores/cartStore";
import type { Cart } from "../../../types/cart";
import { useNavigate } from "react-router-dom";
import ConfirmationModal from "../../../admin/components/common/ConfirmationModal";
import { useAuthStore } from "../../../auth/authStore";
import { useState } from "react";

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

  const [isOpen, setIsOpen] = useState<boolean>(false);

  const user = useAuthStore((s) => s.user);

  const navigate = useNavigate();

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

  const handleCheckout = () => {
    if (!user) {
      setIsOpen(true);
    } else {
      navigate("/checkout");
    }
  };

  const hasItems = Boolean(cart?.cartItems.length);

  return (
    <div className="flex w-full flex-col gap-4">
      {/* Cart items */}
      <div className="flex flex-col gap-2 rounded-lg border border-gray-200 bg-white shadow-lg">
        {isLoading && (
          <div className="absolute inset-0 z-50 flex items-center justify-center rounded-lg bg-white/80 backdrop-blur-sm">
            <div className="flex flex-col items-center gap-3">
              <BeatLoader color="#D87D4A" size={12} />
              <p className="text-sm font-medium text-gray-600">
                Guncelleniyor...
              </p>
            </div>
          </div>
        )}
        {cart?.cartItems?.map((item, index) => (
          <div
            key={item.id}
            className={`flex justify-between gap-2 p-4 ${
              index % 2 === 1 ? "bg-gray-50" : "bg-white"
            }`}
          >
            <div className="flex gap-3 text-start">
              {item.productImageUrl !== null && (
                <img className="size-24" src={item.productImageUrl} />
              )}
              <div className="my-auto flex flex-col justify-center gap-2 ">
                <h1 className="font-semibold text-gray-900">
                  {item.productName}
                </h1>
                <div className="flex items-center gap-4">
                  <div className="flex w-20 justify-between bg-gray-300 p-2 text-lg font-medium">
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
            <div className="flex flex-col items-center justify-center gap-12">
              <button
                onClick={() => handleDelete(item.id)}
                className="text-gray-400 transition hover:text-red-500"
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
          <div className="p-4 text-center text-sm text-gray-500">
            Sepetiniz bos.
          </div>
        )}
      </div>

      {/* Summary card */}
      <div className="ml-auto flex w-full max-w-sm flex-col gap-3 rounded-lg border border-gray-200 bg-white p-4 shadow-lg">
        <div className="flex items-center justify-between">
          <h2 className="text-sm font-medium text-gray-500">TOTAL</h2>
          <p className="text-lg font-semibold text-gray-900">
            ${cart?.totalAmount ?? 0}
          </p>
        </div>
        <button
          type="button"
          disabled={!hasItems}
          className="btn-1 w-full py-3 disabled:cursor-not-allowed disabled:opacity-60"
          onClick={handleCheckout}
        >
          Proceed to Checkout
        </button>
      </div>
      {isOpen == true && (
        <ConfirmationModal
          isOpen={isOpen}
          title="Giris Yapilmadi"
          message="Lutfen odeme islemi icin giris yapiniz."
          onConfirm={() => navigate("/login")}
          onCancel={() => setIsOpen(false)}
          confirmText="Giriş Yap"
          cancelText="İptal"
          variant="warning"
        />
      )}
    </div>
  );
}
