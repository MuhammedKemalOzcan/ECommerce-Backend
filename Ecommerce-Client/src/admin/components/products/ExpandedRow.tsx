import { GalleryHorizontalEndIcon, Package, X } from "lucide-react";
import type { Products } from "../../../types/Products";
import { useState } from "react";
import ProductBoxForm from "../productBox/ProductBoxForm";
import type { ProductBoxes } from "../../../types/ProductBox";
import DisplayProductBox from "../productBox/DisplayProductBox";
import AddProductBox from "../productBox/AddProductBox";
import { productGalleryApi } from "../../../api/productGalleryApi";

type Props = {
  product: Products;
};

export default function ExpandedRow({ product }: Props) {
  const [boxItemId, setBoxItemId] = useState<string | null>(null);

  const handleDelete = async (
    productId: string | null,
    imageId: string | null
  ) => {
    await productGalleryApi.delete(productId, imageId);
  };

  const handleEditBox = (id: string | null) => {
    setBoxItemId(boxItemId ? null : id);
  };

  const handleUpdate = async (boxId: string, data: ProductBoxes) => {
    await updateBoxItem(boxId, data);
    setBoxItemId(null);
  };

  const handleCancelEdit = () => {
    setBoxItemId(null);
  };

  console.log(product);

  return (
    <div className="bg-gray-200 rounded-lg w-full shadow-sm flex flex-col gap-8 p-8">
      <div className="flex text-[20px] text-blue-700 gap-2">
        <Package size={24} />
        <h3 className="font-bold text-blue-500">Product Contains:</h3>
      </div>
      <div className="flex gap-52">
        <div className="flex flex-col whitespace-nowrap">
          <h3 className="font-bold text-blue-500">Product Name:</h3>
          <p>{product.name}</p>
        </div>
        <div>
          <h3 className="font-bold text-blue-500">Product Boxes:</h3>
          {product.productBoxes?.map((bx) => (
            <div key={bx.id} className="flex gap-4 items-center">
              {boxItemId === bx.id ? (
                <ProductBoxForm
                  onCancel={handleCancelEdit}
                  onSubmit={(data) => handleUpdate(bx.id, data)}
                  defaultValues={bx}
                />
              ) : (
                <DisplayProductBox
                  productId={product.id}
                  box={bx}
                  onEditBox={handleEditBox}
                />
              )}
            </div>
          ))}
          <AddProductBox productId={product.id} />
        </div>
        <div className="w-[40%] h-[90%]">
          <div className="flex gap-2 items-center text-blue-500">
            <GalleryHorizontalEndIcon size={20} />
            <h3 className="font-bold text-blue-500">Product Gallery:</h3>
          </div>
          <div className="grid grid-cols-2 gap-2">
            {product.productGalleries?.map((image) => (
              <div className="relative">
                <img className="w-[80%]" src={`${image.path}`} />
                <button
                  onClick={() => handleDelete(product.id, image.id)}
                  className="absolute top-0 "
                >
                  <X color="red" />
                </button>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}
