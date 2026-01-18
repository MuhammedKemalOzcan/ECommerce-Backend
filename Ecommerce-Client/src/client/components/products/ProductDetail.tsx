import { useParams } from "react-router-dom";
import { useShallow } from "zustand/shallow";
import { useEffect, useState } from "react";
import image from "../../../assets/empty.jpg";
import { PacmanLoader } from "react-spinners";
import ProductGallery from "./ProductGallery";
import { Minus, Plus } from "lucide-react";
import { useProductStore } from "../../../stores/productStore";
import { useCartStore } from "../../../stores/cartStore";
import { baseApiUrl } from "../../../constants/apiUrl";

export default function ProductDetail() {
  const { id, category } = useParams<{ id: string; category: string }>();
  let [quantity, setQuantity] = useState<number>(1);
  const { getById, currentProduct, loading } = useProductStore(
    useShallow((s) => ({
      getById: s.getById,
      currentProduct: s.currentProduct,
      loading: s.loading,
    })),
  );
  const addItemToCart = useCartStore((s) => s.addItemToCart);

  useEffect(() => {
    if (id) getById(id);
  }, [id, getById]);

  const productBox = currentProduct?.productBoxes?.flatMap((b) => ({
    name: b.name,
    quantity: b.quantity,
  }));

  const primaryImage = currentProduct?.productGalleries?.find(
    (gallery) => gallery.isPrimary === true,
  );

  const chosen = primaryImage?.path ? `${primaryImage.path}` : image;

  const handlePlus = () => {
    setQuantity((quantity += 1));
  };
  const handleMinus = () => {
    setQuantity(quantity === 1 ? quantity : (quantity -= 1));
  };

  const AddToCart = async (productId: string | undefined) => {
    if (!productId) return;
    await addItemToCart({ productId, quantity });
  };

  if (loading)
    return (
      <div>
        <PacmanLoader />
      </div>
    );

  return (
    <div className="flex flex-col items-center gap-12">
      <div className="lg:flex-row lg:gap-32 flex flex-col md:max-w-[70%] md:justify-center">
        <div className="p-4 lg:w-[70%] lg:h-[60%] flex items-center justify-center rounded-lg">
          <img
            className="shadow-lg size-full sm:mb-8"
            src={`${baseApiUrl}/${chosen}`}
          />
        </div>
        <div className="flex flex-col justify-center lg:w-[60%] gap-10 ">
          <p className="text-[40px] font-bold leading-[44px] tracking-[1.43px] ">
            {currentProduct?.name}
          </p>
          <p className="text-[15px] font-medium leading-[25px] lg:w-[60%] ">
            {currentProduct?.description}
          </p>
          <p className="font-bold">$ {currentProduct?.price}</p>
          <div className="flex items-center gap-8">
            <div className="flex w-[120px] items-center justify-between bg-gray-100 p-4">
              <button onClick={() => handleMinus()}>
                <Minus size={16} />
              </button>
              <span>{quantity}</span>
              <button onClick={() => handlePlus()}>
                <Plus size={16} />
              </button>
            </div>
            <button
              onClick={() => AddToCart(currentProduct?.id)}
              className="btn-1 p-4 w-[160px]"
            >
              Add To Cart
            </button>
          </div>
          {currentProduct?.stock && currentProduct?.stock <= 5 && (
            <p className="text-sm text-red-500">
              Ürün tükenmek üzere acele et. son {currentProduct.stock} adet!
            </p>
          )}
        </div>
      </div>
      <div className="flex flex-col gap-12 lg:gap-40 lg:flex-row lg:justify-between md:max-w-[70%] md:justify-center">
        <div className="flex flex-col lg:w-[50%] gap-6 ">
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
