import React from "react";
import ReactDOM from "react-dom/client";
import { RouterProvider } from "react-router-dom";

import { ThemeProvider } from "@mui/material";

import router from "./routes.tsx";
import LoadingPage from "./pages/loading.page.tsx";
import { theme } from "./theme.ts";

import './styles.css';

ReactDOM.createRoot(document.getElementById("root")!).render(
    <ThemeProvider theme={theme}>
      <RouterProvider router={router} fallbackElement={<LoadingPage />} />
    </ThemeProvider>
);
