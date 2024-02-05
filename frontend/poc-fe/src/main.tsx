import React from "react";
import ReactDOM from "react-dom/client";
import { RouterProvider } from "react-router-dom";

import { ThemeProvider } from "@mui/material";

import { SignalRProvider } from "./context/signalR.context.tsx";
import { API_BASE_URL } from "./config.ts";
import router from "./routes.tsx";
import LoadingPage from "./pages/loading.page.tsx";
import { theme } from "./theme.ts";

import './styles.css';

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <SignalRProvider baseUrl={API_BASE_URL}>
      <ThemeProvider theme={theme}>
        <RouterProvider router={router} fallbackElement={<LoadingPage />} />
      </ThemeProvider>
    </SignalRProvider>
  </React.StrictMode>
);
