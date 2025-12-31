import { PenBoxIcon, Trash2 } from "lucide-react";
import type { ProductBoxes } from "../../../types/ProductBox";
import { useState } from "react";
import { toast } from "react-toastify";
import ConfirmationModal from "../common/ConfirmationModal";
import { useProductStore } from "../../../stores/productStore";

type DisplayProps = {
  box: ProductBoxes;
  onEditBox: (id: string | null) => void;
  productId: string;
};

export default function DisplayProductBox({
  box,
  onEditBox,
  productId,
}: DisplayProps) {
  const removeBoxFromProduct = useProductStore((s) => s.removeBoxFromProduct);

  const [isDeleteModalOpen, setIsDeleteModalOpen] = useState(false);
  const [isDeleting, setIsDeleting] = useState(false);

  const handleConfirmDelete = async () => {
    try {
      setIsDeleting(true);
      await removeBoxFromProduct(box.id, productId);
      setIsDeleteModalOpen(false);
    } catch (error) {
      toast.error("Silme işlemi başarısız");
      console.error(error);
    } finally {
      setIsDeleting(false);
    }
  };

  const handleCancelDelete = () => {
    setIsDeleteModalOpen(false);
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
          onClick={() => setIsDeleteModalOpen(true)}
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
