import styled from '@emotion/styled';
import { NavLink } from 'react-router';
import { RoleGuard } from '../infrastructure/auth';
import { theme } from '../theme';

const MenuElement = styled.ul(`
  list-style: none;
  padding: 0;
  margin: 0;
  display: flex;
  flex-direction: row;

  &>li {
    margin-right: ${theme.spaces.small};
  }
`);

const MenuLink = styled(NavLink)(`
  text-decoration: none;
  color: inherit;
  padding: ${theme.spaces.small};
  border-radius: ${theme.borderRadius.small};
  transition: background-color 0.2s ease;

  &:hover {
    background-color: rgba(255, 255, 255, 0.1);
  }

  &.active {
    background-color: rgba(255, 255, 255, 0.2);
    font-weight: 600;
  }
`);

export default function Menu() {
  return (
    <MenuElement>
      <li>
        <MenuLink to="/">Start</MenuLink>
      </li>
      <li>
        <MenuLink to="/projects">Projects</MenuLink>
      </li>

      {/* Admin-only menu items */}
      <RoleGuard roles={['create-user']}>
        <li>
          <MenuLink to="/users/create">Create User</MenuLink>
        </li>
      </RoleGuard>

      {/* User info page - available to all authenticated users */}
      <li>
        <MenuLink to="/user-info">User Info</MenuLink>
      </li>

      {/* Development/styling page - available to all authenticated users */}
      <li>
        <MenuLink to="/styling">Styling Guidelines</MenuLink>
      </li>
    </MenuElement>
  );
}
