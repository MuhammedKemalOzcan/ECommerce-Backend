import { Package, PenBoxIcon, X } from "lucide-react";
import type { Products } from "../../../types/Products";
import { useEffect, useState } from "react";
import EditBox from "./EditBox";
import { useProductBoxStore } from "../../../stores/ProductBoxStore";
import { useShallow } from "zustand/shallow";

type Props = {
  product: Products;
};

export default function ExpandedRow({ product }: Props) {
  const [boxItemId, setBoxItemId] = useState<string | null>(null);

  const { getAllBoxes, productBoxes } = useProductBoxStore(
    useShallow((state) => ({
      productBoxes: state.productBoxes,
      getAllBoxes: state.getAllBoxes,
    }))
  );

  useEffect(() => {
    getAllBoxes(product.id);
  }, [getAllBoxes]);

  const handleClick = (id: string | null) => {
    setBoxItemId(boxItemId ? null : id);
  };

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
          {productBoxes.map((bx) => (
            <div key={bx.id} className="flex gap-4 relative">
              {boxItemId === bx.id ? (
                <EditBox
                  boxId={bx.id}
                  defaultValues={bx}
                  setBoxItemId={setBoxItemId}
                />
              ) : (
                <div className="flex gap-3">
                  <p className="text-orange-500">{bx.quantity}x</p>
                  <p>{bx.name}</p>
                </div>
              )}
              <button className="flex" onClick={() => handleClick(bx.id)}>
                {boxItemId === bx.id ? (
                  <X
                    className="absolute bottom-0 left-8"
                    size={26}
                    color="red"
                  />
                ) : (
                  <PenBoxIcon className="" size={16} color="blue" />
                )}
              </button>
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
