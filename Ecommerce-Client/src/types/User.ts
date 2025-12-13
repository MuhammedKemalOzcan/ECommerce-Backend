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
  token: string;
  user: User;
};

export interface Register {
  name: string;
  lastName: string;
  email: string;
  password: string;
  phoneNumber: string;
}
