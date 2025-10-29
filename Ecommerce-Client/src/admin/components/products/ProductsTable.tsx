import type { Products } from "../../../types/Products";
import { useState } from "react";
import DeleteModal from "../DeleteModal";
import { useNavigate } from "react-router-dom";
import ProductRow from "./ProductRow";
type Props = {
  products: Products[];
};

export default function ProductsTable({ products }: Props) {
  const navigate = useNavigate();
  const [open, setOpen] = useState<boolean>(false);
  const [expandedRowId, setExpandedRowId] = useState<string | null>(null);

  const handleDelete = (id: string) => {
    setOpen(true);
    navigate(`?id=${id}`);
  };

  const toggleRow = (id: string) => {
    setExpandedRowId(expandedRowId === id ? null : id);
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
      <DeleteModal open={open} setOpen={setOpen} />
    </div>
  );
}
