import { useForm } from "react-hook-form";
import FormField from "../../../admin/components/products/FormField";
import { useCustomerStore } from "../../../stores/customerStore";
import CheckoutCart from "./CheckoutCart";
import { useEffect } from "react";

export default function ShippingDetails() {
  const customer = useCustomerStore((state) => state.customer);

  console.log(customer);

  const addresses = customer?.addresses?.find((a) => a.isPrimary === true);
  console.log(addresses);

  const { register, reset } = useForm({
    defaultValues: {
      firstName: "",
      lastName: "",
      street: "",
      city: "",
      zipCode: "",
      country: "",
    },
  });

  useEffect(() => {
    if (customer) {
      reset({
        firstName: customer?.firstName,
        lastName: customer?.lastName,
        street: addresses?.street,
        city: addresses?.city,
        zipCode: addresses?.zipCode,
        country: addresses?.country,
      });
    }
  }, [customer, reset]);

  return (
    <CheckoutCart title="Shipping Details" count="1">
      <div className="flex justify-between gap-3">
        <FormField
          id="firstName"
          label="First Name"
          placeHolder="e.g. John"
          type="text"
          required
          {...register("firstName")}
        />
        <FormField
          id="lastName"
          label="Last Name"
          placeHolder="e.g. Doe"
          type="text"
          required
          {...register("lastName")}
        />
      </div>
      <FormField
        id="street"
        label="Street"
        placeHolder="1234 Main Street"
        type="text"
        required
        {...register("street")}
      />
      <div className="flex justify-between">
        <FormField
          id="city"
          label="City"
          placeHolder="Istanbul"
          type="text"
          required
          {...register("city")}
        />
        <FormField
          id="zipCode"
          label="Zip Code"
          placeHolder="10000"
          type="text"
          required
          {...register("zipCode")}
        />
      </div>
      <FormField
        id="country"
        label="Country"
        placeHolder="Turkey"
        type="text"
        required
        {...register("country")}
      />
    </CheckoutCart>
  );
}
