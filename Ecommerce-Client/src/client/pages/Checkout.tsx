import ShippingDetails from "../components/checkout/ShippingDetails";
import DeliveryMethod from "../components/checkout/DeliveryMethod";
import OrderSummary from "../components/checkout/OrderSummary";
import { FormProvider, useForm, type SubmitHandler } from "react-hook-form";
import type { CreateOrder } from "../../types/Order";
import { useOrderStore } from "../../stores/orderStore";
import { useState } from "react";
import { useCartStore } from "../../stores/cartStore";
import IyzicoPaymentForm from "../components/payment/PaymentForm";
import { toast } from "react-toastify";

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
      // paymentInfo: {
      //   cardNumber: "",
      //   expireMonth: "",
      //   expireYear: "",
      //   cvc: "",
      //   cardHolderName: "",
      // },
    },
  });
  const { handleSubmit } = methods;
  const createOrder = useOrderStore((state) => state.createOrder);
  const [paymentHtml, setPaymentHtml] = useState<string | null>(null);

  const onSubmit: SubmitHandler<CreateOrder> = async (data: CreateOrder) => {
    const payload = { ...data };
    payload.billingAddress = { ...payload.shippingAddress };
    try {
      const htmlContent = await createOrder(payload);

      if (htmlContent) {
        setPaymentHtml(htmlContent);
        setTimeout(() => {
          document
            .getElementById("iyzipay-checkout-form")
            ?.scrollIntoView({ behavior: "smooth" });
        }, 500);
      }
    } catch (error) {
      console.error("Ödeme başlatılamadı", error);
      toast.error("Ödeme formu oluşturulamadı.");
    }
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
            </div>

            {paymentHtml && (
              <div className="fixed inset-0 z-50 bg-black/50 backdrop-blur-md">
                <IyzicoPaymentForm htmlContent={paymentHtml} />
              </div>
            )}

            <div className="w-full lg:w-[350px] sticky top-10 h-fit flex-shrink-0">
              <OrderSummary />
            </div>
          </form>
        </FormProvider>
      </div>
    </div>
  );
}
