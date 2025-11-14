import HomePage from '@home/home';
import CreateProject from '@projects/create-project';
import ReactDOM from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router';
import AppLayoutComponent from './AppLayout';
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
        path: '/projects',
        children: [
          {
            index: true,
            Component: HomePage,
          },
          {
            path: 'create',
            Component: CreateProject,
          },
        ],
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
