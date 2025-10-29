import { Package } from "lucide-react";
import type { Products } from "../../../types/Products";

type Props = {
  product: Products;
};

export default function ExpandedRow({ product }: Props) {
  return (
    <div className="bg-gray-200 rounded-lg w-full shadow-sm flex flex-col gap-8 p-8">
      <div className="flex text-[20px] text-blue-700 gap-2">
        <Package size={24} />
        <h3>Ürün İçeriği</h3>
      </div>
      <div className="flex gap-20">
        <div className="flex flex-col">
          <h3>Ürün Adı:</h3>
          <p>{product.name}</p>
        </div>
        <div>
          <h3>Kutu İçerisinde:</h3>
          {product.productBoxes?.map((bx, index) => (
            <div key={index} className="flex gap-2">
              <p>{bx.quantity}x</p>
              <p>{bx.name}</p>
            </div>
          ))}
        </div>
        <div className="flex flex-col">
          <h3>Ürün Boyutu:</h3>
          <p>20x30x10</p>
        </div>
        <div className="flex flex-col">
          <h3>Ürün Ağırlığı:</h3>
          <p>5 kg</p>
        </div>
      </div>
    </div>
  );
}
