import Keycloak from 'keycloak-js';
import type { ApplicationConfiguration } from '../configuration';

let keycloakInstance: Keycloak | null = null;

export interface KeycloakUser {
  id: string;
  username: string;
  email: string;
  firstName?: string;
  lastName?: string;
  roles: string[];
}

export function initializeKeycloak(config: ApplicationConfiguration): Keycloak {
  if (!keycloakInstance) {
    keycloakInstance = new Keycloak({
      url: config.keycloak.url,
      realm: config.keycloak.realm,
      clientId: config.keycloak.clientId,
    });
  }
  return keycloakInstance;
}

export function getKeycloakInstance(): Keycloak | null {
  return keycloakInstance;
}

export async function authenticateUser(keycloak: Keycloak): Promise<boolean> {
  try {
    const authenticated = await keycloak.init({
      onLoad: 'check-sso',
      checkLoginIframe: false,
      enableLogging: true,
    });

    if (authenticated) {
      // Set up token refresh
      keycloak.onTokenExpired = () => {
        keycloak
          .updateToken(30)
          .then((refreshed) => {
            if (refreshed) {
              console.log('Token refreshed');
            } else {
              console.log('Token not refreshed, valid for at least 30 seconds');
            }
          })
          .catch(() => {
            console.error('Failed to refresh token');
            keycloak.login();
          });
      };
    }

    return authenticated;
  } catch (error) {
    console.error('Failed to initialize Keycloak', error);
    return false;
  }
}

export function getCurrentUser(keycloak: Keycloak): KeycloakUser | null {
  if (!keycloak.authenticated || !keycloak.tokenParsed) {
    return null;
  }

  const token = keycloak.tokenParsed;
  const realmAccess = token.realm_access as { roles: string[] } | undefined;
  const resourceAccess = token.resource_access as
    | Record<string, { roles: string[] }>
    | undefined;

  // Combine realm roles and client roles
  const roles: string[] = [];
  if (realmAccess?.roles) {
    roles.push(...realmAccess.roles);
  }
  if (resourceAccess) {
    Object.values(resourceAccess).forEach((access) => {
      if (access.roles) {
        roles.push(...access.roles);
      }
    });
  }

  return {
    id: token.sub || '',
    username: token.preferred_username || '',
    email: token.email || '',
    firstName: token.given_name,
    lastName: token.family_name,
    roles: [...new Set(roles)], // Remove duplicates
  };
}

export function logout(keycloak: Keycloak, redirectUri?: string): void {
  keycloak.logout({
    redirectUri: redirectUri || window.location.origin,
  });
}

export function hasRole(keycloak: Keycloak, role: string): boolean {
  const user = getCurrentUser(keycloak);
  return user ? user.roles.includes(role) : false;
}

export function hasAnyRole(keycloak: Keycloak, roles: string[]): boolean {
  const user = getCurrentUser(keycloak);
  return user ? roles.some((role) => user.roles.includes(role)) : false;
}

export function getAuthorizationHeader(keycloak: Keycloak): string | null {
  return keycloak.token ? `Bearer ${keycloak.token}` : null;
}
