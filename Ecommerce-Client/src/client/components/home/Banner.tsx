import { useEffect } from "react";
import { useProductStore } from "../../../stores/productStore";
import { useShallow } from "zustand/shallow";
import { useNavigate } from "react-router-dom";
import image from "../../../assets/product.svg";

export default function HomeBanner() {
  const { getAll, products } = useProductStore(
    useShallow((s) => ({
      getAll: s.getAll,
      products: s.products,
    }))
  );

  const navigate = useNavigate();

  useEffect(() => {
    getAll();
  }, [getAll]);

  const product = products[1];

  return (
    <div className="relative w-full min-h-[600px] lg:min-h-0 flex lg:flex-row flex-col items-center justify-center lg:justify-between overflow-hidden">
      <div className="relative z-10 flex flex-col items-center text-center gap-6 p-6 lg:w-1/2 lg:items-start lg:text-left lg:pr-20">
        <p className="text-black opacity-50 tracking-[10px] text-sm uppercase">
          New Product
        </p>
        <h1 className="text-[40px] leading-tight lg:text-[56px] font-bold uppercase">
          {product?.name}
        </h1>

        <p className="text-black/75 max-w-[400px] lg:max-w-none">
          {product?.description}
        </p>

        <button
          onClick={() => navigate(`/${product?.category}/${product?.id}`)}
          className="bg-[#D87D4A] hover:bg-[#fbaf85] text-white font-bold py-4 px-8 uppercase tracking-wider transition-colors mt-4"
        >
          See Product
        </button>
      </div>
      <div className="absolute inset-0 w-full h-full lg:static lg:w-1/2 lg:h-auto z-0">
        <img
          src={image}
          alt={product?.name}
          className="w-full h-full object-cover opacity-10 lg:opacity-100 lg:object-contain lg:max-w-[708px]"
        />
      </div>
    </div>
  );
}
