import MenuItem from "../components/home/MenuItem";
import StoreIntro from "../components/home/StoreIntro";
import ListProduct from "../components/products/ListProduct";

export default function Earphones() {
  return (
    <div
      id="earphones"
      className="flex flex-col items-center justify-center w-[1110px] gap-40"
    >
      <ListProduct />
      <MenuItem />
      <StoreIntro />
    </div>
  );
}
