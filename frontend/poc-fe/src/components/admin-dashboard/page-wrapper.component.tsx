import React from "react";

import { Box } from "@mui/material";

interface DashboardPageWrapperProps {
  children: React.ReactNode;
}

export const DashboardPageWrapper: React.FC<DashboardPageWrapperProps> = ({
  children,
}) => {
  return <Box sx={{ p: 2 }}>{children}</Box>;
};
