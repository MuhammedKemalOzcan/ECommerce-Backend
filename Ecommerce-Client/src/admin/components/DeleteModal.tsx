import { XCircle } from "lucide-react";
import { useProductStore } from "../../stores/productStore";
import { useSearchParams } from "react-router-dom";
import { useState } from "react";

type Props = {
  open: boolean;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
};

export default function DeleteModal({ open, setOpen }: Props) {
  const deleteProduct = useProductStore((s) => s.deleteProduct);
  const [deleting, setDeleting] = useState(false);

  const [searchParams] = useSearchParams();
  const id = searchParams.get("id");

  const onConfirm = async () => {
    try {
      setDeleting(true);
      if (id) await deleteProduct(id); // optimistic delete + rollback

      setOpen(false);
    } finally {
      setDeleting(false);
    }
  };

  if (!open) return null;

  return (
    <div className="modal">
      <div className="modal-container gap-4 items-center text-center">
        <XCircle color="red" size={60} />
        <h1 className="font-bold text-[24px]">Are You Sure?</h1>
        <p>
          This action cannot be undone. All values associated with this product
          will be lost.
        </p>
        <button
          onClick={onConfirm}
          className="bg-red-500 px-8 py-2 w-full rounded-[8px] text-white"
        >
          Delete
        </button>
        <button
          onClick={() => setOpen(false)}
          className="bg-gray-500 px-8 py-2 w-full rounded-[8px] text-white"
        >
          Cancel
        </button>
      </div>
    </div>
  );
}
