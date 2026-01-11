import {
  getAuthorizationHeader,
  getKeycloakInstance,
} from '@root/infrastructure/auth';
import getConfig from '@root/infrastructure/configuration';
import axios from 'axios';

export abstract class ApiClientBase {
  protected createBffClient() {
    const client = axios.create({
      baseURL: getConfig().bffRoot,
      headers: {
        'Content-Type': 'application/json',
      },
    });

    // Add request interceptor to include authorization header
    client.interceptors.request.use((config) => {
      const keycloak = getKeycloakInstance();
      if (keycloak) {
        const authHeader = getAuthorizationHeader(keycloak);
        if (authHeader) {
          config.headers.Authorization = authHeader;
        }
      }
      return config;
    });

    // Add response interceptor to handle authentication errors
    client.interceptors.response.use(
      (response) => response,
      (error) => {
        if (error.response?.status === 401) {
          // Token might be expired, try to refresh or redirect to login
          const keycloak = getKeycloakInstance();
          if (keycloak) {
            keycloak
              .updateToken(5)
              .then((refreshed) => {
                if (refreshed) {
                  // Retry the request with the new token
                  return client.request(error.config);
                } else {
                  // Token is still valid, but server rejected it
                  console.error('Authentication failed');
                  keycloak.login();
                }
              })
              .catch(() => {
                // Failed to refresh token
                console.error('Failed to refresh token');
                keycloak.login();
              });
          }
        }
        return Promise.reject(error);
      },
    );

    return client;
  }
}
