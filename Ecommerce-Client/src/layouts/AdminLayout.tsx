import { Outlet } from "react-router-dom";
import AdminNavbar from "../admin/components/common/Navbar";
import { useProductStore } from "../stores/productStore";
import { useEffect } from "react";
import { ProtectedAdminRoute } from "../utils/ProtectedRoute";

export default function AdminLayout() {
  const getAll = useProductStore((state) => state.getAll);

  useEffect(() => {
    void getAll();
  }, [getAll]);

  return (
    <ProtectedAdminRoute>
      <div className="flex">
        <AdminNavbar />
        <Outlet />
      </div>
    </ProtectedAdminRoute>
  );
}
