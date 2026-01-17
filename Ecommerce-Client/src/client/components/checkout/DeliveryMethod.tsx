import { shippingOptions } from "../../../constants/shippingOptions";
import CheckoutCart from "./CheckoutCart";
import { useFormContext } from "react-hook-form";

export default function DeliveryMethod() {
  const { register, watch } = useFormContext();

  const currentShippingCost = watch("shippingCost");

  return (
    <CheckoutCart count="2" title="Delivery Method">
      <div>
        {shippingOptions.map((opt) => (
          <label
            key={opt.id}
            className={`flex items-center gap-2 justify-between border p-6 rounded-3xl mb-4 cursor-pointer transition-colors
              ${
                Number(currentShippingCost) === opt.price
                  ? "border-[#D87D4A] bg-orange-50"
                  : "border-gray-200"
              }`}
          >
            <div className="flex items-center gap-2">
              <input
                type="radio"
                {...register("shippingCost", {
                  valueAsNumber: true,
                })}
                value={opt.price}
                className="accent-[#D87D4A] w-5 h-5"
              />
              <div>
                <span className="font-bold block text-sm">{opt.label}</span>
                <p className="text-xs text-gray-400">{opt.desc}</p>
              </div>
            </div>
            <p className="text-[#D87D4A] font-bold">
              {opt.price > 0 ? "$" + opt.price.toFixed(2) : "Free"}
            </p>
          </label>
        ))}
      </div>
    </CheckoutCart>
  );
}
