import { create } from "zustand";
import type { User } from "../types/User";
import { jwtDecode } from "jwt-decode";
import { createJSONStorage, persist } from "zustand/middleware";

type authState = {
  token: string | null;
  user: User | null;
  setToken: (t: string | null) => void;
  clearAuth: () => void;
};

function decodeUser(token: string | null): User | null {
  if (!token) return null;
  try {
    return jwtDecode<User>(token);
  } catch {
    return null;
  }
}

export const useAuthStore = create(
  persist<authState>(
    (set) => ({
      token: null,
      user: null,
      setToken: (t: string | null) => set(() => ({ token: t, user: decodeUser(t) })),
      clearAuth: () => set({ token: null, user: null }),
    }),
    {
      name: "auth",
      storage: createJSONStorage(() => localStorage),
    }
  )
);
