import { useProductStore } from "../../stores/productStore";
import { toast } from "react-toastify";
import type { AddProduct } from "../../types/Products";
import { useParams, useNavigate } from "react-router-dom";
import { useEffect } from "react";
import image from "../../assets/product.svg";
import ProductForm from "../components/products/ProductForm";
import { formatCurrency } from "../../utils/format";

export default function EditProduct() {
  const updateProduct = useProductStore((s) => s.updateProduct);
  const getById = useProductStore((s) => s.getById);
  const currentProduct = useProductStore((s) => s.currentProduct);
  const loading = useProductStore((s) => s.loading);

  const { id } = useParams<string>();
  const navigate = useNavigate();

  useEffect(() => {
    if (id) getById(id);
  }, [id]);

  const handleSubmit = async (data: AddProduct) => {
    if (!id) return;
    try {
      await updateProduct(id, data);
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

  // Modern Loading UI - Brand Colors
  if (!currentProduct) {
    return (
      <div className="flex flex-col justify-center items-center min-h-screen bg-[#FAFAFA] px-4">
        <div className="relative flex items-center justify-center">
          <div className="absolute animate-ping inline-flex h-12 w-12 rounded-full bg-[#D87D4A] opacity-20"></div>
          <div className="relative inline-flex rounded-full h-4 w-4 bg-[#D87D4A] animate-bounce"></div>
        </div>
        <div className="mt-6 text-sm font-bold tracking-widest uppercase text-[#101010] animate-pulse">
          Ürün Verileri Yükleniyor...
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-[#FAFAFA] text-[#101010] py-10 px-4 sm:px-6 lg:px-8 font-sans">
      <div className="max-w-[1110px] mx-auto">
        {/* Header Section */}
        <div className="mb-10 lg:mb-14">
          <div className="flex flex-col md:flex-row md:items-end md:justify-between gap-4">
            <div className="flex-1">
              <h6 className="text-[#D87D4A] text-sm font-bold tracking-[10px] uppercase mb-4">
                YÖNETİM PANELİ
              </h6>
              <h1 className="text-3xl md:text-4xl font-bold uppercase tracking-wide text-[#101010]">
                Ürün Düzenle
              </h1>
            </div>
            <div className="bg-[#FFFFFF] px-6 py-3 rounded-lg border border-[#F1F1F1] shadow-sm">
              <p className="text-sm text-[#101010]/50">
                Düzenlenen ID:{" "}
                <span className="font-bold text-[#D87D4A] ml-1">
                  #{currentProduct.id || id}
                </span>
              </p>
            </div>
          </div>
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-12 gap-8 items-start">
          {/* Sol Kolon - Görsel & Özet (Desktop: 4/12 width) */}
          <div className="space-y-8 lg:col-span-4">
            {/* Ürün Görsel Kartı */}
            <div className="bg-[#FFFFFF] rounded-xl overflow-hidden shadow-lg shadow-[#101010]/5 border border-[#F1F1F1] group hover:border-[#D87D4A]/30 transition-colors duration-300">
              <div className="p-8 flex flex-col items-center text-center bg-[#F1F1F1]/30">
                <div className="w-full flex justify-between items-center mb-6">
                  <h2 className="text-xs font-bold text-[#101010]/40 uppercase tracking-widest">
                    Önizleme
                  </h2>
                  <span className="inline-flex items-center rounded-md bg-[#D87D4A] px-3 py-1 text-[10px] font-bold uppercase tracking-wider text-white">
                    Yayında
                  </span>
                </div>

                <div className="relative w-full aspect-square flex items-center justify-center mb-4">
                  <div className="absolute inset-0 bg-[#D87D4A]/5 rounded-full scale-75 group-hover:scale-90 transition-transform duration-500 ease-out"></div>
                  <img
                    className="relative w-48 h-48 sm:w-56 sm:h-56 object-contain z-10 drop-shadow-xl transition-transform duration-300 group-hover:-translate-y-2"
                    src={image}
                    alt="Product Preview"
                  />
                </div>
              </div>
            </div>

            {/* Mevcut Bilgiler Özeti */}
            <div className="bg-[#FFFFFF] rounded-xl shadow-sm border border-[#F1F1F1]">
              <div className="px-6 py-5 border-b border-[#F1F1F1]">
                <h3 className="text-base font-bold uppercase tracking-wider text-[#101010]">
                  Mevcut Veriler
                </h3>
              </div>
              <div className="px-6 py-2">
                <dl className="divide-y divide-[#F1F1F1]">
                  <div className="py-4">
                    <dt className="text-xs font-bold text-[#101010]/50 uppercase mb-1">
                      Ürün İsmi
                    </dt>
                    <dd className="text-sm font-bold text-[#101010]">
                      {currentProduct.name}
                    </dd>
                  </div>

                  <div className="py-4 grid grid-cols-2 gap-4">
                    <div>
                      <dt className="text-xs font-bold text-[#101010]/50 uppercase mb-1">
                        Kategori
                      </dt>
                      <dd className="text-sm font-medium text-[#101010] capitalize">
                        {currentProduct.category}
                      </dd>
                    </div>
                    <div>
                      <dt className="text-xs font-bold text-[#101010]/50 uppercase mb-1">
                        Stok
                      </dt>
                      <dd className="text-sm font-medium text-[#101010]">
                        {currentProduct.stock}{" "}
                        <span className="text-[#101010]/40 text-xs">adt</span>
                      </dd>
                    </div>
                  </div>

                  <div className="py-4">
                    <dt className="text-xs font-bold text-[#101010]/50 uppercase mb-1">
                      Fiyat
                    </dt>
                    <dd className="text-lg font-bold text-[#D87D4A]">
                      {formatCurrency(currentProduct.price)}
                    </dd>
                  </div>

                  <div className="py-4">
                    <dt className="text-xs font-bold text-[#101010]/50 uppercase mb-1">
                      Açıklama
                    </dt>
                    <dd className="text-sm leading-relaxed text-[#101010]/70 line-clamp-3">
                      {currentProduct.description}
                    </dd>
                  </div>
                </dl>
              </div>
            </div>
          </div>

          {/* Sağ Kolon - Düzenleme Formu (Desktop: 8/12 width) */}
          <div className="lg:col-span-8">
            <div className="bg-[#FFFFFF] rounded-xl shadow-lg shadow-[#101010]/5 border border-[#F1F1F1] overflow-hidden">
              <div className="px-6 py-6 sm:px-8 border-b border-[#F1F1F1] flex flex-col sm:flex-row sm:items-center justify-between gap-4 bg-[#FFFFFF]">
                <div>
                  <h3 className="text-lg font-bold uppercase tracking-wide text-[#101010]">
                    Ürün Bilgilerini Güncelle
                  </h3>
                  <p className="mt-1 text-sm text-[#101010]/50">
                    Form alanlarını eksiksiz doldurduğunuzdan emin olun.
                  </p>
                </div>
              </div>

              <div className="p-6 sm:p-8 bg-[#FFFFFF]">
                {/* ProductForm wrapper to inject focus styles via context if needed, though most styling is inside the component */}
                <div className="[&_input:focus]:ring-1 [&_input:focus]:ring-[#D87D4A] [&_input:focus]:border-[#D87D4A] [&_textarea:focus]:ring-[#D87D4A] [&_textarea:focus]:border-[#D87D4A]">
                  <ProductForm
                    onSubmit={handleSubmit}
                    onCancel={handleCancel}
                    mode="edit"
                    defaultValues={currentProduct}
                    isLoading={loading}
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
