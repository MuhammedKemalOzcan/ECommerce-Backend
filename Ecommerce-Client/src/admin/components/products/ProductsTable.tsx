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

  console.log(deleteId);
  

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
    <div className="w-full mt-8">
      {/* Table Container Card */}
      <div className="bg-[#FFFFFF] rounded-xl border border-[#F1F1F1] shadow-sm overflow-hidden">
        {/* Responsive Scroll Wrapper */}
        <div className="overflow-x-auto">
          <table className="w-full text-left border-collapse">
            <thead>
              <tr className="bg-[#FAFAFA] border-b border-[#F1F1F1]">
                {/* Chevron/Expand Col */}
                <th className="px-6 py-4 text-xs font-bold text-[#101010]/40 uppercase tracking-widest w-[50px]">
                  #
                </th>
                {/* Product Image Col */}
                <th className="px-6 py-4 text-xs font-bold text-[#101010]/40 uppercase tracking-widest w-24">
                  Görsel
                </th>
                {/* Name Col - Geniş Alan */}
                <th className="px-6 py-4 text-xs font-bold text-[#101010]/40 uppercase tracking-widest min-w-[200px]">
                  Ürün Adı
                </th>
                {/* Category */}
                <th className="px-6 py-4 text-xs font-bold text-[#101010]/40 uppercase tracking-widest">
                  Kategori
                </th>
                {/* Price */}
                <th className="px-6 py-4 text-xs font-bold text-[#101010]/40 uppercase tracking-widest">
                  Fiyat
                </th>
                {/* Stock */}
                <th className="px-6 py-4 text-xs font-bold text-[#101010]/40 uppercase tracking-widest">
                  Stok
                </th>
                {/* Status */}
                <th className="px-6 py-4 text-xs font-bold text-[#101010]/40 uppercase tracking-widest">
                  Durum
                </th>
                {/* Actions */}
                <th className="px-6 py-4 text-xs font-bold text-[#101010]/40 uppercase tracking-widest text-right">
                  İşlemler
                </th>
              </tr>
            </thead>
            <tbody className="divide-y divide-[#F1F1F1] bg-[#FFFFFF]">
              {products.length === 0 && (
                <tr>
                  <td colSpan={8} className="px-6 py-16 text-center">
                    <div className="flex flex-col items-center justify-center">
                      <div className="w-12 h-12 rounded-full bg-[#FAFAFA] flex items-center justify-center mb-3">
                        <svg
                          className="w-6 h-6 text-[#101010]/20"
                          fill="none"
                          viewBox="0 0 24 24"
                          stroke="currentColor"
                        >
                          <path
                            strokeLinecap="round"
                            strokeLinejoin="round"
                            strokeWidth={2}
                            d="M20 13V6a2 2 0 00-2-2H6a2 2 0 00-2 2v7m16 0v5a2 2 0 01-2 2H6a2 2 0 01-2-2v-5m16 0h-2.586a1 1 0 00-.707.293l-2.414 2.414a1 1 0 01-.707.293h-3.172a1 1 0 01-.707-.293l-2.414-2.414A1 1 0 006.586 13H4"
                          />
                        </svg>
                      </div>
                      <span className="text-sm font-medium text-[#101010]/60">
                        Kayıt Bulunamadı
                      </span>
                    </div>
                  </td>
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
        </div>
      </div>

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
