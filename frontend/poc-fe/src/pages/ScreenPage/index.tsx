import React, { useState } from "react";
import { useSignalR } from "../../hooks/useSignalR";
import { ScreenAddedDto } from "../../types/SignalR";
import { API_GUEST_HUB_URL } from "../../config";
import OrdersScreenPage from "./OrdersScreenPage";

const ScreenPage: React.FC = () => {
  const [screenInfo, setScreenInfo] = useState<ScreenAddedDto | null>(null);

  const handleScreenAdded = (message: ScreenAddedDto) => {
    console.log("[DEBUG] screen added", message);
    setScreenInfo(message);
  };

  useSignalR({
    hubUrl: API_GUEST_HUB_URL,
    // onConnect: () => {
    //   alert("Connected");
    // },
    // onDisconnect: () => {
    //   alert("Disconnected");
    // },
    commandHandlers: {
      screenAdded: handleScreenAdded,
    },
  });

  return (
    <div>
      {screenInfo ? (
        <OrdersScreenPage screenInfo={screenInfo} />
      ) : (
        <p>{"[No Content]"}</p>
      )}
    </div>
  );
};

export default ScreenPage;
