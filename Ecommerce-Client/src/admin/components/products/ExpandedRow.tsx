import { Package, PenBoxIcon, Trash2 } from "lucide-react";
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

  const { getAllBoxes, productBoxes, deleteBoxItem } = useProductBoxStore(
    useShallow((state) => ({
      productBoxes: state.productBoxes,
      getAllBoxes: state.getAllBoxes,
      deleteBoxItem: state.deleteBoxItem,
    }))
  );

  useEffect(() => {
    getAllBoxes(product.id);
  }, [getAllBoxes]);

  const handleEditBox = (id: string | null) => {
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
            <div key={bx.id} className="flex gap-4 items-center">
              {boxItemId === bx.id ? (
                <EditBox
                  productId={product.id}
                  boxId={bx.id}
                  defaultValues={bx}
                  setBoxItemId={setBoxItemId}
                />
              ) : (
                <div className="flex gap-3">
                  <div className="flex gap-1 w-[120px]">
                    <p className="text-orange-500">{bx.quantity}x</p>
                    <p className="whitespace-nowrap">{bx.name}</p>
                  </div>
                  <button
                    onClick={() => handleEditBox(bx.id)}
                    className="justify-self-end"
                  >
                    <PenBoxIcon size={16} color="blue" />
                  </button>
                  <button
                    onClick={() => deleteBoxItem(bx.id)}
                    className="justify-self-end"
                  >
                    <Trash2 size={16} color="red" className="hover:bg-red-50" />
                  </button>
                </div>
              )}
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}
