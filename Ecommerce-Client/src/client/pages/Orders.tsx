import { useEffect } from "react";
import OrderCard from "../components/orders/OrderCard";
import { useOrderStore } from "../../stores/orderStore";
import { useShallow } from "zustand/shallow";

export default function Orders() {
  const { getOrders, orders } = useOrderStore(
    useShallow((state) => ({
      getOrders: state.getOrders,
      orders: state.orders,
    }))
  );

  useEffect(() => {
    getOrders();
  }, [getOrders]);
  return (
    <div className="p-6 flex flex-col w-full">
      <h1 className="text-[40px] mb-4">My Orders</h1>
      <div className="flex flex-col gap-12">
        {orders && orders.map((order) => (
          <OrderCard order={order} />
        ))}
      </div>
    </div>
  );
}
