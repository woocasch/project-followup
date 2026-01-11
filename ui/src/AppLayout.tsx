// import type { MouseEvent } from 'react';

import styled from '@emotion/styled';
import { Outlet } from 'react-router';
import { UserProfile } from './components';
import Footer from './layout-elements/footer';
import Menu from './layout-elements/menu';
import { theme } from './theme';
import ThemeSwitcher from './theme-switcher';

const AppLayout = styled.div(`
  width: 100%;
  min-height: 100vh;
  display: grid;
  grid-template-rows: auto 1fr auto;
  grid-template-areas:
    "header"
    "main"
    "footer";
`);

const Header = styled.header(`
  background-color: ${theme.colors.primary};
  width: 100%;
  display: grid;
  grid-template-columns: 1fr auto auto;
  grid-area: header;
  padding: ${theme.spaces.small};
  gap: ${theme.spaces.medium};
  align-items: center;
`);

const Main = styled.main(`
  padding: 0.5em;
  grid-area: main;
  overflow-y: auto;
`);

const FooterContainer = styled.footer(`
  grid-area: footer;
`);

export default function AppLayoutComponent() {
  return (
    <AppLayout>
      <Header>
        <Menu />
        <ThemeSwitcher />
        <UserProfile />
      </Header>
      <Main>
        <Outlet />
      </Main>
      <FooterContainer>
        <Footer />
      </FooterContainer>
    </AppLayout>
  );
}
