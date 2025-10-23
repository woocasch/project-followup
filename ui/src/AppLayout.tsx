// import type { MouseEvent } from 'react';
import { Outlet } from 'react-router';
import Footer from './layout-elements/footer';
import ThemeSwitcher from './theme-switcher';
import styled from '@emotion/styled';
import { theme } from './theme';
import Menu from './layout-elements/menu';

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
  background-color: ${theme.colors.background.secondary};
  padding: ${theme.sizes.spacing.small};
  width: 100%;
  display: grid;
  line-height: ${theme.sizes.spacing.large};
  grid-template-columns: 1fr auto;
  grid-area: header;
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
