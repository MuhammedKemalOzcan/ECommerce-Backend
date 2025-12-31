import type { ImageGalleries } from "./Gallery";

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
  name: string;
  stock: number;
  price: number;
  features: string;
  category: string;
  description: string;
  productBoxes?: AddProductBoxes[];
}

export interface ProductBoxes {
  id: string;
  name: string;
  quantity: number;
}

export interface AddProductBoxes {
  name: string;
  quantity: number;
}
