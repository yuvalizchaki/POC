import { Box } from "@mui/material";
import { AppLoader } from "../components/common";

const LoadingPage = () => {
  return (
    <Box
      sx={{
        display: "flex",
        width: "100%",
        height: "100%",
        justifyContent: "center",
        alignItems: "center",
      }}
    >
      <AppLoader fadeInTime={2} />
    </Box>
  );
};

export default LoadingPage;
