import type { Gallery } from "./Gallery";
import type { ProductBox } from "./productBox";


export interface Products {
  id: string;
  name: string;
  stock: number;
  price: number;
  features: string;
  category: string;
  description: string;
  productBox?: ProductBox[];
  productGallery?: Gallery[];
}

export interface AddProduct {
  name: string;
  stock: number;
  price: number;
  features: string;
  category: string;
  description: string;
  productBox?: ProductBox[];
  productGallery?: Gallery[];
}
