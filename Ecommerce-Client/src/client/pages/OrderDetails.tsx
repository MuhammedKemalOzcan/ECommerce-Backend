import { ArrowLeft } from "lucide-react";
import { useParams, useNavigate } from "react-router-dom";
import OrderStatus from "../components/orders/OrderStatus";
import { statusConfig } from "../../utils/statusConfig";
import DetailCard from "../components/orders/DetailCard";
import OrderDetailsCard from "../components/orders/OrderDetailsCard";
import { useOrderStore } from "../../stores/orderStore";
import { useShallow } from "zustand/shallow";
import { useEffect } from "react";

export default function OrderDetails() {
  const { id } = useParams();
  const navigate = useNavigate();

  const { order, getOneOrder } = useOrderStore(
    useShallow((state) => ({
      order: state.order,
      getOneOrder: state.getOneOrder,
    })),
  );

  useEffect(() => {
    if (!id) return;
    getOneOrder(id);
  }, [getOneOrder]);

  if (!order) return <div className="p-10 text-center">Order not found!</div>;

  const currentConfig = statusConfig[order.deliveryStatus];
  console.log(order.paymentInfo);

  return (
    <div className="mx-auto p-6 flex flex-col gap-8 w-full">
      {/* Back Button */}
      <button
        onClick={() => navigate(-1)}
        className="flex items-center gap-2 text-gray-500 hover:text-[#D87D4A] transition-colors w-fit"
      >
        <ArrowLeft size={20} />
        <span className="font-medium">Go Back</span>
      </button>

      {/* Header Area */}
      <div className="flex flex-col md:flex-row justify-between items-start md:items-center gap-4">
        <div>
          <div className="flex items-baseline gap-3">
            <h1 className="text-3xl font-bold text-gray-900">Order Details</h1>
            <span className="text-xl text-gray-400 font-medium">
              #{order.orderCode}
            </span>
          </div>
          <p className="text-sm text-gray-500 mt-1">
            Ordered on{" "}
            {new Date(order.orderDate).toLocaleDateString("tr-TR", {
              year: "numeric",
              month: "long",
              day: "numeric",
              hour: "2-digit",
              minute: "2-digit",
            })}
          </p>
        </div>
        <OrderStatus
          currentConfig={currentConfig}
          status={currentConfig.title}
        />
      </div>

      <div className="flex flex-col lg:flex gap-8 items-start">
        <div className="flex flex-col gap-8 flex-1 w-full">
          <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
            <DetailCard order={order} type="shipping" />
            <DetailCard order={order} type="payment" />
          </div>

          <OrderDetailsCard currentOrder={order} />
        </div>

        <div className="w-full lg:w-[350px] lg:sticky lg:top-6"></div>
      </div>
    </div>
  );
}
