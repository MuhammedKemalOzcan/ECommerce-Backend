import { useFormContext } from "react-hook-form";
import FormField from "../../../admin/components/products/FormField";
import { useCustomerStore } from "../../../stores/customerStore";
import CheckoutCart from "./CheckoutCart";
import { useEffect } from "react";
import { useShallow } from "zustand/shallow";

export default function ShippingDetails() {
  const { customer, getCustomer } = useCustomerStore(
    useShallow((s) => ({
      getCustomer: s.getCustomer,
      customer: s.customer,
    }))
  );

  useEffect(() => {
    getCustomer();
  }, [getCustomer]);

  const addresses = customer?.addresses?.find((a) => a.isPrimary === true);

  const { register, setValue } = useFormContext();

  useEffect(() => {
    if (addresses?.isPrimary) {
      const loc = addresses;
      setValue("shippingAddress.street", loc.location.street, {
        shouldValidate: true,
        shouldDirty: true,
      });
      setValue("shippingAddress.city", loc.location.city, {
        shouldValidate: true,
        shouldDirty: true,
      });
      setValue("shippingAddress.country", loc.location.country, {
        shouldValidate: true,
        shouldDirty: true,
      });
      setValue("shippingAddress.zipCode", loc.location.zipCode, {
        shouldValidate: true,
        shouldDirty: true,
      });
    }
  }, [addresses, setValue]);

  return (
    <CheckoutCart title="Shipping Details" count="1">
      <div>
        <FormField
          id="street"
          label="Street"
          placeHolder="1234 Main Street"
          type="text"
          required
          {...register("shippingAddress.street")}
        />
        <div className="lg:flex-row flex flex-col justify-between gap-3">
          <FormField
            id="city"
            label="City"
            placeHolder="Istanbul"
            type="text"
            required
            {...register("shippingAddress.city")}
          />
          <FormField
            id="country"
            label="Country"
            placeHolder="Turkey"
            type="text"
            required
            {...register("shippingAddress.country")}
          />
          <FormField
            id="zipCode"
            label="Zip Code"
            placeHolder="10000"
            type="text"
            required
            {...register("shippingAddress.zipCode")}
          />
        </div>
        <input id="billingAsShipping" type="checkbox" />
        <label htmlFor="billingAsShipping">test</label>
      </div>
    </CheckoutCart>
  );
}
