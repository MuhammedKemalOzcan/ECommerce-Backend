import logo from "../../../assets/Logo.svg"
import facebook from "../../../assets/facebook.svg";
import instagram from "../../../assets/instagram.svg";
import twitter from "../../../assets/twitter.svg";
import { NavLink } from "react-router-dom";
import { navItems } from "../../../utils/navItems";

function Footer() {
  return (
      <div className="bg-black text-white flex flex-col text-center items-center gap-12 p-4 lg:p-20">
        <div className="w-full lg:flex-row lg:justify-between items-center flex flex-col text-center gap-6">
          <img src={logo} className="size-[50%]" />
          <nav className="w-full lg:flex-row lg:justify-end flex flex-col gap-4 text-center justify-around">
            {navItems.map((item) => (
              <NavLink
                to={item.path}
                key={item.item}
                className="hover:text-[#D87D4A] flex flex-col gap-4 "
              >
                <p>{item.item}</p>
              </NavLink>
            ))}
          </nav>
        </div>
        <div className="lg:flex-row lg:justify-between flex flex-col items-center justify-center gap-6">
          <p className="lg:w-[40%] lg:text-start opacity-50">
            Audiophile is an all in one stop to fulfill your audio needs. We're
            a small team of music lovers and sound specialists who are devoted
            to helping you get the most out of personal audio. Come and visit
            our demo facility - we’re open 7 days a week.
          </p>
          <div className="flex gap-4">
            <button>
              <img src={facebook} className="hover:bg-[#D87D4A]" />
            </button>
            <button>
              <img src={twitter} className="hover:bg-[#D87D4A]" />
            </button>
            <button>
              <img
                src={instagram}
                className="hover:bg-[#D87D4A] hover:rounded-[8px]"
              />
            </button>
          </div>
        </div>
        <p className="opacity-50 w-full lg:text-start">Copyright 2021. All Rights Reserved</p>
      </div>
  );
}

export default Footer;
