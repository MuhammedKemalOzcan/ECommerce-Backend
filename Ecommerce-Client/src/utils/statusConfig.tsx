import LocalShippingIcon from "@mui/icons-material/LocalShipping";
import CancelIcon from "@mui/icons-material/Cancel";
import type { JSX } from "react";
import { Pending } from "@mui/icons-material";
import { Package } from "lucide-react";

export const statusConfig: {
  [key: string]: { title: string; text: string; bg: string; icon: JSX.Element };
} = {
  "1": {
    title: "Pending",
    text: "text-yellow-600",
    bg: "bg-yellow-200",
    icon: <Pending />,
  },
  "2": {
    title: "Shipped",
    text: "text-blue-600",
    bg: " bg-light-blue-50",
    icon: <LocalShippingIcon />,
  },
  "3": {
    title: "Canceled",
    text: "text-red-600",
    bg: " bg-red-50",
    icon: <CancelIcon />,
  },
  "4": {
    title: "Delivered",
    text: "text-blue-600",
    bg: " bg-blue-50",
    icon: <Package />,
  },
};
