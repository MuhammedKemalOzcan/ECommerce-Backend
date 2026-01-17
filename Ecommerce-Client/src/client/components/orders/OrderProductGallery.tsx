import image from "../../../assets/Headphones.svg";
import type { OrderItems } from "../../../types/Order";

interface GalleryProps {
  products: OrderItems[];
}

export default function OrderProductGallery({ products }: GalleryProps) {
  return (
    <div className="flex gap-2">
      {products.map((product) => (
        <div className="flex ">
          <img src={product.imageUrl} className="size-20" />
        </div>
      ))}
    </div>
  );
}
