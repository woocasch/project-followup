import styled from "@emotion/styled";
import { theme } from "../theme";
import { NavLink } from "react-router";

const MenuItem = styled.li(`
  padding: ${theme.sizes.spacing.small};
  margin-right: ${theme.sizes.spacing.small};
  border-radius: ${theme.sizes.borderRadius.medium};
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
                <NavLink to="/" style={{ textDecoration: 'none', color: 'inherit' }}>Start</NavLink>
            </MenuItem>
            <MenuItem>
                <NavLink to="/projects" style={{ textDecoration: 'none', color: 'inherit' }}>Projects</NavLink>
            </MenuItem>
        </MenuElement>
    );
}
