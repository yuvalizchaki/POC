import React from "react";
import { ScreenInfoProvider } from "../../context/screenInfo.context";
import { Outlet } from "react-router-dom";
import { ThemeProvider } from "@mui/material";
import { screenTheme } from "./theme";
import { SignalRProvider } from "../../context/signalR.context";
import { API_BASE_URL } from "../../config";
import { AppClock } from "../../components/common/app-clock.component";

const ScreenPage: React.FC = () => (
  <SignalRProvider baseUrl={API_BASE_URL}>
    <ThemeProvider theme={screenTheme}>
      <ScreenInfoProvider>
        <Outlet />
      </ScreenInfoProvider>
    </ThemeProvider>
  </SignalRProvider>
);

export default ScreenPage;
