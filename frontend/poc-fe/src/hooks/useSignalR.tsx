import { useCallback, useEffect } from "react";
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
  const connection = new signalR.HubConnectionBuilder()
    .withUrl(API_BASE_URL + hubUrl)
    .configureLogging(signalR.LogLevel.Information)
    .build();

  const startConnection = useCallback(async () => {
    try {
      await connection.start();
      console.log("SignalR Connected.");
      onConnect?.();
    } catch (err) {
      console.error(err);
      setTimeout(startConnection, 5000);
    }
  }, [connection, onConnect]);

  useEffect(() => {
    startConnection();

    connection.onclose(async () => {
      onDisconnect?.();
      await startConnection();
    });

    return () => {
      connection.stop();
    };
  }, [startConnection, connection, onDisconnect]);

  useEffect(() => {
    Object.entries(commandHandlers).forEach(([commandName, handler]) => {
      connection.on(commandName, handler);
    });

    return () => {
      Object.keys(commandHandlers).forEach((commandName) => {
        connection.off(commandName);
      });
    };
  }, [connection, commandHandlers]);

  const sendMessage = useCallback(
    async (message: object) => {
      try {
        await connection.invoke("SendMessage", JSON.stringify(message));
      } catch (err) {
        console.error(err);
      }
    },
    [connection]
  );

  return { sendMessage };
};
