import { Send } from "lucide-react";
import { useProductBoxStore } from "../../../stores/ProductBoxStore";
import { useForm, type SubmitHandler } from "react-hook-form";
import type { ProductBoxes } from "../../../types/ProductBox";

type Props = {
  setBoxItemId: React.Dispatch<React.SetStateAction<string | null>>;
  defaultValues: ProductBoxes;
  boxId: string | null;
};

export default function EditBox({ setBoxItemId, defaultValues, boxId }: Props) {
  const updateBoxItem = useProductBoxStore((state) => state.updateBoxItems);
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
      <button className="mt-2 flex items-end " type="submit">
        <Send />
      </button>
    </form>
  );
}
