import type { LoginRequest, LoginResponse } from "../types/User";
import { methods } from "../api/apiClient";

export async function loginUser(email: string, password: string) {
  const response = await methods.post<LoginRequest, LoginResponse>(
    "Auth/login",
    { email, password }
  );
  console.log(response.token);
  
  return response;

  // const { user } = response.data;

  // localStorage.setItem("token", response.data.token);
  // localStorage.setItem("user", JSON.stringify(user));
  // return user;
}
