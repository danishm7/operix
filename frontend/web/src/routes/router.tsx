import { createBrowserRouter } from "react-router-dom";

import App from "@/App";
import LoginPage from "@/features/auth/LoginPage";
import AppLayout from "@/layouts/AppLayout";
import ProtectedRoute from "@/routes/ProtectedRoute";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
  },
  {
    path: "/login",
    element: <LoginPage />,
  },
  {
    element: <ProtectedRoute />,
    children: [
      {
        element: <AppLayout />,
        children: [
          {
            path: "/dashboard",
            element: <div>Dashboard</div>,
          },
        ],
      },
    ],
  },
]);
