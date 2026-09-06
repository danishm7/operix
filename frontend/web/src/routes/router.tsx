import { createBrowserRouter } from "react-router-dom";

import LoginPage from "@/features/auth/LoginPage";
import DashboardPage from "@/features/dashboard/DashboardPage";
import UserFormPage from "@/features/users/UserFormPage";
import UsersPage from "@/features/users/UsersPage";
import AppLayout from "@/layouts/AppLayout";
import ProtectedRoute from "@/routes/ProtectedRoute";

export const router = createBrowserRouter([
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
            element: <DashboardPage />,
          },
          {
            path: "/users",
            element: <UsersPage />,
          },
          {
            path: "/users/new",
            element: <UserFormPage />,
          },
          {
            path: "/users/:id/edit",
            element: <UserFormPage />,
          },
        ],
      },
    ],
  },
]);
