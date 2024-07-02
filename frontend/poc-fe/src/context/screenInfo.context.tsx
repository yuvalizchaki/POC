import React, {
  createContext,
  ReactNode,
  useCallback,
  useEffect,
  useMemo,
  useState,
} from "react";
import axios, { AxiosResponse } from "axios";
import { ScreenInfo } from "../types/screenInfo.types";
import { LOCALSTORAGE_KEY_SCREEN_TOKEN, API_BASE_URL } from "../config";

interface ScreenInfoProviderProps {
  children: ReactNode;
}

export interface ScreenInfoContextType {
  screenInfo: ScreenInfo | null;
  setScreenInfo: (info: ScreenInfo | null) => void;
  token: string | null;
  setToken: (token: string) => void;
  fetchOrders: () => Promise<AxiosResponse>;
}

export const ScreenInfoContext = createContext<ScreenInfoContextType>({
  screenInfo: null,
  setScreenInfo: () => {},
  token: null,
  setToken: () => {},
  fetchOrders: async () => {
    return {} as AxiosResponse;
  },
});

export const ScreenInfoProvider: React.FC<ScreenInfoProviderProps> = ({
  children,
}) => {
  const [screenInfo, setScreenInfo] = useState<ScreenInfo | null>(null);
  const [token, setTokenState] = useState<string | null>(() => {
    // Initialize state from local storage
    return localStorage.getItem(LOCALSTORAGE_KEY_SCREEN_TOKEN);
  });

  const setToken = useCallback((newToken: string | null) => {
    setTokenState(newToken);
    if (newToken !== null) {
      localStorage.setItem(LOCALSTORAGE_KEY_SCREEN_TOKEN, newToken);
    } else {
      localStorage.removeItem(LOCALSTORAGE_KEY_SCREEN_TOKEN);
    }
  }, []);

  // Sync token state with local storage
  useEffect(() => {
    const storedToken = localStorage.getItem(LOCALSTORAGE_KEY_SCREEN_TOKEN);
    if (storedToken !== token) {
      setTokenState(storedToken);
    }
  }, [token]);

  const client = useMemo(() => {
    const axiosInstance = axios.create({ baseURL: API_BASE_URL });
    if (token) {
      axiosInstance.defaults.headers.common[
        "Authorization"
      ] = `Bearer ${token}`;
    } else {
      client.defaults.headers.common["Authorization"] = "";
    }
    return axiosInstance;
  }, [token]);

  const fetchOrders = useCallback(async () => {
    try {
      const response = await client.get("/orders");
      return response;
    } catch (error) {
      console.error("Failed to fetch orders:", error);
      throw error;
    }
  }, [client]);

  const contextValue = {
    screenInfo,
    setScreenInfo,
    token,
    setToken,
    fetchOrders,
  };

  return (
    <ScreenInfoContext.Provider value={contextValue}>
      {children}
    </ScreenInfoContext.Provider>
  );
};
