import { ArrowLeft } from "lucide-react";
import { useNavigate, useParams } from "react-router-dom";
import { useOrderStore } from "../../../stores/orderStore";
import { useShallow } from "zustand/shallow";
import { useEffect } from "react";
import Loading from "../../../client/components/common/Loading";
import { baseApiUrl } from "../../../constants/apiUrl";
import { Cancel } from "@mui/icons-material";
import OrderItem from "./orderManagementDetail/OrderItem";
import OrderProgress from "./orderManagementDetail/OrderProgress";
import OrderSummaryCard from "./orderManagementDetail/OrderSummaryCard";
import AddressCard from "./orderManagementDetail/AddressCard";

const OrderManagementDetail = () => {
  const navigate = useNavigate();
  const { id } = useParams();

  const { order, getOrder, isLoading, deliverOrder, shipOrder } = useOrderStore(
    useShallow((state) => ({
      order: state.order,
      getOrder: state.getOneOrder,
      isLoading: state.isLoading,
      deliverOrder: state.deliverOrder,
      shipOrder: state.shipOrder,
    })),
  );

  useEffect(() => {
    if (!id) return;
    getOrder(id);
  }, [getOrder]);

  const currentStatusKey = String(order?.deliveryStatus);

  if (isLoading && !order) return <Loading />;

  return (
    <div className="min-h-screen bg-gray-50 p-4 md:p-8 font-sans text-slate-700">
      {/* Top Navigation */}
      <div className="max-w-6xl mx-auto flex justify-between items-center mb-6">
        <div className="flex items-center gap-4">
          <button
            onClick={() => navigate(-1)}
            className="flex items-center text-gray-500 hover:text-gray-700 transition"
          >
            <ArrowLeft size={20} className="mr-2" />
            <span className="hidden sm:inline">Back to Orders</span>
          </button>
          <div className="flex items-center gap-3">
            <h1 className="text-xl font-bold">{order?.orderCode}</h1>
            {order?.deliveryStatus === "2" && (
              <span className="bg-orange-100 text-orange-600 px-3 py-1 rounded-full text-xs font-bold uppercase tracking-wider">
                In Transit
              </span>
            )}
          </div>
        </div>
      </div>

      <div className="max-w-6xl mx-auto grid grid-cols-1 lg:grid-cols-3 gap-8">
        {/* Left Column: Status and Items */}
        <div className="lg:col-span-2 space-y-6">
          {/* Progress Bar Card */}
          <div className="bg-white rounded-xl p-8 border border-gray-100 shadow-sm relative overflow-hidden">
            {currentStatusKey === "4" ? (
              // İptal Durumu İçin Özel Görünüm
              <div className="flex items-center justify-center gap-3 text-red-600 bg-red-50 p-4 rounded-lg">
                <Cancel />
                <span className="font-bold">This order has been canceled.</span>
              </div>
            ) : (
              // Normal Stepper Akışı
              <OrderProgress
                status={String(order?.deliveryStatus)}
                shippedDate={order?.shippedDate}
                deliveredDate={order?.deliveredDate}
                orderDate={order?.orderDate} />
            )}
          </div>

          {/* Items List Card */}
          <div className="bg-white rounded-xl border border-gray-100 shadow-sm overflow-hidden">
            <div className="p-6 border-b border-gray-50">
              <h2 className="font-bold text-lg">
                Items List ({order?.orderItems.length})
              </h2>
            </div>
            <div className="divide-y divide-gray-50">
              {order?.orderItems.map((product) => (
                <OrderItem
                  name={product.productName}
                  qty={product.quantity}
                  price={product.price}
                  img={`${baseApiUrl}/${product.imageUrl}`}
                />
              ))}
            </div>
          </div>
          <div className="flex justify-end gap-2">
            {order?.deliveryStatus == "1" && (
              <div className="flex gap-2">
                <button
                  onClick={() => shipOrder(order.id)}
                  className="bg-orange-400 text-white px-4 py-2 rounded-lg"
                >
                  Ship Order
                </button>
                <button
                  onClick={() => cancelOrder(order.id)}
                  className="bg-red-400 text-white px-4 py-2 rounded-lg"
                >
                  Cancel Order
                </button>
              </div>
            )}
            {order?.deliveryStatus == "2" && (
              <button
                onClick={() => deliverOrder(order.id)}
                className="bg-orange-400 text-white px-4 py-2 rounded-lg"
              >
                Deliver Order
              </button>
            )}
          </div>
        </div>
        {/* Right Column: Customer & Summary */}
        <div className="space-y-6">
          {/* Customer Details */}
          <div className="bg-white rounded-xl p-6 border border-gray-100 shadow-sm">
            <h3 className="text-xs font-bold text-gray-400 uppercase tracking-widest mb-4">
              Customer Details
            </h3>
            <div className="flex items-center gap-4">
              <div className="bg-orange-100 text-orange-600 font-bold w-12 h-12 flex items-center justify-center rounded-full">
                {order?.customer.firstName.substring(0, 1)}
                {order?.customer.lastName.substring(0, 1)}
              </div>
              <div>
                <p className="font-bold">
                  {order?.customer.firstName} {order?.customer.lastName}
                </p>
                <p className="text-sm text-gray-500 text-ellipsis overflow-hidden">
                  {order?.customer.email}
                </p>
              </div>
            </div>
          </div>

          {/* Shipping Address */}
          {order && <AddressCard order={order} />}
          {order && <OrderSummaryCard order={order} />}
        </div>
      </div>
    </div >
  );
};





export default OrderManagementDetail;
