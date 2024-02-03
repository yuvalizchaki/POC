import React, { useState } from "react";
import { useSignalR } from "../hooks/useSignalR";
import { ScreenAddedDto } from "../types/SignalR";

const ScreenPage: React.FC = () => {
  const [screenInfo, setScreenInfo] = useState<ScreenAddedDto | null>(null);

  const handleScreenAdded = (message: ScreenAddedDto) => {
    console.log('[DEBUG] screen added');
    setScreenInfo(message);
  };

  useSignalR({
    hubUrl: '/guest-hub',
    onConnect: () => {
        alert('Connected');
    }, 
    onDisconnect: () => {
        alert('Disconnected');
    },
    commandHandlers: {
      screenAdded: handleScreenAdded,
    },
  });

  return (
    <div>
      {screenInfo ? (
        <div>
          <p>Screen ID: {screenInfo.id}</p>
          <p>IP: {screenInfo.ip}</p>
          <p>Screen Profile ID: {screenInfo.screenProfileId}</p>
        </div>
      ) : (
        <p>[No Content]</p>
      )}
    </div>
  );
};

export default ScreenPage;
