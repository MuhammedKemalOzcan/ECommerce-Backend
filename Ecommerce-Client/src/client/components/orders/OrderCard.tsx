import OrderInfo from "./OrderInfo";
import OrderStatus from "./OrderStatus";
import { statusConfig } from "../../../utils/statusConfig";
import OrderProductGallery from "./OrderProductGallery";
import { useNavigate } from "react-router-dom";
import type { Order } from "../../../types/Order";
import { useCartStore } from "../../../stores/cartStore";

interface CardProps {
  order: Order;
}

export default function OrderCard({ order }: CardProps) {
  const navigate = useNavigate();

  const currentConfig = statusConfig[order.deliveryStatus];
  return (
    <div className="border flex flex-col bg-gray-300 rounded-2xl shadow-lg">
      <div className="lg:flex lg:gap-8 p-6 flex justify-between">
        <OrderInfo order={order} />
        <button
          onClick={() => navigate(`${order.id}`)}
          className="text-[#D87D4A] whitespace-nowrap"
        >
          View Details
        </button>
      </div>
      <div className="lg:flex-row lg:justify-between flex flex-col gap-8 p-6 bg-white rounded-b-2xl">
        <OrderStatus
          currentConfig={currentConfig}
          status={currentConfig.title}
        />

        <div className="flex flex-col gap-12">
          <OrderProductGallery products={order.orderItems} />

          <button className="bg-[#D87D4A] text-white p-2 shadow-md rounded-xl w-40 place-self-end">
            Buy Again
          </button>
        </div>
      </div>
    </div>
  );
}
