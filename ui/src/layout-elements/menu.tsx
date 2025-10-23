import styled from "@emotion/styled";
import { theme } from "../theme";
import { NavLink } from "react-router";

const MenuItem = styled.li(`
  padding: ${theme.colors.spacing.small};
  margin-right: ${theme.colors.spacing.small};
  border-radius: ${theme.colors.borderRadius.medium};
  &:hover {
    background-color: ${theme.colors.primary.light};
  }

  &:has(a.active) {
    background-color: ${theme.colors.background.accent};
  }
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
                <NavLink to="/" style={{ textDecoration: 'none', color: 'inherit' }}>Home</NavLink>
            </MenuItem>
            <MenuItem>
                <NavLink to="/about" style={{ textDecoration: 'none', color: 'inherit' }}>About</NavLink>
            </MenuItem>
            <MenuItem>
                <NavLink to="/contact" style={{ textDecoration: 'none', color: 'inherit' }}>Contact</NavLink>
            </MenuItem>
        </MenuElement>
    );
}
