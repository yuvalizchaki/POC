import React from "react";
import { ScreenInfoProvider } from "../../context/screenInfo.context";
import { Outlet } from "react-router-dom";
import { ThemeProvider } from "@mui/material";
import { screenTheme } from "./theme";
import { SignalRProvider } from "../../context/signalR.context";
import { API_BASE_URL } from "../../config";
import { ScreenSupportBar } from "../../components/screen-support-bar/screen-support-bar.component";
import "./styles.css";

const ScreenPage: React.FC = () => (
  <SignalRProvider baseUrl={API_BASE_URL}>
    <ThemeProvider theme={screenTheme}>
      <ScreenInfoProvider>
        <ScreenSupportBar />
        <Outlet />
      </ScreenInfoProvider>
    </ThemeProvider>
  </SignalRProvider>
);

export default ScreenPage;
