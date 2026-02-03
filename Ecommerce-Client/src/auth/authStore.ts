import { create } from "zustand";
import type { User } from "../types/User";
import { jwtDecode } from "jwt-decode";
import { createJSONStorage, persist } from "zustand/middleware";
import { refreshTokenLogin } from "../api/AuthService";

type authState = {
  token: string | null;
  user: User | null;
  refreshToken: string | null;
  isAdmin: boolean;
  setToken: (
    accessToken: string | null,
    refreshToken: string | null,
    isAdmin: boolean,
  ) => void;
  clearAuth: () => void;
  refreshAccessToken: (
    refreshToken: string | null,
  ) => Promise<string | undefined>;
  role: string | null;
};

function decodeUser(token: string | null): User | null {
  console.log(token);

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
      refreshToken: null,
      isAdmin: false,
      role: null,
      setToken: (
        accessToken: string | null,
        refreshToken: string | null,
        isAdmin: boolean,
      ) =>
        set(() => ({
          token: accessToken,
          user: decodeUser(accessToken),
          refreshToken: refreshToken,
          isAdmin: isAdmin,
        })),
      clearAuth: () => set({ token: null, user: null }),
      refreshAccessToken: async (refreshToken: string | null) => {
        if (!refreshToken) return;
        try {
          const response = await refreshTokenLogin(refreshToken);
          console.log("Refresh token Response:", response);

          set({
            token: (response as any).accessToken,
            refreshToken: (response as any).refreshToken,
            user: decodeUser((response as any).accessToken),
          });
          return (response as any).accessToken;
        } catch (error) {
          console.log(error);
          set({ token: null, refreshToken: null });
          throw error;
        }
      },
    }),
    {
      name: "auth",
      storage: createJSONStorage(() => localStorage),
    },
  ),
);
