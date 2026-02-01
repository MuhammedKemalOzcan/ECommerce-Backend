import { useState } from "react";
import { useAuthStore } from "./authStore";
import { loginUser } from "../api/AuthService";

export function useLogin() {
  const setToken = useAuthStore((s) => s.setToken);
  const [loading, setLoading] = useState(false);

  const mutate = async (email: string, password: string) => {
    try {
      setLoading(true);
      const response = await loginUser(email, password);
      setToken(response.accessToken,response.refreshToken);
      return response;
    } catch (e: any) {
      return Promise.reject(e);
    } finally {
      setLoading(false);
    }
  };

  return { mutate, loading };
}
