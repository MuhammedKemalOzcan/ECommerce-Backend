import Navbar from "../client/components/common/Navbar";
import Footer from "../client/components/common/Footer";
import ProfileInformation from "../client/components/Profile/ProfileInformation";
import ProfileNavbar from "../client/components/Profile/ProfileNavbar";
import Orders from "../client/components/Profile/Orders";
import ProfileSettings from "../client/components/Profile/ProfileSettings";
import { useEffect, useState } from "react";
import { useLocation } from "react-router-dom";

export default function ProfileLayout() {
  const location = useLocation();

  const [path, setPathName] = useState(location);

  useEffect(() => {
    setPathName(location);
  }, [location]);

  return (
    <div className="flex flex-col w-screen">
      <Navbar />
      <div className="flex">
        <ProfileNavbar />
        {path.pathname.includes("account") && <ProfileInformation />}
        {path.pathname.includes("orders") && <Orders />}
        {path.pathname.includes("settings") && <ProfileSettings />}
      </div>

      <Footer />
    </div>
  );
}
