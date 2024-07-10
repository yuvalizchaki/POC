// signalR.context.tsx
import React, { createContext, useMemo, FC, useContext, useRef } from "react";
import * as signalR from "@microsoft/signalr";
import { SignalRHandlers } from "../types/signalR.types";

interface ConnectParams { hubUrl: string, token?: string, onConnect?: () => void, onDisconnect?: () => void }

interface SignalRProviderProps {
  baseUrl: string;
  children: React.ReactNode;
}

interface SignalRContextValue {
  connect: (props: ConnectParams) => void;
  bindHandlers: (
    hubUrl: string,
    commandHandlers: Partial<SignalRHandlers>
  ) => void;
  unbindHandlers: (
    hubUrl: string,
    commandHandlers: Partial<SignalRHandlers>
  ) => void;
  getConnection: (hubUrl: string) => signalR.HubConnection | undefined;
}

export const SignalRContext = createContext<SignalRContextValue | null>(null);

export const SignalRProvider: FC<SignalRProviderProps> = ({
  baseUrl,
  children,
}) => {
  const connectionsRef = useRef<{ [key: string]: signalR.HubConnection }>({});

  const connect = ({
    hubUrl,
    token,
    onConnect,
    onDisconnect
  }: ConnectParams) => {
    const connections = connectionsRef.current;
    console.log('[DEBUG] connections:', connections);
    if (connections[hubUrl]) {
      console.log("Already connected to this hub.");
      return;
    }

    const con = new signalR.HubConnectionBuilder()
      .withUrl(baseUrl + hubUrl, token ? { accessTokenFactory: () => token } : {})
      .configureLogging(signalR.LogLevel.Information)
      .withAutomaticReconnect()
      .build();

    con
      .start()
      .then(() => {
        console.log("SignalR Connected to " + hubUrl);
        onConnect?.();
      })
      .catch((err) =>
        console.error("SignalR Connection Error on " + hubUrl + ": ", err)
      );

    con.onclose(() => { onDisconnect?.() });

    connections[hubUrl] = con;
  };

  const bindHandlers = (
    hubUrl: string,
    commandHandlers: Partial<SignalRHandlers>
  ) => {
    const connection = connectionsRef.current?.[hubUrl];
    if (!connection) {
      console.error("No connection found for hub: " + hubUrl);
      return;
    }

    Object.entries(commandHandlers).forEach(([commandName, handler]) => {
      if (handler) {
        connection.on(commandName, handler);
      }
    });
  };

  const unbindHandlers = (
    hubUrl: string,
    commandHandlers: Partial<SignalRHandlers>
  ) => {
    const connection = connectionsRef.current?.[hubUrl];
    if (!connection) {
      console.error("No connection found for hub: " + hubUrl);
      return;
    }

    Object.entries(commandHandlers).forEach(([commandName, handler]) => {
      if (handler) {
        connection.off(commandName, handler);
      }
    });
  };

  const getConnection = (hubUrl: string) => {
    return connectionsRef.current?.[hubUrl];
  };

  const value = useMemo(
    () => ({
      connect,
      bindHandlers,
      unbindHandlers,
      getConnection: (hubUrl: string) => getConnection(hubUrl),
    }),
    []
  );

  return (
    <SignalRContext.Provider value={value}>{children}</SignalRContext.Provider>
  );
};

// eslint-disable-next-line react-refresh/only-export-components
export const useSignalRContext = () => {
  const context = useContext(SignalRContext);
  if (!context) {
    throw new Error("useSignalRContext must be used within a SignalRProvider");
  }
  return context;
};