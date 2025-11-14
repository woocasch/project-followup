import styled from '@emotion/styled';
import { NavLink } from 'react-router';

const MenuElement = styled.ul(`
  list-style: none;
  padding: 0;
  margin: 0;
  display: flex;
  flex-direction: row;
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
    </MenuElement>
  );
}
