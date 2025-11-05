import type { ProductBoxes } from "../../../types/ProductBox";
import { useForm, type SubmitHandler } from "react-hook-form";
import { Send, X } from "lucide-react";
import FormField from "../products/FormField";
import { PRODUCT_VALIDATION_RULES } from "../../../schemes/ProductSchema";

type FormProps = {
  onSubmit: (data: ProductBoxes) => void | Promise<void>;
  defaultValues?: ProductBoxes;
  onCancel: () => void;
};

export default function ProductBoxForm({
  onSubmit,
  defaultValues,
  onCancel,
}: FormProps) {
  const { register, handleSubmit } = useForm<ProductBoxes>({
    defaultValues: defaultValues || { name: "", quantity: 1 },
  });

  const handleFormSubmit: SubmitHandler<ProductBoxes> = async (data) => {
    await onSubmit(data);
  };
  return (
    <form onSubmit={handleSubmit(handleFormSubmit)} className="flex flex-col bg-gray-300 p-4 rounded-lg">
      <FormField
        id="name"
        label="Name"
        placeHolder="USB-C Cable"
        type="text"
        {...register("name", PRODUCT_VALIDATION_RULES.boxName)}
      />
      <FormField
        id="quantity"
        label="Quantity"
        placeHolder="1"
        // error={errors.name}
        type="number"
        {...register("quantity", PRODUCT_VALIDATION_RULES.quantity)}
      />
      <div className="flex items-center gap-2">
        <button
          type="submit"
          className="flex  items-center text-white gap-2 p-1 rounded-lg mt-1 h-8 bg-blue-700 w-[50%]"
        >
          <Send size={20} />
          <p>Confirm</p>
        </button>
        <button
          onClick={onCancel}
          className="flex items-center text-white gap-2 p-1 rounded-lg mt-1 bg-red-700 h-8 w-[50%]"
          type="button"
        >
          <X size={28} />
          <p>Cancel</p>
        </button>
      </div>
    </form>
  );
}
