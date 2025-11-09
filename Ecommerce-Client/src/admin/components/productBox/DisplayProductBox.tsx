import { PenBoxIcon, Trash2 } from "lucide-react";
import type { ProductBoxes } from "../../../types/ProductBox";
import { useProductBoxStore } from "../../../stores/ProductBoxStore";
import { useSearchParams } from "react-router-dom";
import { useState } from "react";
import { toast } from "react-toastify";
import ConfirmationModal from "../common/ConfirmationModal";

type DisplayProps = {
  box: ProductBoxes;
  onEditBox: (id: string | null) => void;
};

export default function DisplayProductBox({ box, onEditBox }: DisplayProps) {
  const deleteBoxItem = useProductBoxStore((s) => s.deleteBoxItem);

  const [searchParams, setSearchParams] = useSearchParams();
  const [isDeleting, setIsDeleting] = useState(false);

  // Modal state from URL
  const boxId = searchParams.get("boxId");
  const isDeleteModalOpen = Boolean(boxId);

  const handleDelete = (id: string) => {
    setSearchParams({ boxId: id });
  };

  const handleConfirmDelete = async () => {
    if (!boxId && boxId === undefined) return;

    try {
      setIsDeleting(true);
      await deleteBoxItem(boxId);
      setSearchParams({});
    } catch (error) {
      toast.error("Silme işlemi başarısız");
      console.error(error);
    } finally {
      setIsDeleting(false);
    }
  };

  const handleCancelDelete = () => {
    setSearchParams({});
  };

  return (
    <div className="flex gap-3">
      <div className="flex gap-1 w-[120px]">
        <p className="text-orange-500">{box.quantity}x</p>
        <p className="whitespace-nowrap">{box.name}</p>
        <button onClick={() => onEditBox(box.id)} className="">
          <PenBoxIcon size={16} color="blue" />
        </button>
        <button
          onClick={() => handleDelete(box.id)}
          className="justify-self-end"
        >
          <Trash2 size={16} color="red" className="hover:bg-red-50" />
        </button>
      </div>

      <ConfirmationModal
        isOpen={isDeleteModalOpen}
        title="Ürünü Sil"
        message="Bu işlem geri alınamaz. Ürün ve tüm ilişkili veriler kalıcı olarak silinecek."
        confirmText="Sil"
        cancelText="İptal"
        variant="danger"
        isLoading={isDeleting}
        onConfirm={handleConfirmDelete}
        onCancel={handleCancelDelete}
      />
    </div>
  );
}
