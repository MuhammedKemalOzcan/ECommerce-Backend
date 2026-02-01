import type {
  LoginRequest,
  LoginResponse,
  Register,
} from "../types/User";
import { methods } from "./apiClient";

export async function loginUser(email: string, password: string) {
  const response = await methods.post<LoginRequest, LoginResponse>(
    "Auth/login",
    { email, password },
  );

  return response;
}

export async function RegisterUser(data: Register) {
  const response = await methods.post<Register, void>("Auth/register", data);
  return response;
}

export const refreshTokenLogin = async (refreshToken: string) => {
  const response = await methods.post("/Auth/refresh-token", {
    refreshToken: refreshToken,
  });
  return response;
};
