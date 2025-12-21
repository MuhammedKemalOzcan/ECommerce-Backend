import ShippingDetails from "../components/checkout/ShippingDetails";
import DeliveryMethod from "../components/checkout/DeliveryMethod";
import Payment from "../components/checkout/Payment";
import OrderSummary from "../components/checkout/OrderSummary";

export default function Checkout() {
  return (
    // 1. DIŞ KATMAN: Arka plan rengi ve dikey boşluklar
    <div className="w-full min-h-screen  py-10 lg:py-20">
      
      {/* 2. ORTALAYICI KONTEYNER (Sihirli Kısım Burası) 
          - max-w-[1100px]: İçerik en fazla 1100px genişliğe ulaşsın (çok yayılmasın).
          - mx-auto: Soldan ve sağdan otomatik eşit boşluk bırakarak ortalar.
          - px-5: Mobildeyken kenarlara sıfır yapışmasın diye minik boşluk.
      */}
      <div className="max-w-[1100px] mx-auto px-5">

        {/* 3. FLEX YAPISI
            - gap-12: Tailwind'de 1 birim 4px'tir. 12 * 4 = 48px boşluk.
            - items-start: Sticky'nin çalışması için gerekli (yükseklikleri eşitlemez).
        */}
        <div className="flex flex-col lg:flex-row gap-12 items-start relative">
          
          {/* --- SOL KOLON --- 
              - flex-1: Kalan boşluğun tamamını kapla.
          */}
          <div className="w-full lg:flex-1 flex flex-col gap-6">
            <ShippingDetails />
            <DeliveryMethod />
            <Payment />
          </div>

          {/* --- SAĞ KOLON (Sticky) --- 
              - w-[350px]: Order Summary için sabit, ideal genişlik.
              - flex-shrink-0: Alan daralırsa sakın küçülme, sabit kal.
          */}
          <div className="w-full lg:w-[350px] sticky top-10 h-fit flex-shrink-0">
            <OrderSummary />
          </div>

        </div>
      </div>
    </div>
  );
}