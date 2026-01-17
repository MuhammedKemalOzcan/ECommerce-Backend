import ShippingDetails from "../components/checkout/ShippingDetails";
import DeliveryMethod from "../components/checkout/DeliveryMethod";
import Payment from "../components/checkout/Payment";
import OrderSummary from "../components/checkout/OrderSummary";
import { FormProvider, useForm, type SubmitHandler } from "react-hook-form";
import type { CreateOrder } from "../../types/Order";
import { useOrderStore } from "../../stores/orderStore";
import ConfirmationModal from "../../admin/components/common/ConfirmationModal";
import { useState } from "react";
import { useCartStore } from "../../stores/cartStore";

export default function Checkout() {
  const methods = useForm<CreateOrder>({
    defaultValues: {
      shippingCost: 0,
      installment: 1,
      shippingAddress: {
        street: "",
        city: "",
        country: "",
        zipCode: "",
      },
      billingAddress: {
        street: "",
        city: "",
        country: "",
        zipCode: "",
      },
      paymentInfo: {
        cardNumber: "",
        expireMonth: "",
        expireYear: "",
        cvc: "",
        cardHolderName: "",
      },
    },
  });
  const { handleSubmit } = methods;
  const createOrder = useOrderStore((state) => state.createOrder);
  const clearCart = useCartStore((state) => state.clearCart);

  const onSubmit: SubmitHandler<CreateOrder> = async (data: CreateOrder) => {
    const payload = { ...data };
    payload.billingAddress = { ...payload.shippingAddress };
    const response = await createOrder(payload);
    if (response) clearCart();
    console.log(response);
  };

  return (
    <div className="w-full py-10 lg:py-20">
      <div className="lg:max-w-[1100px] mx-auto px-5">
        <FormProvider {...methods}>
          <form
            onSubmit={handleSubmit(onSubmit)}
            className="flex flex-col lg:flex-row gap-12 items-start relative"
          >
            <div className="w-full lg:flex-1 flex flex-col gap-6">
              <ShippingDetails />
              <DeliveryMethod />
              <Payment />
            </div>

            <div className="w-full lg:w-[350px] sticky top-10 h-fit flex-shrink-0">
              <OrderSummary />
            </div>
          </form>
        </FormProvider>
      </div>
    </div>
  );
}
