import { Edit, Trash2 } from "lucide-react";
import type { Products } from "../../../types/Products";
import { useState } from "react";
import image from "../../../assets/product.svg";
import { formatCurrency } from "../../../utils/format";
import DeleteModal from "../DeleteModal";
import { useNavigate } from "react-router-dom";

type Props = {
  products: Products[];
};

export default function ProductsTable({ products }: Props) {
  const navigate = useNavigate();
  const [open, setOpen] = useState<boolean>(false);


  const handleDelete = (id: string) => {
    setOpen(true);
    navigate(`?id=${id}`);
  };

  return (
    <div>
      <table className="table-auto w-full mt-8 border">
        <thead>
          <tr className="text-left border">
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
          {products.map((p) => (
            <tr className="relative border" key={p.id}>
              <td>
                <img className="size-20" src={image} />
              </td>
              <td>{p.name}</td>
              <td>{p.category}</td>
              <td>{formatCurrency(p.price)}</td>
              <td>{p.stock}</td>
              <td
                className={`${
                  p.stock === 0
                    ? "bg-red-200 text-red-900"
                    : "bg-green-200 text-green-900"
                } border flex justify-center mt-6 rounded-full`}
              >
                {p.stock > 0 ? "Available" : "Out Of Stock"}
              </td>
              <td className=" gap-2">
                <button
                  onClick={() => handleDelete(p.id)}
                  aria-label={`Delete ${p.name}`}
                  className="inline-flex items-center gap-5 px-3 py-1.5 rounded bg-red-600 text-white disabled:opacity-60"
                >
                  <Trash2 size={16} />
                </button>
                <button
                  onClick={() => navigate(`${p.id}`)}
                  aria-label={`Delete ${p.name}`}
                  className="inline-flex ml-2 items-center gap-5 px-3 py-1.5 rounded bg-blue-600 text-white disabled:opacity-60"
                >
                  <Edit size={16} />
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <DeleteModal open={open} setOpen={setOpen} />
    </div>
  );
}
