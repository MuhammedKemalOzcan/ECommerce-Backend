import { NavLink, useNavigate } from "react-router-dom";
import { navItems } from "../../utils/navItems";
import { LogOutIcon, ShoppingCart } from "lucide-react";
import { useAuthStore } from "../../auth/authStore";
import logo from "../../assets/Logo.svg";

import { useShallow } from "zustand/shallow";
import { useCartStore } from "../../stores/cartStore";

function Navbar() {
  const navigate = useNavigate();
  const { clearAuth, user } = useAuthStore(
    useShallow((state) => ({
      clearAuth: state.clearAuth,
      user: state.user,
    }))
  );
  const cart = useCartStore((s) => s.cart);

  const handleLogout = () => {
    clearAuth();
  };

  const handleLogin = () => {
    navigate("/login");
  };

  return (
    <div className="w-full h-[97px] bg-[#141414] flex flex-col justify-center items-center relative">
      <div className="flex w-[80%] justify-between text-white">
        <img src={logo} />
        <nav className="flex justify-around w-[50%] ">
          {navItems.map((item, key) => (
            <NavLink key={key} className="hover:text-[#D87D4A]" to={item.path}>
              <p>{item.item}</p>
            </NavLink>
          ))}
        </nav>
        <div className="flex gap-4">
          {user ? (
            <button onClick={handleLogout}>
              <LogOutIcon />
            </button>
          ) : (
            <button onClick={handleLogin}>Giriş Yap</button>
          )}
          <button className="relative" onClick={() => navigate("/cart")}>
            <p className="absolute -top-3 -right-4 bg-red-500 px-1 rounded-full ">{cart?.totalItemCount}</p>
            <ShoppingCart />
          </button>
        </div>
      </div>
      <div className="border border-[#FFFFFF] opacity-25 w-[80%] absolute bottom-0 "></div>
    </div>
  );
}

export default Navbar;
