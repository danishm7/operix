import { login, type LoginRequest } from "@/features/auth/authApi";
import { subscribeToAuthEvent } from "@/features/auth/authEvents";
import {
  getAccessToken,
  removeAccessToken,
  storeAccessToken,
} from "@/features/auth/authStorage";
import {
  createContext,
  useContext,
  useEffect,
  useState,
  type ReactNode,
} from "react";

interface AuthContextValue {
  accessToken: string | null;
  isAuthenticated: boolean;
  login: (request: LoginRequest) => Promise<void>;
  logout: () => void;
}

const AuthContext = createContext<AuthContextValue | null>(null);

interface AuthProviderProps {
  children: ReactNode;
}

export function AuthProvider({ children }: AuthProviderProps) {
  const [accessToken, setAccessToken] = useState<string | null>(getAccessToken);

  useEffect(() => {
    return subscribeToAuthEvent("unauthorized", () => {
      setAccessToken(null);
    });
  }, []);

  const handleLogin = async (request: LoginRequest) => {
    const response = await login(request);
    storeAccessToken(response.accessToken);
    setAccessToken(response.accessToken);
  };

  const logout = () => {
    removeAccessToken();
    setAccessToken(null);
  };

  const value: AuthContextValue = {
    accessToken,
    isAuthenticated: accessToken !== null,
    login: handleLogin,
    logout,
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function useAuth() {
  const context = useContext(AuthContext);

  if (context === null) {
    throw new Error("useAuth must be used within an AuthProvider");
  }

  return context;
}
