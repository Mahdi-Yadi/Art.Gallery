import { createBrowserRouter, Outlet, RouterProvider } from 'react-router';
// ==============|Components|============== //
import App from '../../app/App';
import MainPage from '../../pages/MainPage';
import NotFoundPage from '../../pages/NotFoundPage';
import LoginPage from '../../pages/LoginPage';
import RegisterPage from '../../pages/RegisterPage';
import ArtistPage from '../../pages/ArtistPage';

const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      {
        index: true,
        element: <MainPage />,
        errorElement: <Error />,
      },
      {
        path: "Login",
        element: <LoginPage />,
        errorElement: <Error />,
      },
      {
        path: "Register",
        element: <RegisterPage />,
        errorElement: <Error />,
      },
      {
        path: "*",
        element: <NotFoundPage />,
        errorElement: <Error />,
      },
      {
        path: "Artist-Profile/:name",
        element: <ArtistPage />,
        errorElement: <Error />,
      },
    ],
  },
]);

const AppRouter = () => {
  return (
    <RouterProvider router={router}>
      <>
        <Outlet />
      </>
    </RouterProvider>
  );
};

export default AppRouter;
