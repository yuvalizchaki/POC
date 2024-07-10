import { useEffect } from "react";
import { SignalRHandlers } from "../types/signalR.types";
import { useSignalRContext } from "../context/signalR.context";

interface UseSignalRProps {
  hubUrl: string;
  commandHandlers: Partial<SignalRHandlers>;
  onConnect?: () => void;
  onDisconnect?: () => void;
  token?: string; // Added token prop
}

export const useSignalR = ({
  hubUrl,
  commandHandlers,
  onConnect,
  onDisconnect,
  token, // Added token prop
}: UseSignalRProps) => {
  const { connect, bindHandlers, unbindHandlers, getConnection } = useSignalRContext();

  useEffect(() => {
    connect({ hubUrl, token, onConnect, onDisconnect });
  }, [hubUrl, token, onConnect, onDisconnect, connect]); // Include token in dependencies

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