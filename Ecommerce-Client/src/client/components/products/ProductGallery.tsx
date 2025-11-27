import type { Products } from "../../../types/Products";


type Props = {
  product: Products | null;
};

export default function ProductGallery({ product }: Props) {
  return (
    <div className="flex w-[80%] p-4 gap-2 items-center justify-center">
      {product?.productGalleries?.map((image, index) => (
        <img key={index} className="w-[30%]" src={`${image.path}`} />
      ))}
    </div>
  );
}
