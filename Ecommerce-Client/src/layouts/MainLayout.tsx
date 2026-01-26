import { Outlet } from "react-router-dom";
import Navbar from "../client/components/common/Navbar";
import Footer from "../client/components/common/Footer";
import { useEffect } from "react";
import { useProductStore } from "../stores/productStore";
import { useAuthStore } from "../auth/authStore";

export default function MainLayout() {
  const getAll = useProductStore((state) => state.getAll);
  const user = useAuthStore((state) => state.user);
  const clearAuth = useAuthStore((state) => state.clearAuth);


  if (!user) clearAuth();

  useEffect(() => {
    getAll();
  }, [getAll]);

  return (
    <div className="flex flex-col w-full min-h-screen bg-[#FAFAFA] font-sans text-[#101010]">
      {/* Navbar Container */}
      <div className="w-full relative z-50">
        <Navbar />
      </div>

      <main className="flex-1 w-full">
        <div className="w-full lg:max-w-[90%] mx-auto px-4 py-10">
          <Outlet />
        </div>
      </main>

      <div className="w-full">
        <Footer />
      </div>
    </div>
  );
}
