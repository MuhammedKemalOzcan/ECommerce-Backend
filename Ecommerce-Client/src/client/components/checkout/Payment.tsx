import CheckoutCart from "./CheckoutCart";
import FormField from "../../../admin/components/products/FormField";
import { useFormContext } from "react-hook-form";

export default function Payment() {
  const { register } = useFormContext();
  return (
    <CheckoutCart count="3" title="Payment">
      <div className="flex flex-col gap-4">
        <FormField
          label="Card Number"
          id="card number"
          type="text"
          placeHolder="0000 0000 0000 0000"
          {...register("paymentInfo.cardNumber")}
        />
        <div className="lg:flex-row flex flex-col gap-3 justify-between">
          <FormField
            label="Expiry Month"
            id="expiry month"
            type="text"
            placeHolder="MM"
            {...register("paymentInfo.expireMonth")}
          />
          <FormField
            label="Expiry Year"
            id="expiry year"
            type="text"
            placeHolder="YY"
            {...register("paymentInfo.expireYear")}
          />
          <FormField label="CVC" id="cvc" type="text" placeHolder="123" {...register("paymentInfo.cvc")} />
        </div>
        <FormField
          label="Name on Card"
          id="name"
          type="text"
          placeHolder="e.g. John Doe"
          {...register("paymentInfo.cardHolderName")}
        />
      </div>
    </CheckoutCart>
  );
}
