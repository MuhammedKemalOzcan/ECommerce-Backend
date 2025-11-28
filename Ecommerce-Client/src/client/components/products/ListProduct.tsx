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
      <div>
        <PacmanLoader />
      </div>
    );

  return (
    <div className="flex flex-col gap-40">
      {filteredProduct.map(
        (product, index) =>
          product.stock > 0 && (
            <div
              key={index}
              className={`${
                index % 2 === 1 ? "flex flex-row-reverse" : "flex"
              }  gap-32 h-[560px] `}
            >
              {showCase.map(
                (gallery, galleryIndex) =>
                  index === galleryIndex && (
                    <img
                      key={galleryIndex}
                      className="shadow-lg rounded-lg w-[45%] h-[90%]"
                      src={gallery?.path ? `${gallery?.path}` : image}
                    />
                  )
              )}

              <div className="flex flex-col justify-center gap-20 p-2 w-[50%]">
                <p className="text-[40px] font-bold leading-[44px] tracking-[1.43px] ">
                  {product.name}
                </p>
                <p className="text-[15px] font-medium leading-[25px] w-[60%]">
                  {product.description}
                </p>
                <button
                  onClick={() => handleClick(product.id)}
                  className="btn-1 p-4 w-[30%] "
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
