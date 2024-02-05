import { Box } from "@mui/material";
import { SvgIcon } from "../components/common";
import { useEffect, useState } from "react";

const LoadingPage = () => {
  const [showIcon, setShowIcon] = useState(false);

  useEffect(() => {
    const timer = setTimeout(() => {
      setShowIcon(true);
    }, 2000);

    return () => clearTimeout(timer);
  }, []);
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
      {
        <SvgIcon
          url="loading-pulse.svg"
          sx={{
            width: 50,
            height: 50,
            opacity: showIcon ? 1 : 0,
            transition: "opacity 2s",
          }}
        />
      }
    </Box>
  );
};

export default LoadingPage;
