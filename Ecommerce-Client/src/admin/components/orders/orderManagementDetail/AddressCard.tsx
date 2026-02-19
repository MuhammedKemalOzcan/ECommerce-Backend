import { MapPin, Phone } from "lucide-react";
import type { Order } from "../../../../types/Order";

export default function AddressCard({ order }: { order: Order }) {
    return (
        <div className="bg-white rounded-xl p-6 border border-gray-100 shadow-sm relative">
            <MapPin
                className="absolute top-6 right-6 text-orange-400 opacity-50"
                size={20}
            />
            <h3 className="text-xs font-bold text-gray-400 uppercase tracking-widest mb-4">
                Shipping Address
            </h3>
            <div className="space-y-4">
                <div>
                    <p className="text-sm text-gray-500 leading-relaxed">
                        {order?.shippingAddress.street}
                    </p>
                    <p className="text-sm text-gray-500 leading-relaxed">
                        {order?.shippingAddress.city}/{order?.shippingAddress.country}
                    </p>
                    <p className="text-sm text-gray-500 leading-relaxed">
                        {order?.shippingAddress.zipCode}
                    </p>
                </div>

                <div className="flex items-center gap-2 text-sm text-gray-600">
                    <Phone size={16} />
                    <span>{order?.customer.phoneNumber}</span>
                </div>
            </div>
        </div>
    )
}