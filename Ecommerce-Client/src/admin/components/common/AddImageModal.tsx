import { X } from "lucide-react";
import AddProductImage from "../products/AddProductImage";

type Props = {
  isOpen: boolean;
  onCancel: () => void;
  productId: string | null;
};

export default function AddImageModal({ isOpen, onCancel, productId }: Props) {
  if (!isOpen) return;
  return (
    <div className="modal">
      <div className="modal-container gap-4">
        <button className="text-gray-400" onClick={onCancel}>
          <X size={24} />
        </button>
        <AddProductImage productId={productId} />
      </div>
    </div>
  );
}
