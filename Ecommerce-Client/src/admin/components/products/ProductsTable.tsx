import type { Products } from "../../../types/Products";
import { useState } from "react";
import { useSearchParams } from "react-router-dom";
import ProductRow from "./ProductRow";
import { useProductStore } from "../../../stores/productStore";
import { toast } from "react-toastify";
import ConfirmationModal from "../common/ConfirmationModal";
import AddImageModal from "../common/AddImageModal";
type Props = {
  products: Products[];
};

export default function ProductsTable({ products }: Props) {
  const deleteProduct = useProductStore((s) => s.deleteProduct);

  const [expandedRowId, setExpandedRowId] = useState<string | null>(null);
  const [searchParams, setSearchParams] = useSearchParams();

  const deleteId = searchParams.get("deleteId");
  const isDeleteModalOpen = Boolean(deleteId);

  const productId = searchParams.get("productId");
  const isAddModalOpen = Boolean(productId);

  const toggleRow = (id: string) => {
    setExpandedRowId(expandedRowId === id ? null : id);
  };

  const handleDelete = (id: string) => {
    setSearchParams({ deleteId: id });
  };

  const handleAdd = (id: string) => {
    setSearchParams({ productId: id });
  };

  const handleConfirmDelete = async () => {
    if (deleteId) {
      try {
        await deleteProduct(deleteId);
        setSearchParams({});
      } catch (error) {
        toast.error("Silme işlemi başarısız");
        console.error(error);
      }
    }
  };

  const handleCancel = () => {
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
              onAdd={handleAdd}
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
        onConfirm={handleConfirmDelete}
        onCancel={handleCancel}
      />
      <AddImageModal
        productId={productId}
        isOpen={isAddModalOpen}
        onCancel={handleCancel}
      />
    </div>
  );
}
