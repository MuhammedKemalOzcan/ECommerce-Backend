import { LockKeyhole } from "lucide-react";
import { useCartStore } from "../../../stores/cartStore";
import { shippingOptions } from "../../../constants/shippingOptions";
import { useFormContext } from "react-hook-form";
import { baseApiUrl } from "../../../constants/apiUrl";

export default function OrderSummary() {
  const cart = useCartStore((s) => s.cart);

  const { watch } = useFormContext();

  const watchedShippingValue = watch("shippingCost");

  const currentShippingCost = shippingOptions.find(
    (opt) => Number(watchedShippingValue) === opt.price,
  );

  const shippingPrice = currentShippingCost?.price ?? 0;

  const taxRate = 0.2;

  const taxAmount = cart ? cart.totalAmount * taxRate : 0;

  const grandTotal = cart ? cart?.totalAmount + taxAmount + shippingPrice : 0;

  console.log(currentShippingCost);

  return (
    <div className="flex flex-col bg-white w-[70] justify-self-end p-10 gap-4 rounded-3xl shadow">
      <div className="flex gap-3 items-center">
        <h1>Order Summary</h1>
      </div>

      {cart?.cartItems?.map((item) => (
        <div key={item.id} className="flex items-center justify-between">
          <div className="flex gap-4">
            {item.productImageUrl !== null && (
              <img
                className="size-16 rounded-3xl"
                src={`${baseApiUrl}/${item.productImageUrl}`}
              />
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
        <div>
          <div className="flex justify-between items-center">
            <p className="text-[#6B7280]">Shipping</p>
            <h1 className="text-sm">${shippingPrice}</h1>
          </div>
        </div>
        <div className="flex justify-between items-center">
          <p className="text-[#6B7280]">Tax (20%)</p>
          <h1 className="text-sm">${taxAmount.toFixed(2)}</h1>
        </div>
      </div>
      <div className="border-b"></div>
      <div className="flex justify-between items-center">
        <p className="text-[#6B7280]">Total</p>
        <h1 className="text-sm">${grandTotal}</h1>
      </div>
      <button type="submit" className="btn-1 p-2 rounded-3xl shadow-xl">
        Place Order
      </button>
      <div className="flex items-center justify-center gap-4">
        <LockKeyhole size={12} />
        <p className="text-sm">Payments are SSL encrypted and secured</p>
      </div>
    </div>
  );
}
