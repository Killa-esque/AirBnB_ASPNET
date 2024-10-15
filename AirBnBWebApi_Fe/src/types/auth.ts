export interface User {
  name: string;
  email: string;
  role: 'guest' | 'user' | 'host' | 'admin';
}

export interface AuthContextProps {
  user: User | null;
  isLoggedIn: boolean;
  login: (user: User) => void;
  logout: () => void;
}
