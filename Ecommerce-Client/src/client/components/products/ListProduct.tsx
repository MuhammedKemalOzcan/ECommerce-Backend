import { useLocation, useNavigate } from "react-router-dom";
import { useShallow } from "zustand/shallow";
import image from "../../../assets/product.svg";
import { PacmanLoader } from "react-spinners";
import { useProductStore } from "../../../stores/productStore";

export default function ListProduct() {
  let location = useLocation().pathname.slice(1);
  const navigate = useNavigate();

  const { loading, products } = useProductStore(
    useShallow((state) => ({
      loading: state.loading,
      products: state.products,
    }))
  );

  const handleClick = (id: string) => {
    navigate(`${id}`);
  };

  const filteredProduct = products.filter((p) => p.category == location);

  const showCase = filteredProduct.map((p) =>
    p.productGalleries?.find((gallery) => gallery.isPrimary === true)
  );

  if (loading)
    return (
      <div className="absolute inset-0 flex items-center justify-center bg-white/80 rounded-lg z-50">
        <PacmanLoader />
      </div>
    );

  return (
    <div className=" flex flex-col">
      {filteredProduct.map(
        (product, index) =>
          product.stock > 0 && (
            <div
              key={index}
              className={`${
                index % 2 === 1
                  ? "lg:flex-row-reverse flex flex-col"
                  : "lg:flex-row flex flex-col"
              }  lg:gap-32 lg:h-[560px] gap-12 items-center text-center mb-12`}
            >
              {showCase.map(
                (gallery, galleryIndex) =>
                  index === galleryIndex && (
                    <img
                      key={galleryIndex}
                      className="size-[75%] lg:w-[45%] lg:h-[90%] md:max-w-[400px] lg:max-w-[540px] flex"
                      src={gallery?.path ? `${gallery?.path}` : image}
                    />
                  )
              )}
              <div className="flex flex-col gap-8 items-center justify-center p-2 lg:gap-20 lg:w-[50%] md:max-w-[65%] ">
                <p className="text-[40px] font-bold leading-[44px] tracking-[1.43px] ">
                  {product.name}
                </p>
                <p className="text-[15px] font-medium leading-[25px] lg:w-[60%]">
                  {product.description}
                </p>
                <button
                  onClick={() => handleClick(product.id)}
                  className="btn-1 p-4 lg:w-[30%] "
                >
                  See Product
                </button>
              </div>
            </div>
          )
      )}
    </div>
  );
}
