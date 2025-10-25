import logo from "../../assets/logo.svg";
import facebook from "../../assets/facebook.svg";
import instagram from "../../assets/instagram.svg";
import twitter from "../../assets/twitter.svg";
import { NavLink } from "react-router-dom";
import { navItems } from "../../utils/navItems";

function Footer() {
  return (
    <div className="w-full h-[365px] bg-black flex justify-center items-center text-white  ">
      <div className="w-[80%] mt-20 flex flex-col h-[80%] gap-9">
        <div className="flex justify-between">
          <img src={logo} />
          <nav className="flex justify-around gap-4 w-[30%] ">
            {navItems.map((item) => (
              <NavLink
                to={item.path}
                key={item.item}
                className="hover:text-[#D87D4A]"
              >
                <p>{item.item}</p>
              </NavLink>
            ))}
          </nav>
        </div>
        <div className="flex items-end justify-between">
          <p className="w-[40%] opacity-50">
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
        <p className="opacity-50">Copyright 2021. All Rights Reserved</p>
      </div>
    </div>
  );
}

export default Footer;
