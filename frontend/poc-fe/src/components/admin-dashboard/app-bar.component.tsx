import React, { ReactNode } from "react";

import { AppBar, IconButton, Toolbar, useTheme } from "@mui/material";

import MenuIcon from "@mui/icons-material/Menu";
import ChevronLeftIcon from "@mui/icons-material/ChevronLeft";
import ChevronRightIcon from "@mui/icons-material/ChevronRight";

interface DashboardAppBarProps {
  isOpen: boolean;
  toggleOpen: (newValue: boolean) => void;
  children?: ReactNode;
}

export const DashboardAppBar: React.FC<DashboardAppBarProps> = ({
  isOpen,
  toggleOpen,
  children,
}) => {
  const theme = useTheme();
  return (
    <>
      <AppBar position="relative">
        <Toolbar>
          <IconButton
            size="large"
            edge="start"
            color="inherit"
            onClick={() => {
              toggleOpen(!isOpen);
            }}
          >
            {isOpen ? (
              theme.direction === "ltr" ? (
                <ChevronLeftIcon />
              ) : (
                <ChevronRightIcon />
              )
            ) : (
              <MenuIcon />
            )}
          </IconButton>
          {children}
        </Toolbar>
      </AppBar>
    </>
  );
};
