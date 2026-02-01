export interface User {
  id: string;
  name: string;
  lastName: string;
  email: string;
  password: string;
  phoneNumber: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export type LoginResponse = {
  accessToken: string;
  refreshToken: string;
  user: User;
};

export type RefreshResponse = {
  accessToken: string;
  refreshToken: string;
};

export interface Register {
  name: string;
  lastName: string;
  email: string;
  password: string;
  phoneNumber: string;
}
