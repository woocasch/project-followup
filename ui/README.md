# Project Follow-up UI

A React application with Keycloak authentication for project management.

## Setup

Install the dependencies:

```bash
pnpm install
```

## Authentication

This application uses Keycloak for authentication and authorization. Before starting the application:

1. Ensure your Keycloak server is running
2. Configure the Keycloak settings in `/public/configuration.json`
3. Set up the required realm, client, and user roles in Keycloak

For detailed authentication documentation, see [KEYCLOAK_AUTH.md](./KEYCLOAK_AUTH.md).

## Get started

Start the dev server:

```bash
pnpm dev
```

Build the app for production:

```bash
pnpm build
```

Preview the production build locally:

```bash
pnpm preview
```

## Features

- **Authentication**: Keycloak integration with automatic token refresh
- **Role-based Access Control**: Protect routes and components based on user roles
- **Responsive Design**: Modern UI with light/dark theme support
- **Type Safety**: Full TypeScript support
- **API Integration**: Automatic authorization headers for API requests

## Key Pages

- `/` - Home page (protected)
- `/projects` - Project management (protected)
- `/user-info` - Current user information and roles
- `/users/create` - User creation (admin only)
- `/styling` - UI component showcase
