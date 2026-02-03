import { Navigate } from "react-router-dom";
import { useAuthStore } from "../auth/authStore";
import type React from "react";

interface ProtectedRouteProps {
  children: React.ReactNode;
}

//replace ile korunan sayfalara girmeye çalışırken atıldığında geri tuşuna bastığında  tekrar girebilmesini engelliyoruz.
export const ProtectedAdminRoute = ({ children }: ProtectedRouteProps) => {
  const isAdmin = useAuthStore((state) => state.isAdmin);

  if (!isAdmin) {
    return <Navigate to="/" replace />;
  }

  return children;
};

export const ProtectedUserRoute = ({ children }: ProtectedRouteProps) => {
  const user = useAuthStore((state) => state.user);

  if (!user) {
    return <Navigate to="/login" />;
  }

  return children;
};
