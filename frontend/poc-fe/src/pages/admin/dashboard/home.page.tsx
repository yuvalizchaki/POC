import { Box, Typography } from "@mui/material";

import logo from "../../../assets/realtime-warehouse.png"; // Adjust the path if necessary
import { useAdminInfoContext } from "../../../hooks/useAdminInfoContext";

const HomePage = () => {
  const { adminData } = useAdminInfoContext();
  return (
    <>
      <Box
        component="img"
        src={logo}
        alt="Logo"
        style={{ width: 300, height: "auto" }}
      />
      <Typography variant="subtitle1">Version: 1.0-beta</Typography>
      {adminData && (
        <Typography variant="h5">{`Welcome, ${adminData?.username}`}</Typography>
      )}
    </>
  );
};
export default HomePage;
