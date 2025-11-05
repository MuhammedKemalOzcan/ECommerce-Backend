import { useFieldArray, useForm, type SubmitHandler } from "react-hook-form";
import type { AddProduct } from "../../../types/Products";
import { toast } from "react-toastify";
import { PRODUCT_VALIDATION_RULES } from "../../../schemes/ProductSchema";
import { PRODUCT_CATEGORIES } from "../../../constants/catalog";
import { Package, Plus, Trash2 } from "lucide-react";
import FormField from "./FormField";

type Props = {
  onSubmit: (data: AddProduct) => Promise<void>;
  onCancel: () => void;
  mode: "create" | "edit";
  defaultValues?: Partial<AddProduct>;
  isLoading?: boolean;
};

export default function ProductForm({
  onSubmit,
  onCancel,
  mode,
  isLoading,
  defaultValues,
}: Props) {
  const {
    register,
    handleSubmit,
    control,
    formState: { isDirty, errors, isSubmitting },
  } = useForm<AddProduct>({
    defaultValues: defaultValues || {
      name: "",
      description: "",
      features: "",
      category: "",
      price: 0,
      stock: 0,
      productBoxes: [],
    },
  });

  const { fields, append, remove } = useFieldArray({
    control,
    name: "productBoxes",
  });

  const addBoxItems = () => {
    append({ name: "", quantity: 1 });
  };

  const handleFormSubmit: SubmitHandler<AddProduct> = async (data) => {
    if (!isDirty && mode === "edit") {
      toast.info("Hiçbir değişiklik yapılmadı");
      return;
    }
    try {
      console.log("Ürün Verisi:", data);
      await onSubmit(data);
    } catch (error) {
      console.error("Form Submission Error", error);
    }
  };

  const handleCancel = () => {
    if (isDirty && mode === "edit") {
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
      <FormField
        id="name"
        label="İsim"
        required
        type="text"
        error={errors.name}
        {...register("name", PRODUCT_VALIDATION_RULES.name)}
      />
      {/* AÇIKLAMA */}
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
      {/* FEATURES */}
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
      {/* KATEGORİ */}
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
        <FormField
          id="price"
          label="Fiyat ($)"
          required
          type="number"
          error={errors.price}
          {...register("price", PRODUCT_VALIDATION_RULES.price)}
        />
        <FormField
          id="stock"
          label="Stok"
          required
          type="number"
          error={errors.stock}
          {...register("stock", PRODUCT_VALIDATION_RULES.stock)}
        />
      </div>
      {/* Box Item Ekleme */}
      {mode === "create" && (
        <div className="border-t pt-6">
          <div className="flex items-center justify-between mb-4">
            <div className="flex items-center gap-2">
              <Package size={20} className="text-blue-600" />
              <h3 className="text-lg font-semibold text-gray-900">
                Kutu İçeriği
              </h3>
              {fields.length > 0 && (
                <span className="inline-flex items-center px-2 py-1 rounded-full text-xs font-medium bg-blue-100 text-blue-800">
                  {fields.length} öğe
                </span>
              )}
            </div>
            {/* Kutu İçeriği Ekle Butonu */}
            <button
              type="button"
              onClick={addBoxItems}
              className="inline-flex items-center gap-2 px-4 py-2 text-sm font-medium text-blue-600 bg-blue-50 rounded-md hover:bg-blue-100 transition-colors"
            >
              <Plus size={16} />
              İçerik Ekle
            </button>
          </div>
          {/* Box Item Listesi */}
          {fields.length > 0 && (
            <div className="space-y-3">
              {fields.map((box, index) => (
                <div
                  key={box.id}
                  className="flex items-start gap-3 p-4 bg-gray-50 rounded-lg border border-gray-200"
                >
                  {/* Index Badge */}
                  <div className="flex-shrink-0 w-8 h-8 flex items-center justify-center bg-blue-600 text-white rounded-full text-sm font-medium mt-6">
                    {index + 1}
                  </div>
                  {/*Form Field */}
                  <div className="flex-1 grid grid-cols-1 md:grid-cols-2 gap-3">
                    <FormField
                      id={`productBoxes.${index}.name`}
                      label="Name"
                      required
                      type="text"
                      error={errors.productBoxes?.[index]?.name}
                      {...register(
                        `productBoxes.${index}.name`,
                        PRODUCT_VALIDATION_RULES.boxName
                      )}
                    />
                    <FormField
                      id={`productBoxes.${index}.quantity`}
                      label="Quantity"
                      required
                      type="number"
                      error={errors.productBoxes?.[index]?.quantity}
                      {...register(
                        `productBoxes.${index}.quantity`,
                        PRODUCT_VALIDATION_RULES.quantity
                      )}
                    />
                  </div>
                  <button
                    type="button"
                    onClick={() => remove(index)}
                    className="flex-shrink p-2 text-red-600 hover:bg-red-50 rounded-md transition-colors mt-6"
                    aria-label={`${index + 1}. öğeyi sil`}
                  >
                    <Trash2 size={26} />
                  </button>
                </div>
              ))}
            </div>
          )}
          {/* Boş State */}
          {fields.length === 0 && (
            <div className="text-center py-8 bg-gray-50 rounded-lg border-2 border-dashed border-gray-300">
              <Package size={48} className="mx-auto text-gray-400 mb-2" />
              <p className="text-gray-600 mb-4">Henüz kutu içeriği eklenmedi</p>
              <button
                type="button"
                onClick={addBoxItems}
                className="inline-flex items-center gap-2 px-4 py-2 text-sm font-medium text-blue-600 bg-blue-50 rounded-md hover:bg-blue-100 transition-colors"
              >
                <Plus size={16} />
                İlk İçeriği Ekle
              </button>
            </div>
          )}
        </div>
      )}

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

      {isDirty && mode === "edit" && (
        <p className="text-sm text-amber-600 text-center">
          * Kaydedilmemiş değişiklikler var
        </p>
      )}
    </form>
  );
}
