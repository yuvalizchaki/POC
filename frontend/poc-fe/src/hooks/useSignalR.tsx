import { useEffect } from "react";
import { SignalRHandlers } from "../types/SignalR";
import { useSignalRContext } from "./SignalRProvider";

interface UseSignalRProps {
  hubUrl: string;
  commandHandlers: Partial<SignalRHandlers>;
  onConnect?: () => void;
  onDisconnect?: () => void;
}

export const useSignalR = ({
  hubUrl,
  commandHandlers,
  onConnect,
  onDisconnect,
}: UseSignalRProps) => {
  const { connect, bindHandlers, unbindHandlers, getConnection } = useSignalRContext();

  useEffect(() => {
    connect(hubUrl, onConnect, onDisconnect);
  }, [hubUrl, onConnect, onDisconnect, connect]);

  useEffect(() => {
    const connection = getConnection(hubUrl);
    if (commandHandlers && connection) {
      bindHandlers(hubUrl, commandHandlers);
    }

    return () => {
      if (commandHandlers && connection) {
        unbindHandlers(hubUrl, commandHandlers);
      } 
    }
  }, [hubUrl, commandHandlers, bindHandlers, getConnection, unbindHandlers]);
};
