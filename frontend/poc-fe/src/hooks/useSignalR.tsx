import { useEffect } from "react";
import { SignalRHandlers } from "../types/signalR.types";
import { ConnectParams, useSignalRContext } from "../context/signalR.context";

interface UseSignalRProps {
  connectParams: ConnectParams
}

export const useSignalR = ({
  connectParams
}: UseSignalRProps) => {
  const { connect, /*bindHandlers, unbindHandlers, getConnection*/ } = useSignalRContext();

  useEffect(() => {
    connect(connectParams);
  }, [connectParams, connect]);

  // useEffect(() => {
  //   const connection = getConnection(connectParams.hubUrl);
  //   console.log('[DEBUG] Binding handlers, connection: ', connection, 'connectParams: ', connectParams, " commandHandlers: ", commandHandlers);

  //   if (commandHandlers && connection) {
  //     bindHandlers(connectParams.hubUrl, commandHandlers);
  //   }

  //   return () => {
  //     if (commandHandlers && connection) {
  //       unbindHandlers(connectParams.hubUrl, commandHandlers);
  //     }
  //   }
  // }, [connectParams.hubUrl, commandHandlers, bindHandlers, getConnection, unbindHandlers]);
};