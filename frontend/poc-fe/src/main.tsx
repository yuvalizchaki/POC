import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.tsx";
import { SignalRProvider } from "./hooks/SignalRProvider.tsx";
import { API_BASE_URL } from "./config.ts";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <SignalRProvider baseUrl={API_BASE_URL}>
      <App />
    </SignalRProvider>
  </React.StrictMode>
);
