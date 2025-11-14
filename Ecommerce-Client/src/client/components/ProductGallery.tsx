import type { Products } from "../../types/Products";

type Props = {
  product: Products | null;
};

export default function ProductGallery({ product }: Props) {
  const image = product?.productGalleries?.map((gallery) => gallery.path);
  console.log(image);

  return (
    <div className="flex w-[80%] p-4 gap-2 items-center justify-center">
      {product?.productGalleries?.map((image) => (
        <img className="w-[30%]" src={`${image.path}`} />
      ))}
    </div>
  );
}
