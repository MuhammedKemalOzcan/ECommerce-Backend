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
    <div className="w-full">
      {isLoading && (
        <div className="fixed inset-0 z-[60] flex items-center justify-center bg-[#FAFAFA]/80 backdrop-blur-sm">
          <div className="flex flex-col items-center gap-3">
            <BeatLoader color="#D87D4A" size={15} margin={4} />
            <p className="text-sm font-bold uppercase tracking-wider text-[#101010]">
              Sepet Güncelleniyor...
            </p>
          </div>
        </div>
      )}

      {/* Grid Layout: Left Content (Items) - Right Content (Summary) */}
      <div className="grid grid-cols-1 lg:grid-cols-12 gap-8">
        {/* Cart Items Column */}
        <div className="lg:col-span-8 bg-[#FFFFFF] rounded-xl border border-[#F1F1F1] overflow-hidden shadow-sm">
          <div className="p-6 sm:p-8 space-y-6">
            <h2 className="text-lg font-bold uppercase tracking-wide text-[#101010] mb-6">
              Sepet ({cart?.cartItems?.length || 0})
            </h2>

            {cart?.cartItems?.map((item) => (
              <div
                key={item.id}
                className="flex flex-col sm:flex-row items-center gap-6 py-6 border-b border-[#F1F1F1] last:border-0 last:pb-0"
              >
                {/* Image */}
                <div className="shrink-0">
                  {item.productImageUrl ? (
                    <div className="w-24 h-24 rounded-lg bg-[#F1F1F1] flex items-center justify-center overflow-hidden">
                      <img 
                        className="w-full h-full object-contain mix-blend-multiply p-2" 
                        src={item.productImageUrl} 
                        alt={item.productName} 
                      />
                    </div>
                  ) : (
                    <div className="w-24 h-24 rounded-lg bg-[#F1F1F1] animate-pulse" />
                  )}
                </div>

                {/* Info & Controls */}
                <div className="flex-1 w-full flex flex-col sm:flex-row sm:items-center justify-between gap-4">
                  <div className="space-y-1 text-center sm:text-left">
                    <h3 className="text-sm font-bold text-[#101010] uppercase w-full sm:w-40 md:w-auto truncate">
                      {item.productName}
                    </h3>
                    <p className="text-sm font-bold text-[#101010]/50">
                      ${item.unitPrice}
                    </p>
                  </div>

                  {/* Quantity & Actions Wrapper */}
                  <div className="flex items-center justify-between sm:justify-end gap-6 w-full sm:w-auto">
                    {/* Quantity Selector */}
                    <div className="flex items-center bg-[#F1F1F1] px-4 py-2 rounded-sm">
                      <button 
                        onClick={() => handleMinus(item.id, item.quantity)}
                        className="text-[#101010]/40 hover:text-[#D87D4A] transition-colors"
                      >
                        <Minus size={14} />
                      </button>
                      <span className="mx-4 text-xs font-bold text-[#101010] w-3 text-center">
                        {item.quantity}
                      </span>
                      <button 
                        onClick={() => handlePlus(item.id, item.quantity)}
                        className="text-[#101010]/40 hover:text-[#D87D4A] transition-colors"
                      >
                        <Plus size={14} />
                      </button>
                    </div>

                    <div className="flex items-center gap-6">
                      <p className="text-sm font-bold text-[#101010] min-w-[60px] text-right">
                        ${item.totalPrice}
                      </p>
                      <button
                        onClick={() => handleDelete(item.id)}
                        className="text-[#101010]/40 hover:text-[#D87D4A] transition-colors p-1"
                        title="Ürünü Sil"
                      >
                        <Trash2Icon size={18} />
                      </button>
                    </div>
                  </div>
                </div>
              </div>
            ))}

            {(!cart || cart.cartItems.length === 0) && (
              <div className="py-12 flex flex-col items-center justify-center text-center">
                <div className="w-16 h-16 bg-[#F1F1F1] rounded-full flex items-center justify-center mb-4">
                  <span className="text-2xl">🛒</span>
                </div>
                <p className="text-[#101010]/50 font-medium">Sepetinizde ürün bulunmamaktadır.</p>
              </div>
            )}
          </div>
        </div>

        {/* Summary Column */}
        <div className="lg:col-span-4 sticky top-4">
          <div className="bg-[#FFFFFF] rounded-xl border border-[#F1F1F1] shadow-sm p-6 sm:p-8">
            <h3 className="text-lg font-bold uppercase tracking-wide text-[#101010] mb-6">
              Sipariş Özeti
            </h3>
            
            <div className="space-y-4 mb-8">
              <div className="flex items-center justify-between">
                <span className="text-sm font-medium text-[#101010]/50 uppercase">Ara Toplam</span>
                <span className="text-lg font-bold text-[#101010]">${cart?.totalAmount ?? 0}</span>
              </div>
              <div className="flex items-center justify-between">
                <span className="text-sm font-medium text-[#101010]/50 uppercase">Kargo</span>
                <span className="text-sm font-bold text-[#D87D4A]">ÜCRETSİZ</span>
              </div>
              <div className="pt-4 border-t border-[#F1F1F1] flex items-center justify-between mt-4">
                <span className="text-base font-medium text-[#101010]/50 uppercase">Toplam</span>
                <span className="text-xl font-bold text-[#D87D4A]">${cart?.totalAmount ?? 0}</span>
              </div>
            </div>

            <button
              type="button"
              disabled={!hasItems}
              onClick={handleCheckout}
              className="w-full bg-[#D87D4A] hover:bg-[#fbaf85] text-white text-sm font-bold uppercase tracking-widest py-4 px-6 transition-all duration-200 disabled:opacity-50 disabled:cursor-not-allowed disabled:hover:bg-[#D87D4A]"
            >
              Ödeme Yap
            </button>
          </div>
        </div>
      </div>

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
    </div>
  );
}