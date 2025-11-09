import { useShallow } from "zustand/shallow";
import { useProductStore } from "../../stores/productStore";
import { useMemo, useState } from "react";
import CatalogFilter from "../components/products/CatalogFilter";
import ProductsTable from "../components/products/ProductsTable";
import type { Category, Status } from "../../types/Catalog";
import { PacmanLoader } from "react-spinners";

export default function Catalog() {
  const { loading, products } = useProductStore(
    useShallow((state) => ({
      loading: state.loading,
      products: state.products,
    }))
  );
  const [category, setCategory] = useState<Category>("All Categories");
  const [status, setStatus] = useState<Status>("All Status");

  const filtered = useMemo(() => {
    const cat = category.toLowerCase();
    const isAll = cat === "all categories";
    return products.filter((p) => {
      const matchCat = isAll ? true : (p.category ?? "").toLowerCase() === cat;

      const available = (p.stock ?? 0) > 0;
      const matchStatus =
        status === "All Status"
          ? true
          : status === "Available"
          ? available
          : !available;

      return matchCat && matchStatus;
    });
  }, [products, category, status]);

  if (loading)
    return (
      <div className="modal">
        <PacmanLoader />
      </div>
    );

  return (
    <div className="w-full w-auto p-12">
      <h1 className="font-bold text-[40px]">Products</h1>
      <CatalogFilter
        category={category}
        status={status}
        onCategoryChange={setCategory}
        onStatusChange={setStatus}
      />
      <ProductsTable products={filtered} />
    </div>
  );
}
