import type { Category, Status } from "../../../types/Catalog";
import { CATEGORY_OPTIONS, STATUS_OPTIONS } from "../../../constants/catalog";
import { PlusCircleIcon } from "lucide-react";
import { useNavigate } from "react-router-dom";

type Props = {
  category: Category;
  status: Status;
  onCategoryChange: React.Dispatch<React.SetStateAction<Category>>;
  onStatusChange: React.Dispatch<React.SetStateAction<Status>>;
};

export default function CatalogFilter({
  category,
  status,
  onCategoryChange,
  onStatusChange,
}: Props) {
  const navigate = useNavigate();

  return (
    <div className="flex justify-between">
      <div className="flex gap-10">
        <select
          value={category}
          onChange={(e) => onCategoryChange(e.target.value as Category)}
          name="category"
          id="category"
          className="w-auto border rounded-[8px] px-4 py-2 active-none"
        >
          {CATEGORY_OPTIONS.map((c, index) => (
            <option key={index} value={c}>
              {c}
            </option>
          ))}
        </select>
        <select
          value={status}
          onChange={(e) => onStatusChange(e.target.value as Status)}
          className="w-auto border rounded-[8px] px-4 py-2 active-none"
        >
          {STATUS_OPTIONS.map((s, index) => (
            <option key={index} value={s}>
              {s}
            </option>
          ))}
        </select>
      </div>
      {/* Add Product Button */}
      <button
        onClick={() => navigate("create-product")}
        className="btn-1 flex items-center p-3 w-auto gap-3"
      >
        <span>
          <PlusCircleIcon />
        </span>
        <p>Add New Product</p>
      </button>
    </div>
  );
}
