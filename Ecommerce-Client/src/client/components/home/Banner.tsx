import { useEffect } from "react";
import { useProductStore } from "../../../stores/productStore";
import { useShallow } from "zustand/shallow";

export default function HomeBanner() {
  const { getAll, products } = useProductStore(
    useShallow((s) => ({
      getAll: s.getAll,
      products: s.products,
    }))
  );

  useEffect(() => {
    getAll;
  }, [getAll]);

  const product = products[1];

  return (
    <div className="flex items-center justify-center  h-[729px] text-black">
      <div className="flex items-center justify-center w-[80%]">
        <div className="flex flex-col w-[60%] gap-4 ">
          <p className="text-black opacity-[50%]  ">N E W P R O D U C T</p>
          <h1 className="text-[56px]">{product?.name}</h1>
          <p>{product?.description}</p>
          <button className="btn-1 w-[30%] p-2">See Product</button>
        </div>
        {product?.productGalleries?.map(
          (g,index) =>
            g.isPrimary === true && (
              <div key={index} className="ml-20">
                <img src={g.path} className="w-[708px] h-full"/>
              </div>
            )
        )}
      </div>
    </div>
  );
}
