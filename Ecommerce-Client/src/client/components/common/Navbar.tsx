import { NavLink, useNavigate } from "react-router-dom";
import { navItems } from "../../../utils/navItems";
import { LogOutIcon, ShoppingCart, User } from "lucide-react";
import { useAuthStore } from "../../../auth/authStore";
import logo from "../../../assets/Logo.svg";
import { Badge } from "@mui/material";
import { useShallow } from "zustand/shallow";
import { useCartStore } from "../../../stores/cartStore";
import { useEffect } from "react";

function Navbar() {
  const navigate = useNavigate();
  const { clearAuth, user } = useAuthStore(
    useShallow((state) => ({
      clearAuth: state.clearAuth,
      user: state.user,
    }))
  );
  const { cart, listCart } = useCartStore(
    useShallow((s) => ({
      cart: s.cart,
      listCart: s.listCart,
    }))
  );

  useEffect(() => {
    listCart();
  }, [listCart, user]);

  const handleLogout = () => {
    clearAuth();
  };

  const handleLogin = () => {
    navigate("/login");
  };


  return (
    <div className="w-full h-[97px] bg-[#141414] flex flex-col justify-center items-center relative">
      <div className="flex w-[80%] justify-between text-white ">
        <img src={logo} />
        <nav className="hidden lg:flex justify-around w-[50%]">
          {navItems.map((item, key) => (
            <NavLink key={key} className="hover:text-[#D87D4A]" to={item.path}>
              <p>{item.item}</p>
            </NavLink>
          ))}
        </nav>
        {/* Mobil navbar */}
        <nav className="lg:hidden flex fixed bottom-0 left-0 bg-[#F1F1F1] text-black justify-around w-full h-[70px]">
          {navItems.map((item, key) => {
            const MuiIcon = item.icon;
            return (
              <NavLink
                key={key}
                className="hover:text-[#D87D4A] flex flex-col items-center w-[25%] justify-center border-gray-100"
                to={item.path}
              >
                <MuiIcon sx={{ fontSize: 30 }} />
                <p>{item.item}</p>
              </NavLink>
            );
          })}
        </nav>
        <div className="flex gap-4">
          {user ? (
            <div className="flex gap-3">
              <button onClick={handleLogout}>
                <LogOutIcon />
              </button>
              <button onClick={() => navigate("/profile/account")}>
                <User />
              </button>
            </div>
          ) : (
            <button onClick={handleLogin}>Giriş Yap</button>
          )}
          <button className="relative" onClick={() => navigate("/cart")}>
            <Badge badgeContent={cart?.totalItemCount} color="warning">
              <ShoppingCart />
            </Badge>
          </button>
        </div>
      </div>
    </div>
  );
}

export default Navbar;
