import { useState } from "react";
import type { Register } from "../types/User";
import { RegisterUser } from "../services/AuthService";

export function useRegister() {
  const [loading, setLoading] = useState<boolean>(false);

  const mutate = async (data: Register) => {
    try {
      setLoading(true);
      await RegisterUser(data);
    } catch (error: any) {
      console.log(error);
    } finally {
      setLoading(false);
    }
  };

  return { mutate, loading };
}
