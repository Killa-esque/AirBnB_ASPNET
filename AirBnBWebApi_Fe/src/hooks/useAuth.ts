import { useContext } from "react";
import { AuthContextProps } from "@/types";
import { AuthContext } from "@/contexts";

const useAuth = (): AuthContextProps => {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
};

export default useAuth;
