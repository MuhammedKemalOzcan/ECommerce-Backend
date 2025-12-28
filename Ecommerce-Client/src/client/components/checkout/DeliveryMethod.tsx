import React, { useState } from "react";
import CheckoutCart from "./CheckoutCart";

export default function DeliveryMethod() {
  const [selectedShipping, setSelectedShipping] = useState<string>("standard");

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setSelectedShipping(event.target.value);
  };

  return (
    <CheckoutCart count="2" title="Delivery Method">
      <form>
        {shippingOptions.map((opt) => (
          <div className="flex items-center gap-2 justify-between border p-6 rounded-3xl mb-4 focus:border-[#D87D4A]">
            <div className="flex items-center gap-2">
              <input
                type="radio"
                name="shipping"
                id={opt.id}
                value={opt.id}
                checked={selectedShipping === opt.id}
                onChange={handleChange}
                className="radio-option.selected"
              />
              <div>
                <label htmlFor={opt.id}>{opt.label}</label>
                <p className="text-xs text-gray-400">{opt.desc}</p>
              </div>
            </div>
            <p className="text-[#D87D4A] font-bold">
              {opt.price > 0 ? "$" + opt.price.toFixed(2) : "Free"}
            </p>
          </div>
        ))}
      </form>
    </CheckoutCart>
  );
}

const shippingOptions = [
  {
    id: "standard",
    label: "Standard Shipping",
    price: 0,
    desc: "5-7 Business Days",
  },
  {
    id: "express",
    label: "Express Shipping",
    price: 15.0,
    desc: "1-3 Business Days",
  },
];
