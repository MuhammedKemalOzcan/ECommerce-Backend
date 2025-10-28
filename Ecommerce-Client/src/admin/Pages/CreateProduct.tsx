import ProductForm from "../components/products/ProductForm";
import { useProductStore } from "../../stores/productStore";
import type { AddProduct } from "../../types/Products";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

export default function CreateProduct() {
  const createProduct = useProductStore((s) => s.createProduct);

  const navigate = useNavigate();

  const handleSubmit = async (data: AddProduct) => {
    try {
      await createProduct(data);
      toast.success("Ürün Başarıyla Oluşturuldu");
      navigate(-1);
    } catch (error) {
      console.log(error);
      toast.error("Hata!");
    }
  };

  const handleCancel = () => {
    navigate(-1);
  };

  return (
    <div className="p-12 flex gap-12">
      <ProductForm
        mode="create"
        onSubmit={handleSubmit}
        onCancel={handleCancel}
      />
    </div>
  );
}
