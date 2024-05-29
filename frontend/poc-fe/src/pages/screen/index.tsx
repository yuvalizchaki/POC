import React from "react";
import { ScreenInfoProvider } from "../../context/screenInfo.context";
import { Outlet } from "react-router-dom";
import { ThemeProvider } from "@mui/material";
import { screenTheme } from "./theme";

const ScreenPage: React.FC = () => (
  <ThemeProvider theme={screenTheme}>
    <ScreenInfoProvider>
      <Outlet />
    </ScreenInfoProvider>
  </ThemeProvider>
);

export default ScreenPage;
