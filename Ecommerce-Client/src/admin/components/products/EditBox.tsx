import { Send, X } from "lucide-react";
import { useProductBoxStore } from "../../../stores/ProductBoxStore";
import { useForm, type SubmitHandler } from "react-hook-form";
import type { ProductBoxes } from "../../../types/ProductBox";
import { useShallow } from "zustand/shallow";

type Props = {
  setBoxItemId: React.Dispatch<React.SetStateAction<string | null>>;
  defaultValues: ProductBoxes;
  boxId: string | null;
  productId: string | null;
};

export default function EditBox({
  setBoxItemId,
  defaultValues,
  boxId,
  productId,
}: Props) {
  const { updateBoxItem, deleteBoxItem } = useProductBoxStore(
    useShallow((state) => ({
      updateBoxItem: state.updateBoxItems,
      deleteBoxItem: state.deleteBoxItem,
    }))
  );

  const { register, handleSubmit } = useForm<ProductBoxes>({
    defaultValues: defaultValues,
  });

  const handleFormSubmit: SubmitHandler<ProductBoxes> = async (data) => {
    await updateBoxItem(boxId, data);
    setBoxItemId(null);
  };

  return (
    <form onSubmit={handleSubmit(handleFormSubmit)} className="flex flex-col">
      <label htmlFor="">Name:</label>
      <input {...register("name", { required: true })} className="input h-8" />
      <label htmlFor="">Quantity:</label>
      <input
        {...register("quantity", { valueAsNumber: true, min: 1 })}
        type="number"
        className="input h-8"
      />
      <div className="flex items-center">
        <button type="submit">
          <Send size={20} />
        </button>
        <button>
          <X
            onClick={() => setBoxItemId(null)}
            className="flex"
            size={28}
            color="red"
          />
        </button>
      </div>
    </form>
  );
}
