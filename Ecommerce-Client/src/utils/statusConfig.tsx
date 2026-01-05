import DoneIcon from "@mui/icons-material/Done";
import LocalShippingIcon from "@mui/icons-material/LocalShipping";
import CancelIcon from "@mui/icons-material/Cancel";
import type { JSX } from "react";

export const statusConfig: { [key: string]: { style: string; icon: JSX.Element } } = {
  delivered: {
    style: "text-green-600 bg-light-green-200",
    icon: <DoneIcon />,
  },
  shipped: {
    style: "text-blue-600 bg-light-blue-50",
    icon: <LocalShippingIcon />,
  },
  canceled: {
    style: "text-red-600 bg-red-50",
    icon: <CancelIcon />,
  },
};
