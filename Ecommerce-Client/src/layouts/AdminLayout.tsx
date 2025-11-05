import { Outlet } from "react-router-dom";
import AdminNavbar from "../admin/components/common/Navbar";
import { useProductStore } from "../stores/productStore";
import { useEffect } from "react";

export default function AdminLayout() {
  const getAll = useProductStore((state) => state.getAll);

  useEffect(() => {
   void getAll();
  }, []);


  return (
    <div className="flex">
      <AdminNavbar />
      <Outlet />
    </div>
  );
}
