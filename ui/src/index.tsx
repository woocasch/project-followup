import HomePage from '@home/home';
import CreateProject from '@projects/create-project';
import EditProject from '@projects/edit-project';
import ReactDOM from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router';
import AppLayoutComponent from './AppLayout';
import '@assets/reset.scss';
import '@assets/themes/light.scss';
import '@assets/themes/dark.scss';
import '@assets/themes/sizes.scss';
import '@assets/main.scss';
import StylingPage from './styling';
import { loadConfiguration } from './infrastructure/configuration';
import CreateUser from '@user-accounts/create-user';

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
          {
            path: ':projectId/edit',
            Component: EditProject,
          },
        ],
      },
      {
        path: '/users',
        children: [
          {
            index: true,
            Component: HomePage,
          },
          {
            path: 'create',
            Component: CreateUser,
          },
        ]
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

async function bootstrapApp() {
  await loadConfiguration();

  const rootEl = document.getElementById('root');
  if (rootEl) {
    const root = ReactDOM.createRoot(rootEl);
    root.render(<RouterProvider router={router} />);
  }
}

bootstrapApp();
