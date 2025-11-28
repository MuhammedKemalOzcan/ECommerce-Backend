import HomeBanner from "../components/home/Banner";
import MenuItem from "../components/home/MenuItem";
import ProductShowcase from "../components/home/ProductShowcase";

import StoreIntro from "../components/home/StoreIntro";

export default function Home() {
  return (
    <div className=" w-[1110px] flex flex-col items-center justify-center gap-40">
      <HomeBanner />
      <MenuItem />
      <ProductShowcase />
      <StoreIntro />
    </div>
  );
}
