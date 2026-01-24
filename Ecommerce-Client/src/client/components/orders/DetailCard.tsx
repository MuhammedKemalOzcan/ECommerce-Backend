import LocalShippingOutlinedIcon from "@mui/icons-material/LocalShippingOutlined";
import CreditCardIcon from "@mui/icons-material/CreditCard";
import type { Order, PaymentInfo } from "../../../types/Order";
import type { Location } from "../../../types/customer";
import { CARD_ASSOCIATION_CONFIG } from "../../../constants/cardAssociation";
import { useState } from "react";

interface DetailCardProps {
  order: Order;
  type: "shipping" | "payment";
}

const ShippingInfo = ({
  order,
  address,
}: {
  order: Order;
  address: Location;
}) => (
  <div className="border rounded-2xl shadow-sm p-6 flex flex-col gap-4 bg-white h-full">
    <div className="flex items-center gap-3 border-b pb-4">
      <div className="rounded-full bg-blue-50 p-2 text-blue-600">
        <LocalShippingOutlinedIcon fontSize="small" />
      </div>
      <h2 className="font-bold text-gray-800">Delivery Details</h2>
    </div>
    <div className="text-sm space-y-2">
      <p className="font-bold text-gray-900">
        {order.customer.firstName} {order.customer.lastName}
      </p>
      <p className="text-gray-500">
        {address.country} {address.street}
      </p>
      <p className="text-gray-500">{order.customer.phoneNumber}</p>
    </div>
  </div>
);

const PaymentInfo = ({ paymentInfo }: { paymentInfo: PaymentInfo }) => {
  const [loading, setLoading] = useState(false);

  if (!paymentInfo) setLoading(true);

  console.log("Gelen Veri:", paymentInfo.cardAssociation);
  console.log(
    "Bulunan Config:",
    CARD_ASSOCIATION_CONFIG[paymentInfo.cardAssociation],
  );
  const config = CARD_ASSOCIATION_CONFIG[paymentInfo.cardAssociation];
  const IconComponent = config.icon;

  if (loading === true) return <div>Loading...</div>;

  return (
    <div className="border rounded-2xl shadow-sm p-6 flex flex-col gap-4 bg-white h-full">
      <div className="flex items-center gap-3 border-b pb-4">
        <div className="rounded-full bg-purple-50 p-2 text-purple-600">
          <CreditCardIcon fontSize="small" />
        </div>
        <h2 className="font-bold text-gray-800">Payment Method</h2>
      </div>
      <div className="space-y-3">
        <div
          key={paymentInfo.cardHolderName}
          className="flex flex-col text-sm text-gray-600"
        >
          <p className="font-bold text-gray-900">
            **** **** **** {paymentInfo.cardLastFourDigits}
          </p>
          <p>{paymentInfo.cardHolderName.toUpperCase()}</p>
          <IconComponent color={config.color} size={40} />
        </div>
      </div>
    </div>
  );
};

export default function DetailCard({ order, type }: DetailCardProps) {
  console.log(order);

  return type === "shipping" ? (
    <ShippingInfo order={order} address={order.shippingAddress} />
  ) : (
    <PaymentInfo paymentInfo={order.paymentInfo} />
  );
}
