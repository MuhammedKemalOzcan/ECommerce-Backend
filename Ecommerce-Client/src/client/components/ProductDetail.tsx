import { useNavigate, useParams } from "react-router-dom";
import { useProductStore } from "../../stores/productStore";
import { useShallow } from "zustand/shallow";
import { useEffect } from "react";
import image from "../../assets/empty.jpg";
import { PacmanLoader } from "react-spinners";
import ProductGallery from "./ProductGallery";

export default function ProductDetail() {
  const { id, category } = useParams<{ id: string; category: string }>();
  const { getById, currentProduct, loading } = useProductStore(
    useShallow((s) => ({
      getById: s.getById,
      currentProduct: s.currentProduct,
      loading: s.loading,
    }))
  );

  console.log(currentProduct);

  useEffect(() => {
    if (id) getById(id);
  }, [id, getById]);

  const productBox = currentProduct?.productBoxes?.flatMap((b) => ({
    name: b.name,
    quantity: b.quantity,
  }));

  const primaryImage = currentProduct?.productGalleries?.find(
    (gallery) => gallery.isPrimary === true
  );

  const chosen = primaryImage?.path ? `${primaryImage.path}` : image;

  if (loading)
    return (
      <div>
        <PacmanLoader />
      </div>
    );

  return (
    <div className="flex flex-col items-center">
      <div className="flex p-32 gap-32 ">
        <div className="bg-gray-300 p-4 w-[50%] h-[50%] flex items-center justify-center rounded-lg">
          <img className="shadow-lg size-[70%]" src={chosen} />
        </div>

        <div className="flex flex-col justify-center  w-[60%] gap-10">
          <p className="text-[40px] font-bold leading-[44px] tracking-[1.43px] ">
            {currentProduct?.name}
          </p>
          <p className="text-[15px] font-medium leading-[25px] w-[60%] ">
            {currentProduct?.description}
          </p>
          <p className="font-bold">$ {currentProduct?.price}</p>
          <button className="btn-1 p-4 w-[160px]">Add To Cart</button>
        </div>
      </div>
      <div className="p-32 gap-40 flex">
        <div className="flex flex-col w-[50%] gap-6 ">
          <p className="text-[40px] font-bold leading-[44px] tracking-[1.43px] ">
            FEATURES
          </p>
          <p className="text-[15px] text-gray-500 font-medium leading-[15px]">
            {currentProduct?.features}
          </p>
        </div>
        <div className="flex flex-col gap-6 ">
          <p className="text-[40px] font-bold leading-[44px] tracking-[1.43px] ">
            IN THE BOX
          </p>
          {productBox?.map((box, index) => (
            <div key={index} className="flex gap-4">
              <p className="text-[#d87d4a]">{box.quantity}x</p>
              <p>{box.name}</p>
            </div>
          ))}
        </div>
      </div>
      <ProductGallery product={currentProduct} />
    </div>
  );
}
