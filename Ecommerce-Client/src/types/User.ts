export interface User {
  email: string;
  password: string;
}

export interface LoginRequest {
  email: string;
  password: string;
};

export type LoginResponse = {
  token: string;
  user: User;
};
