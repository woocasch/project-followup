import type Keycloak from 'keycloak-js';
import type { ReactNode } from 'react';
import { createContext, useContext, useEffect, useState } from 'react';
import getConfig from '../configuration';
import {
  authenticateUser,
  getCurrentUser,
  hasAnyRole,
  hasRole,
  initializeKeycloak,
  type KeycloakUser,
  logout,
} from './keycloak.service';

interface AuthContextValue {
  isAuthenticated: boolean;
  isLoading: boolean;
  user: KeycloakUser | null;
  keycloak: Keycloak | null;
  login: () => void;
  logout: () => void;
  hasRole: (role: string) => boolean;
  hasAnyRole: (roles: string[]) => boolean;
  getToken: () => string | null;
}

const AuthContext = createContext<AuthContextValue | null>(null);

interface AuthProviderProps {
  children: ReactNode;
}

export function AuthProvider({ children }: AuthProviderProps) {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [isLoading, setIsLoading] = useState(true);
  const [user, setUser] = useState<KeycloakUser | null>(null);
  const [keycloak, setKeycloak] = useState<Keycloak | null>(null);

  useEffect(() => {
    const initializeAuth = async () => {
      try {
        const config = getConfig();
        const keycloakInstance = initializeKeycloak(config);
        setKeycloak(keycloakInstance);

        const authenticated = await authenticateUser(keycloakInstance);
        setIsAuthenticated(authenticated);

        if (authenticated) {
          const currentUser = getCurrentUser(keycloakInstance);
          setUser(currentUser);
        }
      } catch (error) {
        console.error('Failed to initialize authentication:', error);
        setIsAuthenticated(false);
        setUser(null);
      } finally {
        setIsLoading(false);
      }
    };

    initializeAuth();
  }, []);

  const handleLogin = () => {
    if (keycloak) {
      keycloak.login();
    }
  };

  const handleLogout = () => {
    if (keycloak) {
      logout(keycloak);
      setIsAuthenticated(false);
      setUser(null);
    }
  };

  const checkRole = (role: string): boolean => {
    return keycloak ? hasRole(keycloak, role) : false;
  };

  const checkAnyRole = (roles: string[]): boolean => {
    return keycloak ? hasAnyRole(keycloak, roles) : false;
  };

  const getToken = (): string | null => {
    return keycloak?.token || null;
  };

  const contextValue: AuthContextValue = {
    isAuthenticated,
    isLoading,
    user,
    keycloak,
    login: handleLogin,
    logout: handleLogout,
    hasRole: checkRole,
    hasAnyRole: checkAnyRole,
    getToken,
  };

  return (
    <AuthContext.Provider value={contextValue}>{children}</AuthContext.Provider>
  );
}

export function useAuth(): AuthContextValue {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
}
