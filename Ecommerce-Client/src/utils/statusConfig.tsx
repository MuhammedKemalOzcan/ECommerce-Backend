import LocalShippingIcon from "@mui/icons-material/LocalShipping";
import CancelIcon from "@mui/icons-material/Cancel";
import type { JSX } from "react";
import { CircleDashed, Package } from "lucide-react";

export const statusConfig: {
  [key: string]: { title: string; text: string; bg: string; icon: JSX.Element };
} = {
  "1": {
    title: "Pending",
    text: "text-yellow-700",
    bg: "bg-yellow-100",
    icon: <CircleDashed />,
  },
  "2": {
    title: "Shipped",
    text: "text-blue-600",
    bg: " bg-light-blue-50",
    icon: <LocalShippingIcon />,
  },
  "3": {
    title: "Delivered",
    text: "text-blue-600",
    bg: " bg-blue-50",
    icon: <Package />,
  },
  "4": {
    title: "Canceled",
    text: "text-red-600",
    bg: " bg-red-50",
    icon: <CancelIcon />,
  },
};
