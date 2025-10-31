import { useProductStore } from "../../stores/productStore";
import { toast } from "react-toastify";
import type { AddProduct } from "../../types/Products";
import { useParams, useNavigate } from "react-router-dom";
import { useEffect } from "react";
import image from "../../assets/product.svg";
import ProductForm from "../components/products/ProductForm";
import { formatCurrency } from "../../utils/format";

export default function EditProduct() {
  const patchProduct = useProductStore((s) => s.patchProduct);
  const getById = useProductStore((s) => s.getById);
  const currentProduct = useProductStore((s) => s.currentProduct);
  const loading = useProductStore((s) => s.loading);

  const { id } = useParams<string>();
  const navigate = useNavigate();

  useEffect(() => {
    if (id) getById(id);
  }, [id, getById]);

  const handleSubmit = async (data: AddProduct) => {
    if (!id) return;
    try {
      await patchProduct(id, data);
      toast.success("Ürün başarıyla güncellendi");
      navigate(-1);
    } catch (error) {
      toast.error("Güncelleme sırasında hata oluştu");
      console.error(error);
    }
  };

  const handleCancel = () => {
    navigate(-1);
  };

  if (!currentProduct) {
    return (
      <div className="flex justify-center items-center h-screen">
        <div className="text-lg">Yükleniyor...</div>
      </div>
    );
  }

  return (
    <div className="flex gap-20 p-20">
      <div className="flex flex-col gap-4">
        <img
          className="size-48 object-cover rounded-lg"
          src={image}
          alt="Product"
        />
        <div className="space-y-2 bg-gray-50 p-4 rounded-lg">
          <h3 className="font-semibold text-lg border-b pb-2">
            Mevcut Bilgiler
          </h3>
          <div>
            <span className="text-gray-600 text-sm">İsim:</span>
            <p className="font-semibold">{currentProduct.name}</p>
          </div>
          <div>
            <span className="text-gray-600 text-sm">Açıklama:</span>
            <p className="text-sm">{currentProduct.description}</p>
          </div>
          <div>
            <span className="text-gray-600 text-sm">Özellikler:</span>
            <p className="text-sm">{currentProduct.features}</p>
          </div>
          <div>
            <span className="text-gray-600 text-sm">Kategori:</span>
            <p className="text-sm">{currentProduct.category}</p>
          </div>
          <div>
            <span className="text-gray-600 text-sm">Fiyat:</span>
            <p className="font-bold text-green-600">
              {formatCurrency(currentProduct.price)}
            </p>
          </div>
          <div>
            <span className="text-gray-600 text-sm">Stok:</span>
            <p className="font-semibold">{currentProduct.stock} adet</p>
          </div>
        </div>
      </div>
      <ProductForm
        onSubmit={handleSubmit}
        onCancel={handleCancel}
        mode="edit"
        defaultValues={currentProduct}
        isLoading={loading}
        product={currentProduct}
      />
    </div>
  );
}
