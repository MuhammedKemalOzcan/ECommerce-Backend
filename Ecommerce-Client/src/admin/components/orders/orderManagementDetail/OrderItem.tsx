interface OrderItemProps {
    name: string;
    qty: number;
    price: number;
    img: string;
}

export default function OrderItem({ name, qty, price, img }: OrderItemProps) {
    return (
        <div className="p-6 flex items-center gap-4 hover:bg-gray-50/50 transition cursor-default">
            <img
                src={img}
                alt={name}
                className="w-16 h-16 rounded-lg object-cover bg-gray-100"
            />
            <div className="flex-1">
                <h4 className="font-bold text-sm leading-tight">{name}</h4>
                <div className="flex gap-4 mt-2">
                    <span className="text-xs text-gray-500">Qty: {qty}</span>
                </div>
            </div>
            <div className="text-right">
                <span className="font-black text-slate-800">${price}</span>
            </div>
        </div>
    )
}