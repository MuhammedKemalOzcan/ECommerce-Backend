import ProductForm from "../components/products/ProductForm";
import { useProductStore } from "../../stores/productStore";
import type { AddProduct } from "../../types/Products";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import AddProductImage from "../components/products/AddProductImage";
import { useEffect, useMemo, useState } from "react";
import axios from "axios";
import type { UploadStatus } from "../../types/Gallery";

export default function CreateProduct() {
  const [files, setFiles] = useState<File[]>([]);
  const [status, setStatus] = useState<UploadStatus>("initial");

  const createProduct = useProductStore((s) => s.createProduct);

  const navigate = useNavigate();

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (!e.target.files) return;
    setStatus("initial");
    const list = e.target.files;
    setFiles(list ? Array.from(list) : []);
  };

  //Eklenen resmi görüntüleme
  const previewUrls = useMemo(() => {
    if (files?.length === 0) return [];
    return files.map((file) => URL.createObjectURL(file));
  }, [files]);

  //Bellekte memory leak olmaması için oluşturulan url'leri unmount anında revoke ediyor.
  useEffect(() => {
    return () => {
      previewUrls?.map((u) => URL.revokeObjectURL(u));
    };
  }, [previewUrls]);

  const handleSubmit = async (data: AddProduct) => {
    try {
      await createProduct(data);
      if (files) {
        setStatus("uploading");

        const formData = new FormData();
        [...files].forEach((file) => {
          formData.append("files", file);
        });
        try {
          const result = await axios.post("https://httpbin.org/post", formData);
          setStatus("success");
          setFiles([]);
        } catch (error) {
          console.error(error);
          setStatus("fail");
        }
      }
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
    <div className="py-40 h-full w-full flex justify-center items-center gap-12">
      <ProductForm
        mode="create"
        onSubmit={handleSubmit}
        onCancel={handleCancel}
      />
      <AddProductImage
        files={files}
        onFileChange={handleFileChange}
        previewUrls={previewUrls}
      />
    </div>
  );
}
