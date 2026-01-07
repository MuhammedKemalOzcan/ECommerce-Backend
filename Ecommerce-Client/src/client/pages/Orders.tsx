import ordersData from "../../data/orders.json";
import OrderCard from "../components/orders/OrderCard";

export default function Orders() {
  return (
    <div className="p-6 flex flex-col w-full">
      <h1 className="text-[40px] mb-4">My Orders</h1>
      <div className="flex flex-col gap-12">
        {ordersData.orders.map((order) => (
          <OrderCard order={order} />
        ))}
      </div>
    </div>
  );
}
