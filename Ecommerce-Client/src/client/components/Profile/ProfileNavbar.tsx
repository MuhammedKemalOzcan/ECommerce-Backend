import { Avatar } from "@mui/material";
import { Package, User } from "lucide-react";
import { NavLink } from "react-router-dom";
import { useCustomerStore } from "../../../stores/customerStore";

export default function ProfileNavbar() {
  const customer = useCustomerStore((s) => s.customer);

  const profileLinks = [
    { name: "Account", path: "/profile/account", icon: <User size={24} /> },
    { name: "Orders", path: "/profile/orders", icon: <Package size={24} /> },
  ];

  const UserInfo = () => (
    <div className="flex gap-4 items-center">
      <Avatar alt={customer?.firstName} sx={{ width: 50, height: 50 }} />
      <div className="flex flex-col">
        <span className="text-sm text-gray-400">Hello,</span>
        <span className="font-bold text-lg">{customer?.firstName}</span>
      </div>
    </div>
  );

  return (
    <div>
      <div className="hidden lg:flex flex-col w-60 h-full gap-6 p-6 bg-[#141414] text-white rounded-r-lg">
        <div className="mb-4">
          <UserInfo />
        </div>

        <nav className="flex flex-col gap-2">
          {profileLinks.map((link) => (
            <NavLink
              key={link.path}
              to={link.path}
              className={({ isActive }) =>
                `flex w-full h-12 gap-3 items-center px-4 rounded-md transition-colors ${
                  isActive
                    ? "bg-[#D87D4A] text-white"
                    : "hover:bg-white/10 text-gray-300"
                }`
              }
            >
              {link.icon}
              <p className="font-medium">{link.name}</p>
            </NavLink>
          ))}
        </nav>
      </div>
      <div className="flex lg:hidden fixed bottom-0 left-0 w-full h-[70px] bg-[#141414] border-t border-white/10 z-50 justify-around items-center px-2">
        {profileLinks.map((link) => (
          <NavLink
            key={link.path}
            to={link.path}
            className={({ isActive }) =>
              `flex flex-col items-center justify-center gap-1 w-full h-full ${
                isActive ? "text-[#D87D4A]" : "text-gray-400"
              }`
            }
          >
            {link.icon}
            <span className="text-[10px] uppercase font-bold tracking-wide">
              {link.name}
            </span>
          </NavLink>
        ))}
      </div>
    </div>
  );
}
