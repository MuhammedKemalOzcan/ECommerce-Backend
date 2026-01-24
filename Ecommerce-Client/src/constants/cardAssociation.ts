import type { IconType } from "react-icons/lib";
import { FaCcMastercard } from "react-icons/fa";
import { FaCcVisa } from "react-icons/fa";
import { SiAmericanexpress } from "react-icons/si";
import { BiCreditCard } from "react-icons/bi";

export const CARD_ASSOCIATION_CONFIG: {
  [key: string]: { icon: IconType; color: string };
} = {
  MASTER_CARD: {
    icon: FaCcMastercard,
    color: "#EB001B",
  },
  VISA: {
    icon: FaCcVisa,
    color: "#1A1F71",
  },
  AMERICAN_EXPRESS: {
    icon: SiAmericanexpress,
    color: "#006FCF",
  },
  TROY: {
    icon: BiCreditCard,
    color: "#00C1D5",
  },
};
