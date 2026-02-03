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
    <div className="relative flex flex-col h-screen max-h-[600px] lg:max-h-[560px] items-center justify-center w-full bg-[#D87D4A] overflow-hidden">
      {product?.productGalleries?.map(
        (g) =>
          g.isPrimary === true && (
            <div
              key={g.id}
              className="relative h-full w-full flex flex-col lg:flex-row items-center justify-center lg:justify-between px-6 md:px-12 py-8 gap-8 lg:gap-16"
            >
              {/* Resim Bölümü - Mobilde Arka Plan, Masaüstünde Sağ/Sol Blok */}
              <div className="absolute inset-0 w-full h-full lg:relative lg:flex-1 lg:flex lg:items-center lg:justify-center z-0">
                <img
                  src={`${baseApiUrl}/${g.path}`}
                  alt={product.name}
                  className="w-full h-full object-cover opacity-20 lg:opacity-100 lg:h-full lg:max-h-96 lg:object-contain drop-shadow-2xl hover:scale-105 transition-transform duration-500"
                />
              </div>

              {/* İçerik Bölümü - Resmin Üzerinde Kalması İçin z-10 ekledik */}
              <div className="relative z-10 flex flex-col flex-1 justify-center items-center text-center lg:items-start lg:text-left gap-6">
                <div className="space-y-4">
                  <span className="text-white/60 tracking-[10px] uppercase text-sm font-light">
                    New Product
                  </span>
                  <h1 className="text-4xl md:text-5xl lg:text-6xl font-bold text-white leading-tight">
                    {product.name}
                  </h1>
                  <p className="text-base text-white/90 leading-relaxed max-w-md mx-auto lg:mx-0">
                    {product.description}
                  </p>
                </div>

                <button
                  onClick={() => navigate(`/${product.category}/${product.id}`)}
                  className="bg-black text-white px-8 py-3 font-semibold rounded-sm hover:bg-[#fbaf85] hover:text-white transition-all duration-300 w-fit shadow-lg active:scale-95 uppercase tracking-wider"
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
