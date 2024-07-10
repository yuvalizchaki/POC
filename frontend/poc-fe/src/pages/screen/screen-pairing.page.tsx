import { useCallback, useState } from "react";

import { useSignalR } from "../../hooks/useSignalR";
import { PairingCodeDto, ScreenAddedDto } from "../../types/signalR.types";
import { API_GUEST_HUB_URL } from "../../config";
import { useScreenInfoContext } from "../../hooks/useScreenInfoContext";
import { useNavigate } from "react-router-dom";
import { Box, Typography } from "@mui/material";
import { AppLoader } from "../../components/common";

// TODO: Use specific endpoint for setScreenInfo (instead of hard coded), with display name
const ScreenPairingPage = () => {
  const { setToken } = useScreenInfoContext();
  const [pairingCode, setPairingCode] = useState<string | null>(null);
  const navigate = useNavigate();

  const handleScreenAdded = useCallback(
    (message: ScreenAddedDto) => {
      console.log("[DEBUG] screen added", message);
      setToken(message);
      navigate({ pathname: "/screen/content" });
    },
    [setToken, navigate]
  );

  const handlePairingCode = useCallback(
    (message: PairingCodeDto) => {
      console.log("[DEBUG] paring code", message);
      setPairingCode(message);
    },
    [setPairingCode]
  );

  useSignalR({
    connectParams: {
      hubUrl: API_GUEST_HUB_URL,
    },
    commandHandlers: {
      screenAdded: handleScreenAdded,
      pairCode: handlePairingCode,
    },
  });

  return (
    <Box
      sx={{
        width: "100%",
        height: "100%",
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        alignItems: "center",
        boxSizing: "border-box",
        p: 4,
        bgcolor: "background.paper",
        boxShadow: 3,
        borderRadius: 2,
      }}
    >
      <Typography variant="h3" gutterBottom textAlign="center">
        Screen Pairing Code
      </Typography>
      {pairingCode ? (
        <Typography
          variant="h1"
          color="primary"
          gutterBottom
          textAlign="center"
        >
          {pairingCode}
        </Typography>
      ) : (
        <AppLoader sx={{ width: 145.625, height: 145.625 }} />
      )}
      <Typography variant="h5" textAlign="center">
        Contact your administrator with this code to pair the screen.
      </Typography>
    </Box>
  );
};
export default ScreenPairingPage;
