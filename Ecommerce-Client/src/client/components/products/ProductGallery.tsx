import { useState } from "react";
import type { Products } from "../../../types/Products";
import { ChevronLeft, ChevronRight, ChevronRightCircle } from "lucide-react";
import { baseApiUrl } from "../../../constants/apiUrl";

type Props = {
  product: Products | null;
};

export default function ProductGallery({ product }: Props) {
  const [currentIndex, setCurrentIndex] = useState(0);

  const lastIndex = (product?.productGalleries?.length ?? 0) - 1;

  const handlePrev = () => {
    setCurrentIndex(currentIndex === 0 ? lastIndex : currentIndex - 1);
  };

  const handleNext = () => {
    setCurrentIndex(currentIndex === lastIndex ? 0 : currentIndex + 1);
  };

  const currentSlide = product?.productGalleries?.[currentIndex];

  return (
    <div className="flex flex-col gap-8 w-full">
      {/* Ana Carousel Container */}
      <div className="relative w-full bg-gray-100 rounded-2xl overflow-hidden shadow-lg">
        {/* Resim Container 1:1 */}
        <div className="flex w-full aspect-square bg-gray-100 rounded-2xl mx-auto max-w-2xl relative">
          <button
            onClick={handlePrev}
            className="absolute left-4 top-1/2 -translate-y-1/2 bg-black/60 hover:bg-black p-3 rounded-full group"
          >
            <ChevronLeft color="white" />
          </button>
          <img
            src={`${baseApiUrl}/${currentSlide?.path}`}
            alt={product?.name}
            className="w-full h-full object-contain p-8 transition-transform duration-300 group-hover:scale-105"
          />
          <button
            onClick={handleNext}
            className="absolute right-4 top-1/2 -translate-y-1/2 z-20 bg-black/60 hover:bg-black text-white p-3 
            rounded-full transition-all duration-200 hover:scale-110 active:scale-95 backdrop-blur-sm"
          >
            <ChevronRight size={24} />
          </button>
        </div>
        <p>
          {currentIndex + 1}/{product?.productGalleries?.length}
        </p>
        <div className="flex gap-3 pb-2 px-4 ">
          {product?.productGalleries?.map((gallery, index) => (
            <button
              onClick={() => setCurrentIndex(index)}
              key={gallery.id}
              className={`flex-shrink-0 w-20 h-20 rounded-lg overflow-hidden border-2 ${
                index === currentIndex
                  ? "border-black scale-105"
                  : "border-gray-300"
              } `}
            >
              <img src={`${baseApiUrl}/${gallery.path}`} />
            </button>
          ))}
        </div>
      </div>
    </div>
  );
}
