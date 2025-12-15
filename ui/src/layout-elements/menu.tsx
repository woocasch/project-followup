import styled from '@emotion/styled';
import { theme } from '../theme';
import { NavLink } from 'react-router';

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

export default function Menu() {
  return (
    <MenuElement>
      <li>
        <NavLink to="/" style={{ textDecoration: 'none', color: 'inherit' }}>
          Start
        </NavLink>
      </li>
      <li>
        <NavLink
          to="/styling"
          style={{ textDecoration: 'none', color: 'inherit' }}
        >
          Styling guidelines
        </NavLink>
      </li>
      <li>
        <NavLink
          to="/users/create"
          style={{ textDecoration: 'none', color: 'inherit' }}
        >
          Create user
        </NavLink>
      </li>
    </MenuElement>
  );
}
