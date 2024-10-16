import { createContext, useState, useEffect, ReactNode } from 'react';
import { storage } from '@/utils';
import storageKeys from '@/constants/storageKeys';
import { AuthContextProps, User } from '@/types';

interface AuthProviderProps {
  children: ReactNode;
}

export const AuthContext = createContext<AuthContextProps>({
  user: null,
  signOut: () => { },
});

export const AuthProvider = ({ children }: AuthProviderProps) => {
  const [user, setUser] = useState<User | null>(storage.get(storageKeys.USER));

  useEffect(() => {
    const handleStorageChange = () => {
      setUser(storage.get(storageKeys.USER));
    };

    window.addEventListener('storage', handleStorageChange);
    return () => window.removeEventListener('storage', handleStorageChange);
  }, []);

  const signOut = () => {
    storage.clear();
    setUser(null);
    window.location.reload();
  };

  return (
    <AuthContext.Provider value={{ user, signOut }}>
      {children}
    </AuthContext.Provider>
  );
};
