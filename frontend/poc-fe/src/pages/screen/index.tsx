import React from "react";
import { ScreenInfoProvider } from "../../context/screenInfo.context";
import { Outlet } from "react-router-dom";

const ScreenPage: React.FC = () => {
  return (
    <ScreenInfoProvider>
      <Outlet />
    </ScreenInfoProvider>
  );
};

export default ScreenPage;
