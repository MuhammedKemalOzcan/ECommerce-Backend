interface infoProps {
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

export default function OrderInfo({ order }: infoProps) {
  return (
    <div className="lg:flex gap-12 w-full">
      <div>
        <p className="text-red-500 text-sm">ORDER PLACED</p>
        <p className="text-sm font-bold">
          {new Date(order.date).toLocaleDateString()}
        </p>
      </div>
      <div>
        <p className="text-red-500 text-sm">TOTAL</p>
        <p className="text-sm font-bold">${order.totalAmount}</p>
      </div>
      <div>
        <p className="text-red-500 text-sm">ORDER</p>
        <p className="text-sm font-bold">{order.orderCode}</p>
      </div>
    </div>
  );
}
