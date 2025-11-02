import styled from "@emotion/styled";
import { NavLink } from "react-router";

const MenuItem = styled.li(`
`);
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
            <MenuItem>
                <NavLink to="/" style={{ textDecoration: 'none', color: 'inherit' }}>Start</NavLink>
            </MenuItem>
            <MenuItem>
                <NavLink to="/styling" style={{ textDecoration: 'none', color: 'inherit' }}>Styling guidelines</NavLink>
            </MenuItem>
        </MenuElement>
    );
}
