# Keycloak Authentication Integration

This document describes how to use the Keycloak authentication system integrated into the application.

## Overview

The application uses Keycloak for authentication and authorization. All routes are protected by default, and users must authenticate before accessing any content.

## Configuration

The Keycloak configuration is stored in `/public/configuration.json`:

```json
{
  "bffRoot": "https://localhost:7037",
  "keycloak": {
    "url": "http://localhost:5000/auth",
    "realm": "project-follow-up",
    "clientId": "frontend"
  }
}
```

## Key Components

### AuthProvider

The `AuthProvider` component wraps the entire application and manages authentication state:

```tsx
import { AuthProvider } from './infrastructure/auth';

// In your root component
<AuthProvider>
  <RouterProvider router={router} />
</AuthProvider>
```

### useAuth Hook

The `useAuth` hook provides access to authentication state and functions:

```tsx
import { useAuth } from './infrastructure/auth';

function MyComponent() {
  const { 
    isAuthenticated, 
    isLoading, 
    user, 
    login, 
    logout, 
    hasRole, 
    hasAnyRole 
  } = useAuth();

  if (isLoading) return <LoadingSpinner />;
  if (!isAuthenticated) return <div>Please log in</div>;

  return <div>Welcome, {user?.username}!</div>;
}
```

### ProtectedRoute Component

Use `ProtectedRoute` to protect entire pages or components:

```tsx
import { ProtectedRoute } from './infrastructure/auth';

// Protect a route for any authenticated user
<ProtectedRoute>
  <MyPage />
</ProtectedRoute>

// Protect a route for specific roles
<ProtectedRoute roles={['admin', 'user-manager']}>
  <AdminPage />
</ProtectedRoute>
```

### RoleGuard Component

Use `RoleGuard` to conditionally render content based on user roles:

```tsx
import { RoleGuard } from './infrastructure/auth';

function MyComponent() {
  return (
    <div>
      <p>This is visible to all authenticated users</p>
      
      <RoleGuard roles={['admin']}>
        <button>Admin Only Action</button>
      </RoleGuard>
      
      <RoleGuard roles={['admin', 'moderator']}>
        <p>Visible to admins and moderators</p>
      </RoleGuard>
    </div>
  );
}
```

## API Client Integration

The API client automatically includes authorization headers for authenticated requests:

```tsx
// The API client will automatically include the Bearer token
// No additional configuration needed in your API calls
const client = this.createBffClient();
const response = await client.get('/api/projects');
```

The client also handles token refresh automatically:
- If a 401 error occurs, it attempts to refresh the token
- If refresh fails, it redirects the user to login
- If refresh succeeds, it retries the original request

## User Information

The `user` object from `useAuth()` contains:

```typescript
interface KeycloakUser {
  id: string;
  username: string;
  email: string;
  firstName?: string;
  lastName?: string;
  roles: string[];
}
```

## Role-Based Access Control

### Checking Roles

```tsx
const { hasRole, hasAnyRole } = useAuth();

// Check for a specific role
if (hasRole('admin')) {
  // User has admin role
}

// Check for any of multiple roles
if (hasAnyRole(['admin', 'moderator', 'user-manager'])) {
  // User has at least one of these roles
}
```

### Common Role Patterns

- `admin` - Full system administration
- `user-manager` - Can manage user accounts
- `project-manager` - Can manage projects
- `viewer` - Read-only access

## Error Handling

The authentication system handles several error scenarios:

1. **Failed authentication** - Redirects to Keycloak login
2. **Token expiration** - Automatically refreshes token
3. **Insufficient permissions** - Shows permission denied message
4. **Network errors** - Retries authentication

## Development and Testing

### User Info Page

Visit `/user-info` to see current user information, roles, and token details.

### Testing Different Roles

1. Log out using the user profile dropdown
2. Log in with a different user account in Keycloak
3. Navigate to protected pages to test role-based access

### Local Development

Ensure your Keycloak server is running on the configured URL and that the realm and client are properly configured.

## Security Best Practices

1. **Never store tokens in localStorage** - The Keycloak adapter handles token storage securely
2. **Use HTTPS in production** - Configure Keycloak and the application for HTTPS
3. **Regularly refresh tokens** - The system automatically handles token refresh
4. **Validate roles server-side** - Client-side role checks are for UX only
5. **Use specific roles** - Prefer specific roles over broad permissions

## Troubleshooting

### Common Issues

1. **Authentication redirects in a loop**
   - Check Keycloak client configuration
   - Verify redirect URIs are correctly set

2. **Token refresh fails**
   - Check if refresh tokens are enabled in Keycloak
   - Verify session timeout settings

3. **Roles not appearing**
   - Check if roles are mapped to the token
   - Verify client mappers in Keycloak

4. **CORS errors**
   - Configure Keycloak CORS settings
   - Check allowed origins in client configuration

### Debug Information

Use the browser's developer tools to:
- Check network requests for authentication calls
- Inspect JWT tokens in the application tab
- View console logs for authentication events
- Monitor API calls for proper authorization headers