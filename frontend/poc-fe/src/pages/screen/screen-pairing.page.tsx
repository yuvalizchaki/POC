import { useCallback, useState } from "react";

import { useSignalR } from "../../hooks/useSignalR";
import { PairingCodeDto, ScreenAddedDto } from "../../types/signalR.types";
import { API_GUEST_HUB_URL } from "../../config";
import { useScreenInfo } from "../../hooks/useScreenInfo";
import { useNavigate } from "react-router-dom";

const ScreenPairingPage = () => {
  const { setScreenInfo } = useScreenInfo();
  const[pairingCode, setPairingCode] = useState<string | null>(null);
  const navigate = useNavigate();

  const handleScreenAdded = useCallback(
    (message: ScreenAddedDto) => {
      console.log("[DEBUG] screen added", message);
      setScreenInfo(message);
      navigate({ pathname: "/screen/content" });
    },
    [setScreenInfo, navigate]
  );

  const handlePairingCode = useCallback(
    (message: PairingCodeDto) => {
      console.log("[DEBUG] paring code", message);
      setPairingCode(message);
    },
    [setPairingCode]
  );

  useSignalR({
    hubUrl: API_GUEST_HUB_URL,
    commandHandlers: {
      screenAdded: handleScreenAdded,
      pairCode: handlePairingCode,
    },
  });

  return (
    <>
      <h1>Screen Code: {pairingCode}</h1>
    </>
  );
};
export default ScreenPairingPage;
