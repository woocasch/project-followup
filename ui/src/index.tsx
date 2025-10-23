import ReactDOM from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router';
import AppLayoutComponent from './AppLayout';
import HomePage from '@home/home';
import ProjectsListPage from '@projects/use-cases/projects-list/projects-list';
import '@assets/reset.scss';
import '@assets/themes.scss';
import '@assets/main.scss';

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
        Component: ProjectsListPage,
      },
      {
        path: '*',
        Component: HomePage,
      }
    ],
  },
]);

const rootEl = document.getElementById('root');
if (rootEl) {
  const root = ReactDOM.createRoot(rootEl);
  root.render(<RouterProvider router={router} />);
}
