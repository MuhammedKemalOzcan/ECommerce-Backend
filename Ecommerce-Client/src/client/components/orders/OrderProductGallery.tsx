import image from "../../../assets/Headphones.svg";

interface GalleryProps {
  products: {
    productId: string;
    name: string;
    image: string;
    price: number;
    quantity: number;
  }[];
}

export default function OrderProductGallery({ products }: GalleryProps) {
  return (
    <div className="flex gap-2">
      {products.map((product) => (
        <div className="flex ">
          <img src={image} className="size-20" />
        </div>
      ))}
    </div>
  );
}
