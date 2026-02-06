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
import CreateUser from '@user-accounts/create-user';
import ActivateAccount from './account/activate-account';
import { AuthProvider, ProtectedRoute } from './infrastructure/auth';
import { loadConfiguration } from './infrastructure/configuration';
import StylingPage from './styling';
import UserInfoPage from './user-info/user-info';
import ProjectDetails from './projects/project-details';

const router = createBrowserRouter([
  {
    path: '/',
    Component: AppLayoutComponent,
    children: [
      {
        index: true,
        element: (
          <ProtectedRoute>
            <HomePage />
          </ProtectedRoute>
        ),
      },
      {
        path: '/projects',
        children: [
          {
            index: true,
            element: (
              <ProtectedRoute>
                <HomePage />
              </ProtectedRoute>
            ),
          },
          {
            path: 'create',
            element: (
              <ProtectedRoute>
                <CreateProject />
              </ProtectedRoute>
            ),
          },
          {
            path: ':projectId/edit',
            element: (
              <ProtectedRoute>
                <EditProject />
              </ProtectedRoute>
            ),
          },
          {
            path: ':projectId/details',
            element: (
              <ProtectedRoute>
                <ProjectDetails />
              </ProtectedRoute>
            ),
          },
        ],
      },
      {
        path: '/users',
        children: [
          {
            index: true,
            element: (
              <ProtectedRoute roles={['manage-user']}>
                <HomePage />
              </ProtectedRoute>
            ),
          },
          {
            path: 'create',
            element: (
              <ProtectedRoute roles={['create-user']}>
                <CreateUser />
              </ProtectedRoute>
            ),
          },
        ],
      },
      {
        path: '/account',
        children: [
          {
            path: 'activate/:linkCode',
            Component: ActivateAccount,
          },
        ],
      },
      {
        path: '/styling',
        element: (
          <ProtectedRoute>
            <StylingPage />
          </ProtectedRoute>
        ),
      },
      {
        path: '/user-info',
        element: (
          <ProtectedRoute>
            <UserInfoPage />
          </ProtectedRoute>
        ),
      },
      {
        path: '*',
        element: (
          <ProtectedRoute>
            <HomePage />
          </ProtectedRoute>
        ),
      },
    ],
  },
]);

async function bootstrapApp() {
  await loadConfiguration();

  const rootEl = document.getElementById('root');
  if (rootEl) {
    const root = ReactDOM.createRoot(rootEl);
    root.render(
      <AuthProvider>
        <RouterProvider router={router} />
      </AuthProvider>,
    );
  }
}

bootstrapApp();
