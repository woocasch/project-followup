import styled from '@emotion/styled';
import { LogOut, User } from 'lucide-react';
import { useState } from 'react';
import { NavLink } from 'react-router';
import { useAuth } from '../infrastructure/auth';
import { theme } from '../theme';

const UserProfileContainer = styled.div(`
  position: relative;
  display: flex;
  align-items: center;
  gap: ${theme.spaces.small};
`);

const UserButton = styled.button(`
  display: flex;
  align-items: center;
  gap: ${theme.spaces.small};
  padding: ${theme.spaces.small};
  background: transparent;
  border: 1px solid ${theme.colors.border};
  border-radius: ${theme.borderRadius.small};
  color: ${theme.colors.text};
  cursor: pointer;
  transition: all 0.2s ease;

  &:hover {
    background: ${theme.colors.surface};
    border-color: ${theme.colors.primary};
  }

  &:focus {
    outline: 2px solid ${theme.colors.primary};
    outline-offset: 2px;
  }
`);

const DropdownMenu = styled.div<{
  isOpen?: boolean;
}>`
  position: absolute;
  top: 100%;
  right: 0;
  margin-top: ${theme.spaces.small};
  background: ${theme.colors.surface};
  border: 1px solid ${theme.colors.border};
  border-radius: ${theme.borderRadius.small};
  box-shadow: ${theme.shadows.medium};
  min-width: 200px;
  z-index: 1000;
  display: ${(props) => (props.isOpen ? 'block' : 'none')};
`;

const DropdownItem = styled.div(`
  padding: ${theme.spaces.medium};
  border-bottom: 1px solid ${theme.colors.border};
  
  &:last-child {
    border-bottom: none;
  }
`);

const UserInfo = styled.div(`
  display: flex;
  flex-direction: column;
  gap: 2px;
  
  .username {
    font-weight: 600;
    color: ${theme.colors.text};
  }
  
  .email {
    font-size: 0.875rem;
    color: ${theme.colors.textSecondary};
  }
`);

const LogoutButton = styled.button(`
  display: flex;
  align-items: center;
  gap: ${theme.spaces.small};
  width: 100%;
  padding: ${theme.spaces.small};
  background: transparent;
  border: none;
  color: ${theme.colors.danger};
  cursor: pointer;
  border-radius: ${theme.borderRadius.small};
  transition: background-color 0.2s ease;

  &:hover {
    background: ${theme.colors.dangerLight};
  }

  &:focus {
    outline: 2px solid ${theme.colors.danger};
    outline-offset: 2px;
  }
`);

export default function UserProfile() {
  const { user, logout } = useAuth();
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);

  if (!user) {
    return null;
  }

  const handleToggleDropdown = () => {
    setIsDropdownOpen(!isDropdownOpen);
  };

  const handleLogout = () => {
    setIsDropdownOpen(false);
    logout();
  };

  // Close dropdown when clicking outside
  const handleBlur = (e: React.FocusEvent) => {
    if (!e.currentTarget.contains(e.relatedTarget)) {
      setIsDropdownOpen(false);
    }
  };

  const displayName =
    user.firstName && user.lastName
      ? `${user.firstName} ${user.lastName}`
      : user.username;

  return (
    <UserProfileContainer onBlur={handleBlur} tabIndex={-1}>
      <UserButton onClick={handleToggleDropdown} aria-expanded={isDropdownOpen}>
        <User size={18} />
        <span>{displayName}</span>
      </UserButton>

      <DropdownMenu isOpen={isDropdownOpen}>
        <DropdownItem>
          <UserInfo>
            <div className="username">
              <NavLink to="/user-info">{displayName}</NavLink>
            </div>
            {user.email && <div className="email">{user.email}</div>}
          </UserInfo>
        </DropdownItem>

        <DropdownItem>
          <LogoutButton onClick={handleLogout}>
            <LogOut size={16} />
            Logout
          </LogoutButton>
        </DropdownItem>
      </DropdownMenu>
    </UserProfileContainer>
  );
}
