import { createContext, useState, ReactNode, FC, useContext } from 'react';
import { User, AuthContextProps } from '@/types';

export const AuthContext = createContext<AuthContextProps | undefined>(undefined);

export const AuthProvider: FC<{ children: ReactNode }> = ({ children }) => {
  const [user, setUser] = useState<User | null>(null);
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);

  const login = (userData: User) => {
    setUser(userData);
    setIsLoggedIn(!!!isLoggedIn);
  };

  const logout = () => {
    setUser(null);
    setIsLoggedIn(!!!isLoggedIn);
  };

  return (
    <AuthContext.Provider value={{ user, isLoggedIn, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
