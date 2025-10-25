import type { LoginRequest, LoginResponse } from "../types/User";
import { methods } from "../api/apiClient";

export async function loginUser(email: string, password: string) {
  const response = await methods.post<LoginRequest, LoginResponse>(
    "Auth/login",
    { email, password }
  );

  return response;
}
