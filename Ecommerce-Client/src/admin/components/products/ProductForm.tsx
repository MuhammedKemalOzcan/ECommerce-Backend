import { useForm, type SubmitHandler } from "react-hook-form";
import type { AddProduct, Products } from "../../../types/Products";
import { toast } from "react-toastify";
import { PRODUCT_VALIDATION_RULES } from "../../../schemes/ProductSchema";
import { useEffect } from "react";
import { PRODUCT_CATEGORIES } from "../../../constants/catalog";

type Props = {
  onSubmit: (data: AddProduct) => Promise<void>;
  onCancel: () => void;
  mode: "create" | "edit";
  defaultValues?: Partial<AddProduct>;
  isLoading?: boolean;
  product?: Products;
};

export default function ProductForm({
  onSubmit,
  onCancel,
  mode,
  isLoading,
  product,
}: Props) {
  const {
    register,
    handleSubmit,
    reset,
    formState: { isDirty, errors, isSubmitting },
  } = useForm<AddProduct>();

  // Form'u mevcut ürün verileriyle doldur
  useEffect(() => {
    if (product) {
      reset({
        ...product,
        productBox: product.productBoxes
          ? product.productBoxes
          : [{ name: "", quantity: 1 }],
      });
    }
  }, [product, reset]);

  const handleFormSubmit: SubmitHandler<AddProduct> = async (data) => {
    if (!isDirty) {
      toast.info("Hiçbir değişiklik yapılmadı");
      return;
    }
    try {
      await onSubmit(data);
    } catch (error) {
      console.error("Form Submission Error", error);
    }
  };

  const handleCancel = () => {
    if (isDirty) {
      const confirmCancel = window.confirm(
        "Değişiklikler kaydedilmedi. Çıkmak istediğinize emin misiniz?"
      );
      if (!confirmCancel) return;
    }
    onCancel();
  };

  const isFormDisabled = isLoading || isSubmitting;

  return (
    <form
      className="flex flex-col flex-1 max-w-xl"
      onSubmit={handleSubmit(handleFormSubmit)}
      noValidate
    >
      {mode === "edit" ? (
        <h2 className="text-2xl font-bold mb-4">Ürün Düzenle</h2>
      ) : (
        <h2 className="text-2xl font-bold mb-4">Ürün Ekle</h2>
      )}
      <div className="flex flex-col gap-1">
        <label htmlFor="name" className="font-medium">
          İsim
        </label>
        <input
          disabled={isFormDisabled}
          id="name"
          {...register("name", PRODUCT_VALIDATION_RULES.name)}
          type="text"
          placeholder="Ürün İsmi"
          className="input"
        />
        {errors.name && (
          <span id="name-error" className="text-sm text-red-600" role="alert">
            {errors.name.message}
          </span>
        )}
      </div>

      <div className="flex flex-col gap-1">
        <label htmlFor="description" className="font-medium">
          Açıklama
        </label>
        <textarea
          disabled={isFormDisabled}
          id="description"
          rows={3}
          {...register("description", PRODUCT_VALIDATION_RULES.description)}
          placeholder="Ürün Açıklaması"
          className="input resize-none"
        />
        {errors.description && (
          <span className="text-sm text-red-600">
            {errors.description.message}
          </span>
        )}
      </div>

      <div className="flex flex-col gap-1">
        <label htmlFor="features" className="font-medium">
          Özellikler
        </label>
        <textarea
          disabled={isFormDisabled}
          id="features"
          rows={3}
          {...register("features", PRODUCT_VALIDATION_RULES.features)}
          placeholder="Ürün Özellikleri"
          className="input resize-none"
        />
        {errors.features && (
          <span className="text-sm text-red-600">
            {errors.features.message}
          </span>
        )}
      </div>

      <div className="flex flex-col gap-1">
        <label htmlFor="category" className="font-medium">
          Kategori
        </label>
        <select
          disabled={isFormDisabled}
          id="category"
          {...register("category", PRODUCT_VALIDATION_RULES.category)}
          className="input"
        >
          {PRODUCT_CATEGORIES.map((cat, index) => (
            <option key={index}>{cat}</option>
          ))}
        </select>
        {errors.category && (
          <span className="text-sm text-red-600">
            {errors.category.message}
          </span>
        )}
      </div>

      <div className="flex gap-4">
        <div className="flex flex-col gap-1 flex-1">
          <label htmlFor="price" className="font-medium">
            Fiyat ($)
          </label>
          <input
            disabled={isFormDisabled}
            id="price"
            {...register("price", PRODUCT_VALIDATION_RULES.price)}
            type="number"
            step="0.01"
            placeholder="0.00"
            className="input"
          />
          {errors.price && (
            <span className="text-sm text-red-600">{errors.price.message}</span>
          )}
        </div>
        <div className="flex flex-col gap-1 flex-1">
          <label htmlFor="stock" className="font-medium">
            Stok
          </label>
          <input
            disabled={isFormDisabled}
            id="stock"
            {...register("stock", PRODUCT_VALIDATION_RULES.stock)}
            type="number"
            className="input"
          />
          {errors.stock && (
            <span className="text-sm text-red-600">{errors.stock.message}</span>
          )}
        </div>
      </div>
      {/* Box Item Ekleme */}
      {mode === "create" && <div></div>}

      <div className="flex gap-3 mt-4">
        <button
          disabled={isFormDisabled}
          type="submit"
          className="bg-blue-500 px-8 py-2 flex-1 rounded-lg text-white font-medium hover:bg-blue-600 transition disabled:bg-gray-300 disabled:cursor-not-allowed"
        >
          {mode === "edit" ? "Güncelle" : "Ürün Ekle"}
        </button>
        <button
          disabled={isFormDisabled}
          type="button"
          onClick={handleCancel}
          className="bg-gray-500 px-8 py-2 flex-1 rounded-lg text-white font-medium hover:bg-gray-600 transition disabled:bg-gray-300 disabled:cursor-not-allowed"
        >
          İptal
        </button>
      </div>

      {isDirty && (
        <p className="text-sm text-amber-600 text-center">
          * Kaydedilmemiş değişiklikler var
        </p>
      )}
    </form>
  );
}
