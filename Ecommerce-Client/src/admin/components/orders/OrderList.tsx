import { Eye } from "lucide-react";
import { useNavigate } from "react-router-dom";
import type { OrderInfo } from "../../../types/Order";
import { statusConfig } from "../../../utils/statusConfig";
import Loading from "../../../client/components/common/Loading";

interface OrderListProps {
  orders: OrderInfo | null;
  isLoading: boolean;
}

export default function OrderList({ orders, isLoading }: OrderListProps) {
  const navigate = useNavigate();

  if (isLoading) {
    return <Loading />;
  }

  const handleClick = (id: string | null) => {
    if (!id) return;
    navigate(`/admin/order-management/${id}`);
  };

  return (
    <div className="overflow-x-auto">
      <table className="w-full text-left border-collapse">
        <thead>
          <tr className="bg-gray-50 text-gray-600 uppercase text-xs font-semibold">
            <th className="px-6 py-4">ID</th>
            <th className="px-6 py-4">Müşteri</th>
            <th className="px-6 py-4">Ürün</th>
            <th className="px-6 py-4">Tarih</th>
            <th className="px-6 py-4 text-right">Tutar</th>
            <th className="px-6 py-4 text-center">Durum</th>
          </tr>
        </thead>
        <tbody className="divide-y divide-gray-100">
          {orders?.items.map((order, i) => {
            const currentConfig = statusConfig[order.deliveryStatus];
            return (
              <tr
                key={i}
                className="hover:bg-gray-50 transition-colors cursor-pointer"
              >
                <td className="px-6 py-4 font-medium text-blue-600">
                  {order.orderCode}
                </td>
                <td className="px-6 py-4 text-gray-700 font-medium">
                  {order.customer?.firstName}
                </td>

                <td className="px-6 py-4 text-gray-500 text-sm">
                  {order.orderItems[0].productName}
                  {order.orderItems.length > 1 && (
                    <span
                      className="text-blue-500 font-medium cursor-help"
                      title={order.orderItems
                        .slice(1)
                        .map((i) => i.productName)
                        .join(", ")}
                    >
                      {` (+${order.orderItems.length - 1} ürün)`}
                    </span>
                  )}
                </td>

                <td className="px-6 py-4 text-gray-500 text-sm">
                  {order.orderDate}
                </td>
                <td className="px-6 py-4 text-right font-bold text-gray-900">
                  {order.grandTotal}
                </td>
                <td className="px-6 py-4 text-center flex gap-2">
                  <span
                    className={`text-xs font-semibold uppercase ${currentConfig.text}`}
                  >
                    {currentConfig.icon}
                  </span>
                  <span
                    key={i}
                    className={`px-3 py-1 rounded-full text-xs font-semibold uppercase border ${currentConfig.bg} ${currentConfig.text}`}
                  >
                    {currentConfig.title}
                  </span>
                  <button onClick={() => handleClick(order.id)}>
                    <Eye />
                  </button>
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
}
