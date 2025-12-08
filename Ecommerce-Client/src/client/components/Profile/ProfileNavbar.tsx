import { Avatar } from "@mui/material";
import { Package, Settings, User } from "lucide-react";
import { Link } from "react-router-dom";

export default function ProfileNavbar() {
  const profileLinks = [
    { name: "Account", path: "/profile/account", icon: <User /> },
    { name: "Orders", path: "/profile/orders", icon: <Package /> },
    { name: "Settings", path: "/profile/settings", icon: <Settings /> },
  ];

  return (
    <div className="flex flex-col w-80 gap-4 p-4 h-screen bg-[#141414] items-center text-white rounded-r-lg">
      <div className="flex gap-4 items-center mb-8">
        <Avatar />
        <div>
          <p className="text-sm">Hello</p>
          <p className="font-bold">Muhammed Kemal Özcan</p>
        </div>
      </div>

      {profileLinks.map((link) => (
        <Link
          className="flex w-full h-12 gap-2 items-center px-4 focus:bg-orange-500 rounded-md"
          key={link.path}
          to={link.path}
        >
          <span>{link.icon}</span>
          <p>{link.name}</p>
        </Link>
      ))}
    </div>
  );
}
