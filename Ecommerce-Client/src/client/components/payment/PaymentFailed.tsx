import { useEffect, useState } from "react";
import { useSearchParams, useNavigate } from "react-router-dom"; 

export default function PaymentFailed() {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  useEffect(() => {
    // Backend'den gönderdiğin ?reason= parametresini okuyoruz
    const reason = searchParams.get("reason");
    if (reason) {
      setErrorMessage(reason);
    }
  }, [searchParams]);

  return (
    <div className="min-h-screen bg-gray-50 flex flex-col justify-center items-center px-4">
      {/* Kart Yapısı */}
      <div className="bg-white p-8 rounded-2xl shadow-xl max-w-md w-full text-center border border-red-50 relative overflow-hidden">
        
        {/* Üst Kısımdaki İnce Kırmızı Çizgi (Estetik detay) */}
        <div className="absolute top-0 left-0 w-full h-1.5 bg-red-500"></div>

        {/* Hata İkonu (Hareketli - Pulse Efekti) */}
        <div className="mb-6 flex justify-center">
          <div className="h-24 w-24 bg-red-50 rounded-full flex items-center justify-center animate-pulse">
             {/* X İkonu */}
            <svg 
              className="h-10 w-10 text-red-500" 
              fill="none" 
              viewBox="0 0 24 24" 
              stroke="currentColor"
            >
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" />
            </svg>
          </div>
        </div>

        {/* Başlık */}
        <h1 className="text-2xl font-bold text-gray-900 mb-2">
          Ödeme Tamamlanamadı
        </h1>
        <p className="text-gray-500 mb-6">
          Maalesef işleminiz sırasında bir sorun oluştu. Kartınızdan herhangi bir ücret tahsil edilmedi.
        </p>

        {/* Hata Mesajı Kutusu */}
        <div className="bg-red-50 border border-red-100 rounded-lg p-4 mb-8 text-left">
          <div className="flex items-start gap-3">
            <svg className="w-5 h-5 text-red-600 mt-0.5 flex-shrink-0" fill="none" viewBox="0 0 24 24" stroke="currentColor">
               <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 8v4m0 4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
            </svg>
            <div>
              <p className="text-xs font-bold text-red-800 uppercase tracking-wide">Hata Nedeni</p>
              <p className="text-sm text-red-700 mt-1 font-medium">
                {errorMessage || "Bilinmeyen bir hata oluştu. Lütfen bankanızla iletişime geçin."}
              </p>
            </div>
          </div>
        </div>

        {/* Aksiyon Butonları */}
        <div className="flex flex-col gap-3">
          <button
            onClick={() => navigate("/checkout")}
            className="w-full bg-red-600 text-white py-3.5 rounded-xl font-medium hover:bg-red-700 transition-all duration-200 shadow-lg shadow-red-100 flex justify-center items-center gap-2"
          >
            {/* Retry Icon */}
            <svg className="w-5 h-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 4v5h.582m15.356 2A8.001 8.001 0 004.582 9m0 0H9m11 11v-5h-.581m0 0a8.003 8.003 0 01-15.357-2m15.357 2H15" />
            </svg>
            Tekrar Dene
          </button>
          
          <button
            onClick={() => navigate("/cart")}
            className="w-full bg-white text-gray-700 border border-gray-200 py-3.5 rounded-xl font-medium hover:bg-gray-50 transition-all duration-200"
          >
            Sepete Dön
          </button>
        </div>

        {/* Yardım Linki */}
        <div className="mt-8 pt-6 border-t border-gray-100">
           <p className="text-sm text-gray-400">
             Sorun devam ederse? <span onClick={() => navigate('/contact')} className="text-gray-600 font-semibold cursor-pointer underline hover:text-black">Bize ulaşın</span>
           </p>
        </div>

      </div>
    </div>
  );
}