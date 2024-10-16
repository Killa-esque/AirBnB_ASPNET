export interface AuthContextProps {
  user: User | null,
  signOut: () => void
}

export interface User {
  id: string;
  fullName: string;
  email: string;
  phoneNumber: string;
  isHost: boolean;
  isAdmin: boolean;
  isUser: boolean;
  avatar: string;
  createdAt: Date;
  updatedAt: Date;
  token: {
    accessToken: string;
    refreshToken: string;
  }
}
