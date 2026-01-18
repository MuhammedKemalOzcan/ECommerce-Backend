import { useShallow } from "zustand/shallow";
import { useProductStore } from "../../../stores/productStore";
import { useEffect, useMemo } from "react";
import { useNavigate } from "react-router-dom";
import { randomNumberGenerator } from "../../../constants/randomNumberGenerator";
import { baseApiUrl } from "../../../constants/apiUrl";

export default function ProductShowcase() {
  const { getAll, products } = useProductStore(
    useShallow((s) => ({
      getAll: s.getAll,
      products: s.products,
    })),
  );

  const navigate = useNavigate();

  useEffect(() => {
    getAll();
  }, [getAll]);

  const randomIndex = useMemo(() => {
    return randomNumberGenerator(products.length);
  }, [products.length]);

  const product = products?.[randomIndex];

  if (!product?.productGalleries) {
    return <div className="h-[560px] bg-[#D87D4A]"></div>;
  }

  return (
    <div className="flex flex-col h-screen max-h-[560px] items-center justify-center w-full bg-gradient-to-r from-[#D87D4A] to-[#E8956F] overflow-hidden">
      {product?.productGalleries?.map(
        (g) =>
          g.isPrimary === true && (
            <div
              key={g.id}
              className="h-full w-full flex items-center justify-between px-12 py-8 gap-16"
            >
              {/* Resim Bölümü */}
              <div className="flex-1 flex items-center justify-center">
                <img
                  src={`${baseApiUrl}/${g.path}`}
                  alt={product.name}
                  className="h-full max-h-96 object-contain drop-shadow-2xl hover:scale-105 transition-transform duration-500"
                />
              </div>

              {/* İçerik Bölümü */}
              <div className="flex flex-col flex-1 justify-center gap-6">
                <div className="space-y-4">
                  <h1 className="text-5xl md:text-6xl font-bold text-white leading-tight">
                    {product.name}
                  </h1>
                  <p className="text-base text-white/80 leading-relaxed max-w-md">
                    {product.description}
                  </p>
                </div>

                <button
                  onClick={() => navigate(`/${product.category}/${product.id}`)}
                  className="bg-black text-white px-8 py-3 font-semibold rounded-lg hover:bg-white hover:text-black transition-all duration-300 w-fit shadow-lg hover:shadow-2xl active:scale-95 uppercase tracking-wider"
                >
                  See Product
                </button>
              </div>
            </div>
          ),
      )}
    </div>
  );
}
