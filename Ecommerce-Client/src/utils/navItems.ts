import headphones from "../assets/Headphones.svg";
import earphones from "../assets/earphones.svg";
import speakers from "../assets/Speaker.svg";
import {
  Earbuds,
  Headphones,
  Home,
  Speaker,
  type SvgIconComponent,
} from "@mui/icons-material";

export interface NavItem {
  item: string;
  path: string;
  image?: string;
  icon: SvgIconComponent;
}

export const navItems: NavItem[] = [
  { item: "HOME", path: "/", icon: Home },
  {
    item: "HEADPHONES",
    path: "/headphones",
    image: headphones,
    icon: Headphones,
  },
  { item: "SPEAKERS", path: "/speakers", image: speakers, icon: Speaker },
  { item: "EARPHONES", path: "/earphones", image: earphones, icon: Earbuds },
];
