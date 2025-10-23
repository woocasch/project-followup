import ReactDOM from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router';
import AppLayoutComponent from './AppLayout';
import HomePage from './home/home';
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
