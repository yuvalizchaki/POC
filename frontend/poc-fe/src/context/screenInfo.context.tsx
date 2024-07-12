import React, {
  createContext,
  ReactNode,
  useCallback,
  useEffect,
  useMemo,
  useState,
} from "react";
import axios, { AxiosInstance, AxiosResponse } from "axios";
import { ScreenMetaData } from "../types/screenMetaData.types";
import { LOCALSTORAGE_KEY_SCREEN_TOKEN, API_BASE_URL } from "../config";

interface ScreenInfoProviderProps {
  children: ReactNode;
}

export interface ScreenInfoContextType {
  screenInfo: ScreenMetaData | null;
  setScreenInfo: (info: ScreenMetaData | null) => void;
  token: string | null;
  setToken: (token: string | null) => void;
  client: AxiosInstance
}

export const ScreenInfoContext = createContext<ScreenInfoContextType>({
  screenInfo: null,
  setScreenInfo: () => { },
  token: null,
  setToken: () => { },
  client: {} as AxiosInstance
});

export const ScreenInfoProvider: React.FC<ScreenInfoProviderProps> = ({
  children,
}) => {
  const [screenInfo, setScreenInfo] = useState<ScreenMetaData | null>(null);
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

  const client = useMemo(() => {
    const axiosInstance = axios.create({ baseURL: API_BASE_URL });
    if (token) {
      axiosInstance.defaults.headers.common[
        "Authorization"
      ] = `Bearer ${token}`;
    } else {
      axiosInstance.defaults.headers.common["Authorization"] = "";
    }
    return axiosInstance;
  }, [token]);

  const fetchAndSetScreenMetaData = useCallback(async () => {
    try {
      const response = await client.get("/meta");
      setScreenInfo(response.data);
    } catch (error) {
      console.error("Failed to fetch screen meta data:", error);
    }
  }, [client]);

  const contextValue = {
    screenInfo,
    setScreenInfo,
    token,
    setToken,
    client,
  };

  // Sync token state with local storage
  useEffect(() => {
    const storedToken = localStorage.getItem(LOCALSTORAGE_KEY_SCREEN_TOKEN);
    if (storedToken !== token) {
      setTokenState(storedToken);
    }

    // Fetch screen meta data
    fetchAndSetScreenMetaData()
  }, [token]);

  return (
    <ScreenInfoContext.Provider value={contextValue}>
      {children}
    </ScreenInfoContext.Provider>
  );
};
