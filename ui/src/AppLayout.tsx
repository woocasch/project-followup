// import type { MouseEvent } from 'react';
import { Outlet } from 'react-router';
import Footer from './layout-elements/footer';
import ThemeSwitcher from './theme-switcher';
import styled from '@emotion/styled';
import { theme } from './theme';

const AppLayout = styled.div(`
  width: 100%;
`);

const Header = styled.header(`
  background-color: ${theme.colors.panelheader};
  padding: 0.5em;
  width: 100%;
  display: grid;
  line-height: 3em;
  grid-template-columns: 1fr auto;
`);

const Main = styled.main(`
  padding: 0.5em;
`)

export default function AppLayoutComponent() {
  return (
    <AppLayout>
      <Header>
        Menu
        <ThemeSwitcher />
      </Header>
      <Main>
        <Outlet />
      </Main>
      <footer>
        <Footer />
      </footer>
    </AppLayout>
  );
}
