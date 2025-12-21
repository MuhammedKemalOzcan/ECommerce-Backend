import { useForm } from "react-hook-form";
import FormField from "../../../admin/components/products/FormField";
import { useCustomerStore } from "../../../stores/customerStore";
import CheckoutCart from "./CheckoutCart";

export default function ShippingDetails() {
  const customer = useCustomerStore((state) => state.customer);

  console.log(customer);

  const { register } = useForm({
    defaultValues: {
      firstName: customer?.firstName || "",
      lastName: customer?.lastName || "",
      street: "",
      city: "",
      postalCode: "",
      country: "",
    },
  });

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
      />
      <div className="flex justify-between">
        <FormField
          id="city"
          label="City"
          placeHolder="Istanbul"
          type="text"
          required
        />
        <FormField
          id="postalCode"
          label="Postal Code"
          placeHolder="10000"
          type="text"
          required
        />
      </div>
      <FormField
        id="country"
        label="Country"
        placeHolder="Turkey"
        type="text"
        required
      />
    </CheckoutCart>
  );
}
