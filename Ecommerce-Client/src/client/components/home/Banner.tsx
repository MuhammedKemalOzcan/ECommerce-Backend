import { useEffect } from "react";
import { useProductStore } from "../../../stores/productStore";
import { useShallow } from "zustand/shallow";
import { useNavigate } from "react-router-dom";

export default function HomeBanner() {
  const { getAll, products } = useProductStore(
    useShallow((s) => ({
      getAll: s.getAll,
      products: s.products,
    }))
  );

  const navigate = useNavigate();

  useEffect(() => {
    getAll;
  }, [getAll]);

  const product = products[1];

  return (
    <div className="flex items-center justify-between text-black">
      <div className="flex items-center justify-center w-full">
        <div className="flex flex-col w-[60%] gap-4 ">
          <p className="text-black opacity-[50%]  ">N E W P R O D U C T</p>
          <h1 className="text-[56px]">{product?.name}</h1>
          <p>{product?.description}</p>
          <button
            onClick={() => navigate(`/${product.category}/${product.id}`)}
            className="btn-1 w-[30%] p-2"
          >
            See Product
          </button>
        </div>
        {product?.productGalleries?.map(
          (g, index) =>
            g.isPrimary === true && (
              <div key={index} className="ml-20">
                <img src={g.path} className="w-[708px] h-full" />
              </div>
            )
        )}
      </div>
    </div>
  );
}
