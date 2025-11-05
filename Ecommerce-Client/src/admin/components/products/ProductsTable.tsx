import type { Products } from "../../../types/Products";
import { useState } from "react";
import { useSearchParams } from "react-router-dom";
import ProductRow from "./ProductRow";
import { useProductStore } from "../../../stores/productStore";
import { toast } from "react-toastify";
import ConfirmationModal from "../common/ConfirmationModal";
type Props = {
  products: Products[];
};

export default function ProductsTable({ products }: Props) {
  const [expandedRowId, setExpandedRowId] = useState<string | null>(null);
  const deleteProduct = useProductStore((s) => s.deleteProduct);
  const [searchParams, setSearchParams] = useSearchParams();

  const toggleRow = (id: string) => {
    setExpandedRowId(expandedRowId === id ? null : id);
  };

  const deleteProductId = searchParams.get("deleteId");
  const isDeleteModalOpen = Boolean(deleteProductId);

  const [isDeleting, setIsDeleting] = useState(false);

  const handleDelete = (id: string) => {
    setSearchParams({ deleteId: id });
  };

  const handleConfirmDelete = async () => {
    if (!deleteProductId) return;
    try {
      setIsDeleting(true);
      await deleteProduct(deleteProductId);
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
    <div>
      <table className="table-auto w-full mt-8 border">
        <thead>
          <tr className="text-left border">
            <th className="w-[20px]"></th>
            <th className="w-20">Product</th>
            <th className="w-60">Name</th>
            <th className="w-20">Category</th>
            <th className="w-20">Price</th>
            <th className="w-20">Stock</th>
            <th className="w-20">Status</th>
            <th className="w-20">Actions</th>
          </tr>
        </thead>
        <tbody>
          {products.length === 0 && (
            <tr>
              <td colSpan={8}>Kayıt Bulunamadı</td>
            </tr>
          )}
          {products.map((p) => (
            <ProductRow
              key={p.id}
              product={p}
              expandedRowId={expandedRowId}
              onToggle={toggleRow}
              onDelete={handleDelete}
            />
          ))}
        </tbody>
      </table>
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
