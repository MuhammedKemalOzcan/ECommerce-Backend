import { Outlet } from "react-router-dom";
import Navbar from "../client/components/common/Navbar";
import Footer from "../client/components/common/Footer";
import { useEffect } from "react";
import { useProductStore } from "../stores/productStore";
import { useCustomerStore } from "../stores/customerStore";

export default function MainLayout() {
  const getAll = useProductStore((state) => state.getAll);
  const getCustomer = useCustomerStore((state) => state.getCustomer)

  useEffect(() => {
    getAll();
    getCustomer();
  }, [getAll,getCustomer]);

  return (
    <div className="flex flex-col items-center justify-center w-screen">
      <Navbar />
      <div className="flex flex-col pb-40">
        <Outlet />
      </div>
      <Footer />
    </div>
  );
}
