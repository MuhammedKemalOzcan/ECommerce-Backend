import { Outlet } from "react-router-dom";
import Navbar from "../client/components/common/Navbar";
import Footer from "../client/components/common/Footer";
import { useEffect } from "react";
import { useProductStore } from "../stores/productStore";

export default function MainLayout() {
  const getAll = useProductStore((state) => state.getAll);

  useEffect(() => {
    getAll();
  }, [getAll]);

  return (
    <div>
      <Navbar />
      <Outlet />
      <Footer />
    </div>
  );
}
