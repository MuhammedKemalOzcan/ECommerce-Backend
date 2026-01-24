import { useEffect, useState } from "react";
import { useSearchParams, useNavigate } from "react-router-dom";

export default function PaymentSuccess() {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  const [orderCode, setOrderCode] = useState<string | null>(null);

  useEffect(() => {
    // URL'den orderCode parametresini çekiyoruz
    const code = searchParams.get("orderCode");
    if (code) {
      setOrderCode(code);
    }
  }, [searchParams]);

  return (
    <div className=" bg-gray-50 flex flex-col items-center px-4">
      {/* Kart Yapısı */}
      <div className="bg-white p-8 rounded-2xl shadow-xl max-w-md w-full text-center border border-gray-100">
        {/* Hareketli Onay İkonu */}
        <div className="mb-6 flex justify-center">
          <div className="h-24 w-24 bg-green-100 rounded-full flex items-center justify-center animate-bounce-slow">
            <svg
              className="h-12 w-12 text-green-600"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth={3}
                d="M5 13l4 4L19 7"
              />
            </svg>
          </div>
        </div>
        {/* Başlıklar */}
        <h1 className="text-3xl font-extrabold text-gray-900 mb-2">
          Ödeme Başarılı!
        </h1>
        <p className="text-gray-500 mb-8">
          Siparişiniz başarıyla alındı. Teşekkür ederiz.
        </p>

        {/* Sipariş Kodu Alanı */}
        {orderCode && (
          <div className="bg-gray-50 border border-dashed border-gray-300 rounded-lg p-4 mb-8">
            <p className="text-sm text-gray-500 mb-1">Sipariş Kodunuz:</p>
            <p className="text-xl font-mono font-bold text-gray-800 tracking-wider">
              #{orderCode}
            </p>
          </div>
        )}

        {/* Butonlar */}
        <div className="flex flex-col gap-3">
          <button
            onClick={() => navigate("/profile/orders")}
            className="w-full bg-black text-white py-3.5 rounded-xl font-medium hover:bg-gray-800 transition-all duration-200 shadow-lg shadow-gray-200"
          >
            Siparişimi Görüntüle
          </button>

          <button
            onClick={() => navigate("/")}
            className="w-full bg-white text-gray-700 border border-gray-200 py-3.5 rounded-xl font-medium hover:bg-gray-50 transition-all duration-200"
          >
            Alışverişe Devam Et
          </button>
        </div>
      </div>
    </div>
  );
}
