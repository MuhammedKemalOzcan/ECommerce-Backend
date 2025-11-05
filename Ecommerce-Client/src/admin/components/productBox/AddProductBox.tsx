import { PlusCircleIcon } from "lucide-react";
import { useState } from "react";
import ProductBoxForm from "./ProductBoxForm";
import { useProductBoxStore } from "../../../stores/ProductBoxStore";
import type { ProductBoxes } from "../../../types/ProductBox";

type AddItemProps = {
  productId: string | null;
};

export default function AddProductBox({ productId }: AddItemProps) {
  const [isCreating, setIsCreating] = useState<boolean>(false);

  const createBoxItem = useProductBoxStore((s) => s.createBoxItem);

  const handleSubmit = (data: ProductBoxes, productId: string | null) => {
    createBoxItem(productId, data);
    setIsCreating(false);
  };

  const handleCancel = () => {
    setIsCreating(false);
  };

  return (
    <div>
      {isCreating === false ? (
        <button
          onClick={() => setIsCreating(true)}
          className="flex items-center justify-center gap-2 p-2 rounded-xl border-2 border-dashed border-black mt-2 bg-white hover:bg-black hover:text-white"
        >
          <PlusCircleIcon size={20} />
          <p>Add New Item</p>
        </button>
      ) : (
        <ProductBoxForm
          onSubmit={(data) => handleSubmit(data, productId)}
          onCancel={handleCancel}
        />
      )}
    </div>
  );
}
