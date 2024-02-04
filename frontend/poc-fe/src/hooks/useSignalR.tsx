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
  const { connect, bindHandlers, getConnection } = useSignalRContext();

  useEffect(() => {
    connect(hubUrl);
  }, [hubUrl, connect]);

  useEffect(() => {
    const connection = getConnection(hubUrl);
    if (commandHandlers && connection) {
      bindHandlers(hubUrl, commandHandlers);
    }
  }, [hubUrl, commandHandlers, bindHandlers, getConnection]);

  useEffect(() => {
    const connection = getConnection(hubUrl);
    if (!connection) return;

    const handleConnect = () => onConnect?.();
    const handleDisconnect = () => onDisconnect?.();

    connection.onreconnected(handleConnect);
    connection.onclose(handleDisconnect);
    
    // return () => {
    //   connection.offreconnected(handleConnect);
    //   connection.offclose(handleDisconnect);
    // };
  }, [hubUrl, onConnect, onDisconnect, getConnection]);
};
