import type { Order } from "../../../types/Order";

export default function OrderDetailsCard({
  currentOrder,
}: {
  currentOrder: Order;
}) {
  return (
    <div className="border rounded-2xl bg-gray-50 overflow-hidden">
      <h1 className="p-6 font-bold text-lg border-b bg-gray-100">
        Items Ordered
      </h1>

      <div className="bg-white">
        {currentOrder.products.map((product, index) => (
          <div
            key={product.id}
            className={`p-6 flex gap-6 items-center ${
              index !== currentOrder.products.length - 1 ? "border-b" : ""
            }`}
          >
            <div className="bg-gray-100 p-3 rounded-lg size-24 flex-shrink-0">
              <img className="w-full h-full object-contain mix-blend-multiply" />
            </div>

            <div className="flex-1">
              <h3 className="font-bold text-gray-800">{product.name}</h3>
              <p className="text-sm text-gray-500 mt-1">
                Product Code: {product.id}
              </p>
            </div>

            <div className="text-right">
              <p className="font-bold text-lg">${product.price}</p>
              {/* Quantity ekle */}
              <p className="text-sm text-gray-500">Qty: {2}</p>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
