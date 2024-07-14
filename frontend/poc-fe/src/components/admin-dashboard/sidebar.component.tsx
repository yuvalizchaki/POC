import React from "react";
import { Link } from "react-router-dom";

import {
  Box,
  Divider,
  Drawer,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  Theme,
  useMediaQuery,
} from "@mui/material";

import HomeIcon from "@mui/icons-material/Home";
import TvIcon from "@mui/icons-material/Tv";
// import SettingsIcon from "@mui/icons-material/Settings";
import LogoutIcon from '@mui/icons-material/Logout';

export const SIDEBAR_WIDTH = "300px";

const menuItems = [
  { title: "Home", path: "/admin/dashboard/home", icon: <HomeIcon /> },
  {
    title: "Screen Management",
    path: "/admin/dashboard/screen-management",
    icon: <TvIcon />,
  },
  // {
  //   title: "Settings",
  //   path: "/admin/dashboard/settings",
  //   position: "end",
  //   icon: <SettingsIcon />,
  // },
  {
    title: "Logout",
    path: "/admin/logout",
    position: "end",
    icon: <LogoutIcon/>,
  },
];

interface DashboardSidebarProps {
  isOpen: boolean;
  onClose: () => void;
}

export const DashbaordSidebar: React.FC<DashboardSidebarProps> = ({
  isOpen,
  onClose,
}) => {
  const isMobile = useMediaQuery((theme: Theme) =>
    theme.breakpoints.down("sm")
  );

  return (
    <>
      <Drawer
        transitionDuration={0}
        variant={isMobile ? "temporary" : "persistent"}
        anchor="left"
        open={isOpen}
        onClose={() => {
          onClose();
        }}
        PaperProps={{
          sx: {
            width: SIDEBAR_WIDTH,
            "& .MuiDrawer-paper": {
              width: SIDEBAR_WIDTH,
            },
          },
        }}
      >
        <Box
          sx={{
            width: "100%",
            bgcolor: "background.paper",
            height: "100%",
          }}
        >
          <List
            sx={{
              height: "100%",
              display: "flex",
              flexDirection: "column",
              boxSizing: "border-box",
            }}
          >
            {menuItems
              .filter((item) => item.position !== "end")
              .map((item) => (
                <ListItem key={item.path} disablePadding>
                  <ListItemButton component={Link} to={item.path}>
                    {item.icon && <ListItemIcon>{item.icon}</ListItemIcon>}
                    <ListItemText primary={item.title} />
                  </ListItemButton>
                </ListItem>
              ))}
            <div style={{ flex: "1 0 0" }} />
            <Divider />
            {menuItems
              .filter((item) => item.position === "end")
              .map((item) => (
                <ListItem key={item.path} disablePadding>
                  <ListItemButton component={Link} to={item.path}>
                    {item.icon && <ListItemIcon>{item.icon}</ListItemIcon>}
                    <ListItemText primary={item.title} />
                  </ListItemButton>
                </ListItem>
              ))}
          </List>
        </Box>
      </Drawer>
    </>
  );
};
