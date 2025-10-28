import type { RegisterOptions } from "react-hook-form";
import type { AddProduct } from "../types/Products";

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
      value: 500,
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
};
