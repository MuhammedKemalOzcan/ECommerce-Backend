import { Download } from "lucide-react";
import type { Order } from "../../../../types/Order";

export default function OrderSummaryCard({ order }: { order: Order }) {
    const taxAmount = order?.subTotal * 0.2;
    return (
        <div className="bg-white rounded-xl p-6 border border-gray-100 shadow-sm">
            <h3 className="text-xs font-bold text-gray-400 uppercase tracking-widest mb-4">
                Order Summary
            </h3>
            <div className="space-y-3 text-sm border-b border-gray-50 pb-4">
                <div className="flex justify-between text-gray-500">
                    <span>Subtotal</span>
                    <span className="font-bold text-slate-700">
                        ${order?.subTotal}
                    </span>
                </div>
                <div className="flex justify-between text-gray-500">
                    <span>Shipping</span>
                    <span className="text-green-500 font-bold">
                        ${order?.shippingCost}
                    </span>
                </div>
                <div className="flex justify-between text-gray-500">
                    <span>Estimated Tax</span>
                    <span className="font-bold text-slate-700">
                        ${taxAmount.toFixed(1)}
                    </span>
                </div>
            </div>
            <div className="flex justify-between items-center mt-4">
                <span className="font-bold text-lg">Total</span>
                <span className="text-2xl font-black text-orange-500">
                    ${order?.grandTotal}
                </span>
            </div>

            <div className="mt-6 space-y-3">
                <button className="w-full bg-orange-500 hover:bg-orange-600 text-white font-bold py-3 rounded-xl flex items-center justify-center gap-2 transition shadow-lg shadow-orange-200">
                    <Download size={18} />
                    Download Invoice
                </button>
            </div>
        </div>
    )
}