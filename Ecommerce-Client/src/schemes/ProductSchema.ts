import type { RegisterOptions } from "react-hook-form";
import type { AddProduct } from "../types/Products";
import type { ProductBoxes } from "../types/ProductBox";

export const PRODUCT_VALIDATION_RULES = {
  name: {
    required: "Ürün ismi zorunludur.",
    minLength: {
      value: 3,
      message: "Ürün ismi en az 3 karakter olmalıdır.",
    },
    maxLength: {
      value: 100,
      message: "Ürün ismi en fazla 100 karakter olabilir.",
    },
  } satisfies RegisterOptions<AddProduct, "name">,
  description: {
    maxLength: {
      value: 500,
      message: "Ürün açıklaması en fazla 500 karakter olabilir.",
    },
  } satisfies RegisterOptions<AddProduct, "description">,
  features: {
    maxLength: {
      value: 1000,
      message: "Ürün ismi en fazla 500 karakter olabilir.",
    },
  } satisfies RegisterOptions<AddProduct, "features">,
  category: {
    required: "Category seçimi zorunludur.",
  } satisfies RegisterOptions<AddProduct, "name">,
  price: {
    required: "Fiyat bilgisi zorunludur.",
    min: {
      value: 1,
      message: "Geçerli bir fiyat giriniz.",
    },
  } satisfies RegisterOptions<AddProduct, "price">,
  stock: {
    required: "stok bilgisi zorunludur.",
    min: {
      value: 0,
      message: "Geçerli bir stok bilgisi giriniz.",
    },
  } satisfies RegisterOptions<AddProduct, "stock">,
  boxName: {
    required: "İçerik ismi zorunludur.",
    maxLength: {
      value: 50,
      message: "İçerik ismi en fazla 50 karakterli olabilir",
    },
    minLength: {
      value: 1,
      message: "İçerik ismi zorunludur",
    },
  } satisfies RegisterOptions<ProductBoxes, "name">,
  quantity: {
    required: "Miktar bilgisi zorunludur.",
    min: {
      value: 1,
      message: "Geçerli bir stok bilgisi giriniz.",
    },
  } satisfies RegisterOptions<ProductBoxes, "quantity">,
};
