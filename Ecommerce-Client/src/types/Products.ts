import type { AddProductBoxes, ProductBoxes } from "./ProductBox";

export interface Products {
  id: string;
  name: string;
  stock: number;
  price: number;
  features: string;
  category: string;
  description: string;
  productBoxes?: ProductBoxes[];
}

export interface AddProduct {
  name: string | null;
  stock: number;
  price: number;
  features: string;
  category: string;
  description: string;
  productBoxes?: AddProductBoxes[];
}
