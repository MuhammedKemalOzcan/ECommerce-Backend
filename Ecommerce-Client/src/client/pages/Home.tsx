import HomeBanner from "../components/home/Banner";
import MenuItem from "../components/home/MenuItem";
import ProductShowcase from "../components/home/ProductShowcase";
import StoreIntro from "../components/home/StoreIntro";

export default function Home() {
  return (
    <div className="w-full flex flex-col items-center justify-center lg:gap-40 gap-12">
      <HomeBanner />
      <MenuItem />
      <ProductShowcase />
      <StoreIntro />
    </div>
  );
}
