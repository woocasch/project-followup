// import type { MouseEvent } from 'react';
import { Outlet } from 'react-router';
import './AppLayout.scss';
import SidebarComponent from './sidebar/sidebar';

export default function AppLayoutComponent() {
  return (
    <div className="app-layout">
      <SidebarComponent />
      <Outlet />
    </div>
  );
}
