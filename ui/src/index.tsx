import HomePage from '@home/home';
import ReactDOM from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router';
import AppLayoutComponent from './AppLayout';
import HomePage from '@home/home';
import '@assets/reset.scss';
import '@assets/themes/light.scss';
import '@assets/themes/dark.scss';
import '@assets/main.scss';
import StylingPage from './styling';

const router = createBrowserRouter([
  {
    path: '/',
    Component: AppLayoutComponent,
    children: [
      {
        index: true,
        Component: HomePage,
      },
      {
        path: '/styling',
        Component: StylingPage,
      },
      {
        path: '*',
        Component: HomePage,
      },
    ],
  },
]);

const rootEl = document.getElementById('root');
if (rootEl) {
  const root = ReactDOM.createRoot(rootEl);
  root.render(<RouterProvider router={router} />);
}
