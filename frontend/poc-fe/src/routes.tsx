/* eslint-disable react-refresh/only-export-components */
import { Navigate, createBrowserRouter } from "react-router-dom";

import { lazy } from "react";

import MainPage from "./pages";
import NotFoundPage from "./pages/not-found.page";

import AdminPage from "./pages/admin";
const AdminLoginPage = lazy(() => import("./pages/admin/login.page"));
import AdminLogoutPage from "./pages/admin/logout.page";

// const AdminDashboardPage = lazy(() => import("./pages/admin/dashboard/index"));
import AdminDashboardPage from "./pages/admin/dashboard";
const HomePage = lazy(() => import("./pages/admin/dashboard/home.page"));
const ScreenManagementPage = lazy(
  () => import("./pages/admin/dashboard/screen-management.page")
);
const SettingsPage = lazy(
  () => import("./pages/admin/dashboard/settings.page")
);

import ScreenPage from "./pages/screen";
const ScreenContentPage = lazy(
  () => import("./pages/screen/screen-content.page")
);
const ScreenPairingPage = lazy(
  () => import("./pages/screen/screen-pairing.page")
);

// TODO: remove
const ScreenDemoOrdersTemplate = lazy(
  () => import("./pages/screen/demo/screen-orders-demo.page")
);

// TODO: remove
const ScreenDemoInventoryTemplate = lazy(
  () => import("./pages/screen/demo/screen-inventory-demo.page")
);

const router = createBrowserRouter([
  {
    path: "/",
    element: <MainPage />,
    errorElement: <NotFoundPage />,
    children: [
      { index: true, element: <Navigate to="screen" replace /> },
      {
        path: "admin",
        element: <AdminPage />,
        children: [
          { index: true, element: <Navigate to="dashboard" /> },
          { path: "login", element: <AdminLoginPage /> },
          { path: "logout", element: <AdminLogoutPage /> },
          {
            path: "dashboard",
            element: <AdminDashboardPage />,
            children: [
              { index: true, element: <Navigate to="home" replace /> },
              { path: "home", element: <HomePage /> },
              { path: "screen-management", element: <ScreenManagementPage /> },
              { path: "settings", element: <SettingsPage /> },
            ],
          },
        ],
      },
      {
        path: "screen",
        element: <ScreenPage />,
        children: [
          { index: true, element: <Navigate to="content" replace /> },
          { path: "content", element: <ScreenContentPage /> },
          { path: "pair", element: <ScreenPairingPage /> },
          { path: "demo-orders", element: <ScreenDemoOrdersTemplate /> }, // TODO: remove
          { path: "demo-inventory", element: <ScreenDemoInventoryTemplate /> }, // TODO: remove
        ],
      },
    ],
  },
]);

export default router;
