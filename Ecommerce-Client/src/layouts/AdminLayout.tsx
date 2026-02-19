import { Outlet } from "react-router-dom";
import AdminNavbar from "../admin/components/common/Navbar";
import { useProductStore } from "../stores/productStore";
import { useEffect } from "react";
import { ProtectedAdminRoute } from "../utils/ProtectedRoute";
import { useNotificationStore } from "../stores/NotificationStore";
import { ToastContainer } from "react-toastify";

export default function AdminLayout() {
  const getAll = useProductStore((state) => state.getAll);
  const startConnection = useNotificationStore(
    (state) => state.startConnection,
  );

  useEffect(() => {
    void getAll();
  }, [getAll]);

  useEffect(() => {
    startConnection();
  }, []);

  return (
    <ProtectedAdminRoute>
      <div className="flex">
        <AdminNavbar />
        <Outlet />
        <ToastContainer />
      </div>
    </ProtectedAdminRoute>
  );
}
