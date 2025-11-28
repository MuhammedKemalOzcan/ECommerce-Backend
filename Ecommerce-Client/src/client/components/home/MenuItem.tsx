import { ArrowBigRight } from "lucide-react";
import { navItems } from "../../../utils/navItems";
import { useNavigate } from "react-router-dom";

export default function MenuItem() {
  const navs = navItems;
  const navigate = useNavigate();

  return (
    <div className="flex w-full gap-6 px-6 py-8">
      {navs.map(
        (navItem, index) =>
          navItem.item.toLowerCase() !== "home" && (
            <button
              onClick={() => navigate(navItem.path)}
              key={index}
              className="flex flex-col items-center justify-end flex-1 bg-gradient-to-b from-gray-100 to-gray-200 rounded-2xl h-80 relative shadow-lg hover:shadow-2xl transition-all duration-300 hover:scale-105 cursor-pointer group"
            >
              <img
                src={navItem.image}
                className="size-40 absolute -top-10 object-contain transition-transform duration-300 group-hover:scale-125"
              />
              <div className="flex flex-col items-center gap-4 pb-8 z-10 bg-gradient-to-t from-white via-white to-transparent pt-20 w-full">
                <h1 className="text-xl font-bold text-gray-800">
                  {navItem.item}
                </h1>
                <div
                  onClick={() => navigate(navItem.path)}
                  className="flex items-center gap-2 px-6 py-2 bg-orange-500 text-white rounded-lg font-semibold hover:bg-orange-600 transition-colors duration-200 active:scale-95"
                >
                  <p>SHOP</p>
                  <span>
                    <ArrowBigRight size={20} />
                  </span>
                </div>
              </div>
            </button>
          )
      )}
    </div>
  );
}
