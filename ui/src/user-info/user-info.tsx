import { Card } from '@components/card';
import { PageHeader } from '@components/headers';
import styled from '@emotion/styled';
import { useAuth } from '../infrastructure/auth';
import { theme } from '../theme';

const UserInfoContainer = styled.div(`
  display: flex;
  flex-direction: column;
  gap: ${theme.spaces.medium};
  max-width: 800px;
  margin: 0 auto;
  padding: ${theme.spaces.medium};
`);

const InfoGrid = styled.div(`
  display: grid;
  grid-template-columns: auto 1fr;
  gap: ${theme.spaces.small} ${theme.spaces.medium};
  align-items: center;
`);

const Label = styled.span(`
  font-weight: 600;
  color: ${theme.colors.textSecondary};
  min-width: 120px;
`);

const Value = styled.span(`
  color: ${theme.colors.text};
`);

const RolesList = styled.ul(`
  list-style: none;
  padding: 0;
  margin: 0;
  display: flex;
  flex-wrap: wrap;
  gap: ${theme.spaces.small};
`);

const RoleTag = styled.li(`
  background: ${theme.colors.primary};
  color: ${theme.colors.surface};
  padding: ${theme.spaces.small};
  border-radius: ${theme.borderRadius};
  font-size: ${theme.fontSizes.small};
`);

export default function UserInfoPage() {
  const { user, isAuthenticated, keycloak } = useAuth();

  if (!isAuthenticated || !user) {
    return (
      <UserInfoContainer>
        <PageHeader>User Information</PageHeader>
        <Card>
          You are not authenticated or user information is not available.
        </Card>
      </UserInfoContainer>
    );
  }

  return (
    <UserInfoContainer>
      <PageHeader>User Information</PageHeader>

      <Card>
        <Card.Header>Basic Information</Card.Header>
        <Card.Content>
          <InfoGrid>
            <Label>User ID:</Label>
            <Value>{user.id}</Value>

            <Label>Username:</Label>
            <Value>{user.username}</Value>

            <Label>Email:</Label>
            <Value>{user.email || 'Not provided'}</Value>

            <Label>First Name:</Label>
            <Value>{user.firstName || 'Not provided'}</Value>

            <Label>Last Name:</Label>
            <Value>{user.lastName || 'Not provided'}</Value>
          </InfoGrid>
        </Card.Content>
      </Card>

      <Card>
        <Card.Header>Roles and Permissions</Card.Header>
        <Card.Content>
          {user.roles && user.roles.length > 0 ? (
            <RolesList>
              {user.roles.map((role) => (
                <RoleTag key={role}>{role}</RoleTag>
              ))}
            </RolesList>
          ) : (
            <span>No roles assigned.</span>
          )}
        </Card.Content>
      </Card>

      <Card>
        <Card.Header>Token Information</Card.Header>
        <Card.Content>
          <InfoGrid>
            <Label>Token Valid:</Label>
            <Value>{keycloak?.token ? 'Yes' : 'No'}</Value>

            <Label>Token Expires:</Label>
            <Value>
              {keycloak?.tokenParsed?.exp
                ? new Date(keycloak.tokenParsed.exp * 1000).toLocaleString()
                : 'Unknown'}
            </Value>

            <Label>Refresh Token:</Label>
            <Value>
              {keycloak?.refreshToken ? 'Available' : 'Not available'}
            </Value>
          </InfoGrid>
        </Card.Content>
      </Card>
    </UserInfoContainer>
  );
}
