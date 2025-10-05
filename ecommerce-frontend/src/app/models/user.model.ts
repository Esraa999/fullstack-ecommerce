export interface User {
  id: number;
  username: string;
  email: string;
  lastLogin?: Date;
}

export interface LoginDto {
  username: string;
  password: string;
}

export interface RegisterDto {
  username: string;
  password: string;
  email: string;
}

export interface AuthResult {
  success: boolean;
  accessToken?: string;
  refreshToken?: string;
  errors?: string[];
}