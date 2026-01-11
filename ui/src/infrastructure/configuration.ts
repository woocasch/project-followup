export interface KeycloakConfiguration {
    url: string;
    realm: string;
    clientId: string;
}

export interface ApplicationConfiguration {
    bffRoot: string;
    keycloak: KeycloakConfiguration;
}

let config: ApplicationConfiguration;

export async function loadConfiguration() {
    const response = await fetch('/configuration.json');
    config = await response.json();
}

export function isConfigLoaded(): boolean {
    return !!config;
}

export default function getConfig(): ApplicationConfiguration {
  if (!config) {
    throw new Error("Config not loaded yet!");
  }
  return config;
}
