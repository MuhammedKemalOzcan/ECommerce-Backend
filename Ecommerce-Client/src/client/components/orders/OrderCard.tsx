import OrderInfo from "./OrderInfo";
import OrderStatus from "./OrderStatus";
import { statusConfig } from "../../../utils/statusConfig";
import OrderProductGallery from "./OrderProductGallery";

interface CardProps {
  order: {
    id: string;
    orderCode: string;
    userId: string;
    date: string;
    status: string;
    totalAmount: number;
    currency: string;
    products: {
      productId: string;
      name: string;
      image: string;
      price: number;
      quantity: number;
    }[];
  };
}

export default function OrderCard({ order }: CardProps) {
  const currentConfig = statusConfig[order.status] || statusConfig.shipped;
  return (
    <div className="border flex flex-col bg-gray-300 rounded-2xl shadow-lg">
      <div className="lg:flex lg:gap-8 p-6 flex justify-between">
        <OrderInfo order={order} />
        <button className="text-[#D87D4A] whitespace-nowrap">
          View Details
        </button>
      </div>
      <div className="lg:flex-row lg:justify-between flex flex-col gap-8 p-6 bg-white rounded-b-2xl">
        <OrderStatus currentConfig={currentConfig} status={order.status} />
        <div className="flex flex-col gap-12">
          <OrderProductGallery products={order.products} />
          <button className="bg-[#D87D4A] text-white p-2 shadow-md rounded-xl w-40 place-self-end">
            Buy Again
          </button>
        </div>
      </div>
    </div>
  );
}
