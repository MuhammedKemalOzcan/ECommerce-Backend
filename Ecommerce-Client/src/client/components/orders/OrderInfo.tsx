import type { Order } from "../../../types/Order";

interface infoProps {
  order: Order
  };

export default function OrderInfo({ order }: infoProps) {
  return (
    <div className="lg:flex gap-12 w-full">
      <div>
        <p className="text-red-500 text-sm">ORDER PLACED</p>
        <p className="text-sm font-bold">
          {new Date(order.orderDate).toLocaleDateString()}
        </p>
      </div>
      <div>
        <p className="text-red-500 text-sm">TOTAL</p>
        <p className="text-sm font-bold">${order.grandTotal}</p>
      </div>
      <div>
        <p className="text-red-500 text-sm">ORDER</p>
        <p className="text-sm font-bold">{order.orderCode}</p>
      </div>
    </div>
  );
}
