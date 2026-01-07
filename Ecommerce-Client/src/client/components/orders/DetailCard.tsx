import LocalShippingOutlinedIcon from "@mui/icons-material/LocalShippingOutlined";
import CreditCardIcon from "@mui/icons-material/CreditCard";
import type { Order } from "../../../types/Order";

interface DetailCardProps {
  order: Order;
  type: "shipping" | "payment";
}

const ShippingInfo = ({ order }: { order: Order }) => (
  <div className="border rounded-2xl shadow-sm p-6 flex flex-col gap-4 bg-white h-full">
    <div className="flex items-center gap-3 border-b pb-4">
      <div className="rounded-full bg-blue-50 p-2 text-blue-600">
        <LocalShippingOutlinedIcon fontSize="small" />
      </div>
      <h2 className="font-bold text-gray-800">Delivery Details</h2>
    </div>
    <div className="text-sm space-y-2">
      <p className="font-bold text-gray-900">{order.customerName}</p>
      <p className="text-gray-500">{order.shippingAddress}</p>
      <p className="text-gray-500">{order.phoneNumber}</p>
    </div>
  </div>
);

const PaymentInfo = ({ order }: { order: Order }) => (
  <div className="border rounded-2xl shadow-sm p-6 flex flex-col gap-4 bg-white h-full">
    <div className="flex items-center gap-3 border-b pb-4">
      <div className="rounded-full bg-purple-50 p-2 text-purple-600">
        <CreditCardIcon fontSize="small" />
      </div>
      <h2 className="font-bold text-gray-800">Payment Method</h2>
    </div>
    <div className="space-y-3">
      {order.cart.map((card, index) => (
        <div key={index} className="flex flex-col text-sm text-gray-600">
          <p className="font-bold text-gray-900">
            **** **** **** {card.cartNumber.slice(-4)}
          </p>
          <p>{card.name.toUpperCase()}</p>
          <p className="text-xs text-gray-400">Exp: {card.lastDate}</p>
        </div>
      ))}
    </div>
  </div>
);

export default function DetailCard({ order, type }: DetailCardProps) {
  return type === "shipping" ? (
    <ShippingInfo order={order} />
  ) : (
    <PaymentInfo order={order} />
  );
}
