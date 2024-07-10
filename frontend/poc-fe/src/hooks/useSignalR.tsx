import { useEffect } from "react";
import { SignalRHandlers } from "../types/signalR.types";
import { ConnectParams, useSignalRContext } from "../context/signalR.context";

interface UseSignalRProps {
  commandHandlers: Partial<SignalRHandlers>;
  connectParams: ConnectParams
}

export const useSignalR = ({
  commandHandlers,
  connectParams
}: UseSignalRProps) => {
  const { connect, bindHandlers, unbindHandlers, getConnection } = useSignalRContext();

  useEffect(() => {
    connect(connectParams);
    return () => {
      
    }
  }, [connectParams, connect]);
  useEffect(() => {
    const connection = getConnection(connectParams.hubUrl);
    if (commandHandlers && connection) {
      bindHandlers(connectParams.hubUrl, commandHandlers);
    }

    return () => {
      if (commandHandlers && connection) {
        unbindHandlers(connectParams.hubUrl, commandHandlers);
      }
    }
  }, [connectParams.hubUrl, commandHandlers, bindHandlers, getConnection, unbindHandlers]);
};