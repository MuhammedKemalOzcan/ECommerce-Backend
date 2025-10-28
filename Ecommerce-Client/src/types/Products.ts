import type { ImageGalleries } from "./Gallery";
import type { ProductBoxes } from "./ProductBox";

export interface Products {
  id: string;
  name: string;
  stock: number;
  price: number;
  features: string;
  category: string;
  description: string;
  productBoxes?: ProductBoxes[];
  productGalleries?: ImageGalleries[];
}

export interface AddProduct {
  name: string | null;
  stock: number;
  price: number;
  features: string;
  category: string;
  description: string;
  productBox?: ProductBoxes[];
  productGallery?: ImageGalleries[];
}
