import { DependencyList, useEffect } from "react";
import { ConnectParams, useSignalRContext } from "../context/signalR.context";

interface UseSignalRProps {
  connectParams: ConnectParams
}

export const useSignalR = ({
  connectParams,
}: UseSignalRProps) => {
  const { connect, disconnect } = useSignalRContext();

  useEffect(() => {
    connect(connectParams);
    return () => {
      disconnect(connectParams)
    }
  }, []);

  // useEffect(() => {
  //   console.log('[DEBUG] useEffect rebind');
  //   bindHandlers(connectParams.hubUrl, connectParams.commandHandlers)
  //   return () => {
  //     unbindHandlers(connectParams.hubUrl, connectParams.commandHandlers)
  //   }
  // }, [connectParams, dependencies, bindHandlers, unbindHandlers]);

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