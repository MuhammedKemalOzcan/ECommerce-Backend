import { Box, BringToFront, Home, LogOutIcon, Users } from "lucide-react";
import { NavLink } from "react-router-dom";
import logo from "../../assets/Logo.svg";

export default function AdminNavbar() {
  const Items = [
    { name: "Dashboard", path: "dashboard", icon: <Home /> },
    { name: "Products", path: "products", icon: <Box /> },
    { name: "Orders", path: "orders", icon: <BringToFront /> },
    { name: "Customers", path: "customers", icon: <Users /> },
  ];

  return (
    <div className="w-[20%] relative ">
      <div className="bg-[#101010] w-[17%] fixed text-white flex flex-col p-6 h-screen rounded-r-[12px] gap-6 left-0">
        <img src={logo} />
        {Items.map((item, index) => (
          <NavLink
            key={index}
            to={item.path}
            className="flex items-center gap-4"
          >
            <span>{item.icon}</span>
            <p>{item.name}</p>
          </NavLink>
        ))}
        <button className="flex gap-3 absolute bottom-10">
          <LogOutIcon />
          <p>Logout</p>
        </button>
      </div>
    </div>
  );
}
