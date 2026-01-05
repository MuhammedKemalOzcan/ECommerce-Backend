import React from "react";
import CheckoutCart from "./CheckoutCart";
import FormField from "../../../admin/components/products/FormField";

export default function Payment() {
  return (
    <CheckoutCart count="3" title="Payment">
      <div className="flex flex-col gap-4">
        <FormField
          label="Card Number"
          id="card number"
          type="number"
          placeHolder="0000 0000 0000 0000"
        />
        <div className="lg:flex-row flex flex-col gap-3 justify-between">
          <FormField
            label="Expiry Date"
            id="expiry date"
            type="number"
            placeHolder="MM/YY"
          />
          <FormField label="CVC" id="cvc" type="number" placeHolder="123" />
        </div>
        <FormField
          label="Name on Card"
          id="name"
          type="text"
          placeHolder="e.g. John Doe"
        />
      </div>
    </CheckoutCart>
  );
}
