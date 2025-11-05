import { Package } from "lucide-react";
import type { Products } from "../../../types/Products";
import { useEffect, useState } from "react";
import { useProductBoxStore } from "../../../stores/ProductBoxStore";
import { useShallow } from "zustand/shallow";
import ProductBoxForm from "../productBox/ProductBoxForm";
import type { ProductBoxes } from "../../../types/ProductBox";
import DisplayProductBox from "../productBox/DisplayProductBox";
import AddProductBox from "../productBox/AddProductBox";

type Props = {
  product: Products;
};

export default function ExpandedRow({ product }: Props) {
  const [boxItemId, setBoxItemId] = useState<string | null>(null);

  const { getAllBoxes, productBoxes, updateBoxItem } = useProductBoxStore(
    useShallow((state) => ({
      productBoxes: state.productBoxes,
      getAllBoxes: state.getAllBoxes,
      updateBoxItem: state.updateBoxItems,
    }))
  );

  useEffect(() => {
    getAllBoxes(product.id);
  }, [getAllBoxes, product.id]);

  const handleEditBox = (id: string | null) => {
    setBoxItemId(boxItemId ? null : id);
  };

  const handleUpdate = async (boxId: string, data: ProductBoxes) => {
    await updateBoxItem(boxId, data);
    setBoxItemId(null);
  };

  const handleCancelEdit = () => {
    setBoxItemId(null);
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
                <ProductBoxForm
                  onCancel={handleCancelEdit}
                  onSubmit={(data) => handleUpdate(bx.id, data)}
                  defaultValues={bx}
                />
              ) : (
                <DisplayProductBox box={bx} onEditBox={handleEditBox} />
              )}
            </div>
          ))}
          <AddProductBox productId={product.id} />
        </div>
      </div>
    </div>
  );
}
