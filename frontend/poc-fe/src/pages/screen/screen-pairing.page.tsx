import { useCallback } from "react";

import { useSignalR } from "../../hooks/useSignalR";
import { ScreenAddedDto } from "../../types/signalR.types";
import { API_GUEST_HUB_URL } from "../../config";
import { useScreenInfo } from "../../hooks/useScreenInfo";
import { useNavigate } from "react-router-dom";

const ScreenPairingPage = () => {
  const { setScreenInfo } = useScreenInfo();

  const navigate = useNavigate();

  const handleScreenAdded = useCallback(
    (message: ScreenAddedDto) => {
      console.log("[DEBUG] screen added", message);
      setScreenInfo(message);
      navigate({ pathname: "/screen/content" });
    },
    [setScreenInfo, navigate]
  );

  useSignalR({
    hubUrl: API_GUEST_HUB_URL,
    commandHandlers: {
      screenAdded: handleScreenAdded,
    },
  });

  return (
    <>
      <h1>Screen Code: {"::1" /* TODO: add screen code*/}</h1>
    </>
  );
};
export default ScreenPairingPage;
