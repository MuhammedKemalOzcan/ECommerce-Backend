import React from "react";
import type { Products } from "../../../types/Products";
import { ChevronDown, ChevronUp, Edit, Trash2 } from "lucide-react";
import image from "../../../assets/product.svg";
import { formatCurrency } from "../../../utils/format";
import StatusBadge from "./StatusBadge";
import { useNavigate } from "react-router-dom";
import ExpandedRow from "./ExpandedRow";

type RowProps = {
  product: Products;
  onToggle: (id: string) => void;
  expandedRowId: string | null;
  onDelete: (id: string) => void;
};

const ProductRow = React.memo(function ProductRow({
  product,
  expandedRowId,
  onToggle,
  onDelete,
}: RowProps) {
  
  const navigate = useNavigate();

  const toggleRow = (id: string | null) => {
    if (!id) return;
    onToggle(id);
  };

  const handleDelete = (id: string | null) => {
    if (!id) return;
    onDelete(id);
  };

  return (
    <React.Fragment key={product.id}>
      <tr className="relative border hover:bg-gray-200">
        <td
          onClick={(e) => {
            e.stopPropagation();
            toggleRow(product.id);
          }}
        >
          {expandedRowId === product.id ? <ChevronUp /> : <ChevronDown />}
        </td>
        <td>
          <img loading="lazy" className="size-20" src={image} />
        </td>
        <td>{product.name}</td>
        <td>{product.category}</td>
        <td>{formatCurrency(product.price)}</td>
        <td>{product.stock}</td>
        <td>
          <StatusBadge stock={product.stock} />
        </td>
        <td className=" gap-2">
          <button
            onClick={() => handleDelete(product.id)}
            aria-label={`Delete ${product.name}`}
            className="inline-flex items-center gap-5 px-3 py-1.5 rounded bg-red-600 text-white disabled:opacity-60"
          >
            <Trash2 size={16} />
          </button>
          <button
            onClick={() => navigate(`${product.id}`)}
            aria-label={`Delete ${product.name}`}
            className="inline-flex ml-2 items-center gap-5 px-3 py-1.5 rounded bg-blue-600 text-white disabled:opacity-60"
          >
            <Edit size={16} />
          </button>
        </td>
      </tr>
      {expandedRowId === product.id && (
        <tr>
          <td className="px-4 py-6" colSpan={8}>
            <ExpandedRow product={product} />
          </td>
        </tr>
      )}
    </React.Fragment>
  );
});

export default ProductRow;
