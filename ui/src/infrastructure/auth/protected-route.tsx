import type { ReactNode } from 'react';
import LoadingSpinner from '../../components/loading-spinner';
import { useAuth } from './auth.context';
import { Button } from '@root/components';

interface ProtectedRouteProps {
  children: ReactNode;
  roles?: string[];
  fallback?: ReactNode;
}

export function ProtectedRoute({
  children,
  roles,
  fallback,
}: ProtectedRouteProps) {
  const { isAuthenticated, isLoading, hasAnyRole, login } = useAuth();

  if (isLoading) {
    return <LoadingSpinner message="Authenticating..." />;
  }

  if (!isAuthenticated) {
    return (
      fallback || (
        <div
          style={{
            display: 'flex',
            flexDirection: 'column',
            justifyContent: 'center',
            alignItems: 'center',
            height: '100vh',
          }}
        >
          <div>You need to be authenticated to access this page.</div>
          <div>To login click <Button buttonType='rounded' variant='action' onClick={() => login()}>here</Button>.</div>
        </div>
      )
    );
  }

  if (roles && roles.length > 0 && !hasAnyRole(roles)) {
    return (
      fallback || (
        <div
          style={{
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center',
            height: '100vh',
          }}
        >
          <div>
            You don't have the required permissions to access this page.
          </div>
        </div>
      )
    );
  }

  return <>{children}</>;
}

interface RoleGuardProps {
  children: ReactNode;
  roles: string[];
  fallback?: ReactNode;
}

export function RoleGuard({ children, roles, fallback }: RoleGuardProps) {
  const { hasAnyRole } = useAuth();

  if (!hasAnyRole(roles)) {
    return fallback || null;
  }

  return <>{children}</>;
}
