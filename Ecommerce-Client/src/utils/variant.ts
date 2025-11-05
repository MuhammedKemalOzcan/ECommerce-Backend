import { AlertTriangle, Info, XCircle } from "lucide-react";

export const variantConfig = {
    danger: {
      icon: XCircle,
      iconColor: "text-red-600",
      confirmButtonClass: "bg-red-600 hover:bg-red-700 focus:ring-red-500",
      iconBgClass: "bg-red-100",
    },
    warning: {
      icon: AlertTriangle,
      iconColor: "text-amber-600",
      confirmButtonClass:
        "bg-amber-600 hover:bg-amber-700 focus:ring-amber-500",
      iconBgClass: "bg-amber-100",
    },
    info: {
      icon: Info,
      iconColor: "text-blue-600",
      confirmButtonClass: "bg-blue-600 hover:bg-blue-700 focus:ring-blue-500",
      iconBgClass: "bg-blue-100",
    },
  };