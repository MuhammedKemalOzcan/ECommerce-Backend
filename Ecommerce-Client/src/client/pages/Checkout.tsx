import ShippingDetails from "../components/checkout/ShippingDetails";
import DeliveryMethod from "../components/checkout/DeliveryMethod";
import Payment from "../components/checkout/Payment";
import OrderSummary from "../components/checkout/OrderSummary";

export default function Checkout() {
  return (
    <div className="w-full min-h-screen  py-10 lg:py-20">
      <div className="max-w-[1100px] mx-auto px-5">
        <div className="flex flex-col lg:flex-row gap-12 items-start relative">
          <div className="w-full lg:flex-1 flex flex-col gap-6">
            <ShippingDetails />
            <DeliveryMethod />
            <Payment />
          </div>

          <div className="w-full lg:w-[350px] sticky top-10 h-fit flex-shrink-0">
            <OrderSummary />
          </div>
        </div>
      </div>
    </div>
  );
}
