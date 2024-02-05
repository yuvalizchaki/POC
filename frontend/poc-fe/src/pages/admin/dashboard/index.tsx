import { Suspense, useState } from "react";
import { Outlet } from "react-router-dom";

import { DashbaordSidebar } from "../../../components/admin-dashboard";
import LoadingPage from "../../loading.page";
import { DashboardAppBar } from "../../../components/admin-dashboard/app-bar.component";
import { Box, Theme, useMediaQuery } from "@mui/material";
import { SIDEBAR_WIDTH } from "../../../components/admin-dashboard/sidebar.component";
import { DashboardPageWrapper } from "../../../components/admin-dashboard/page-wrapper.component";

const DashboardPage = () => {
  const isMobile = useMediaQuery((theme: Theme) =>
    theme.breakpoints.down("sm")
  );
  const [isDrawerOpen, setIsDrawerOpen] = useState(false);

  return (
    <>
      <DashbaordSidebar
        isOpen={isDrawerOpen}
        onClose={() => {
          setIsDrawerOpen(false);
        }}
      />
      <Box
        sx={{
          display: "flex",
          flexDirection: "column",
          width: "100%",
          height: "100%",
          pl: !isMobile && isDrawerOpen ? SIDEBAR_WIDTH : 0,
          boxSizing: "border-box",
        }}
      >
        <DashboardAppBar
          isOpen={isDrawerOpen}
          toggleOpen={(newValue) => {
            setIsDrawerOpen(newValue);
          }}
        />
        <Suspense fallback={<LoadingPage />}>
          <DashboardPageWrapper>
          <Outlet />
          </DashboardPageWrapper>
        </Suspense>
      </Box>
    </>
  );
};
export default DashboardPage;
