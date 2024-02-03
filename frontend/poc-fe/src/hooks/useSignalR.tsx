import { useCallback, useEffect, useMemo, useState } from "react";
import * as signalR from "@microsoft/signalr";
import { SignalRHandlers } from "../types/SignalR";
import { API_BASE_URL } from "../config";

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
  const [connection, setConnection] = useState<signalR.HubConnection>();

  const createConnection = () => {
    const con = new signalR.HubConnectionBuilder()
      .withUrl(API_BASE_URL + hubUrl)
      .configureLogging(signalR.LogLevel.Information)
      .withAutomaticReconnect()
      .build();

    setConnection(con);
  };

  useEffect(() => {
    createConnection();
  }, [hubUrl]);

  useEffect(() => {
    if (connection) {
      try {
        connection.start()
          .then(() => {
            console.log("SignalR Connected.");
            onConnect?.();
            Object.entries(commandHandlers).forEach(([commandName, handler]) => {
              console.log('[DEBUG] adding command name: ', commandName);
              connection.on(commandName, handler);
            });
          })
          .catch((err) => {
            console.error(err);
            setTimeout(createConnection, 5000);
          });

        connection.onclose(async () => {
          onDisconnect?.();
          await createConnection();
        });
      } catch (error) {
        console.error(error);
      }
    }

    return () => {
      if (connection) {
        Object.keys(commandHandlers).forEach((commandName) => {
          connection.off(commandName);
        });
        connection.stop();
      }
    };
  }, [connection, onDisconnect, commandHandlers, onConnect]);

  return {};
};
