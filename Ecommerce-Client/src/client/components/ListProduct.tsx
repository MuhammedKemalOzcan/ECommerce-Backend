import { useLocation, useNavigate } from "react-router-dom";
import { useProductStore } from "../../stores/productStore";
import { useShallow } from "zustand/shallow";
import image from "../../assets/product.svg";
import { PacmanLoader } from "react-spinners";

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

  const firstGallery = filteredProduct.map((p) => {
    return p.productGalleries?.[0];
  });
  console.log(firstGallery);

  if (loading)
    return (
      <div>
        <PacmanLoader />
      </div>
    );

  return (
    <div>
      {filteredProduct.map((product, index) => (
        <div className="gap-20" key={product.id}>
          <div
            className={`${
              index % 2 === 1 ? "flex flex-row-reverse" : "flex"
            }  p-40 gap-32`}
          >
            {firstGallery.map(
              (gallery, galleryIndex) =>
                index === galleryIndex && (
                  <div
                    className="bg-gray-300 w-[50%] flex items-center justify-center rounded-lg"
                    key={galleryIndex}
                  >
                    <img
                      className="shadow-lg rounded-lg h-[90%]"
                      src={
                        gallery?.path
                          ? `${gallery?.path}`
                          : image
                      }
                    />
                  </div>
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
        </div>
      ))}
    </div>
  );
}
