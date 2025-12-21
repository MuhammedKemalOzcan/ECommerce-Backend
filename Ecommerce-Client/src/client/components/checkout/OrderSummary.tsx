import { Lock, LockKeyhole } from "lucide-react";
import { useCartStore } from "../../../stores/cartStore";

export default function OrderSummary() {
  const cart = useCartStore((s) => s.cart);

  return (
    <div className="flex flex-col bg-white w-[70] justify-self-end p-10 gap-4 rounded-3xl shadow">
      <div className="flex gap-3 items-center">
        <h1>Order Summary</h1>
      </div>

      {cart?.cartItems?.map((item) => (
        <div className="flex items-center justify-between">
          <div className="flex gap-4">
            {item.productImageUrl !== null && (
              <img className="size-16 rounded-3xl" src={item.productImageUrl} />
            )}
            <div className="flex flex-col justify-center">
              <h1 className="text-sm">{item.productName}</h1>
              <p>{item.quantity}x</p>
            </div>
          </div>
          <h1 className="text-sm">${item.totalPrice.toFixed(2)}</h1>
        </div>
      ))}

      <div className="border-b"></div>
      <div>
        <div className="flex justify-between items-center">
          <p className="text-[#6B7280]">Subtotal</p>
          <h1 className="text-sm">${cart?.totalAmount.toFixed(2)}</h1>
        </div>
        <div className="flex justify-between items-center">
          <p className="text-[#6B7280]">Shipping</p>
          <h1 className="text-sm">$15.00</h1>
        </div>
        <div className="flex justify-between items-center">
          <p className="text-[#6B7280]">Tax (estimated)</p>
          <h1 className="text-sm">$35.84</h1>
        </div>
      </div>
      <div className="border-b"></div>
      <div>Total</div>
      <button className="btn-1 p-2 rounded-3xl shadow-xl">Place Order</button>
      <div className="flex items-center justify-center gap-4">
        <LockKeyhole size={12} />
        <p className="text-sm">Payments are SSL encrypted and secured</p>
      </div>
    </div>
  );
}
