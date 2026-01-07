import Navbar from "../client/components/common/Navbar";
import Footer from "../client/components/common/Footer";
import ProfileNavbar from "../client/components/Profile/ProfileNavbar";
import { useEffect, useState } from "react";
import { Outlet, useLocation } from "react-router-dom";
import { useCustomerStore } from "../stores/customerStore";

export default function ProfileLayout() {
  const location = useLocation();

  const [path, setPathName] = useState(location);
  const getCustomer = useCustomerStore((s) => s.getCustomer);

  useEffect(() => {
    setPathName(location);
    getCustomer();
  }, [location, getCustomer]);

  return (
    <div className="flex flex-col w-full">
      <Navbar />
      <div className="lg:flex">
        <ProfileNavbar />
        <Outlet />
      </div>
      <Footer />
    </div>
  );
}
