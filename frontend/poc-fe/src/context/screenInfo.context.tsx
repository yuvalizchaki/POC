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
import { AppEntity, OrderTag } from "../types/crmTypes.types";

interface ScreenInfoProviderProps {
  children: ReactNode;
}

export interface ScreenInfoContextType {
  screenInfo: ScreenMetaData | null;
  setScreenInfo: (info: ScreenMetaData | null) => void;
  token: string | null;
  setToken: (token: string | null) => void;
  client: AxiosInstance;
  entities: AppEntity,
  fetchEntitiesData: () => Promise<void>;
  orderTags: OrderTag[],
  fetchOrderTagsData: () => Promise<void>;
  isLoading: boolean;
}

export const ScreenInfoContext = createContext<ScreenInfoContextType>({
  screenInfo: null,
  setScreenInfo: () => { },
  token: null,
  setToken: () => { },
  client: {} as AxiosInstance,
  entities: {} as AppEntity,
  fetchEntitiesData: async () => { },
  orderTags: [],
  fetchOrderTagsData: async () => { },
  isLoading: false,
});

export const ScreenInfoProvider: React.FC<ScreenInfoProviderProps> = ({
  children,
}) => {

  const [entities, setEntities] = useState<AppEntity>({} as AppEntity); // TODO: Set this to empty array once is implemented
  const [orderTags, setOrderTags] = useState<OrderTag[]>([]); // TODO: Set this to empty array once is implemented

  const [isLoadingEntities, setIsLoadingEntities] = useState<boolean>(true);
  const [isLoadingOrderTags, setIsLoadingOrderTags] = useState<boolean>(true);


  const [screenInfo, setScreenInfo] = useState<ScreenMetaData | null>(null);
  const [isLoadingScreenInfo, setIsLoadingScreenInfo] = useState<boolean>(false);

  const isLoading = isLoadingEntities || isLoadingOrderTags || isLoadingScreenInfo;

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
      setIsLoadingScreenInfo(true);
      const response = await client.get("/meta");
      setScreenInfo(response.data);
      setIsLoadingScreenInfo(false);
    } catch (error) {
      setIsLoadingScreenInfo(false);
      console.error("Failed to fetch screen meta data:", error);
    }
  }, [client]);

  const fetchEntitiesData = useCallback(async () => {
    setIsLoadingEntities(true);
    try {
      // TODO: Implement Correctly
      setIsLoadingEntities(true);
      const response = await client.get("/types/company");
      console.log("response:", response);
      setEntities(response.data);
      setIsLoadingEntities(false);
    } catch (error) {
      setIsLoadingEntities(false);
      console.error("Error fetching screen profiles:", error);
    }
  }, [client])

  const fetchOrderTagsData = useCallback(async () => {
    try {
      // TODO: Implement Correctly
      setIsLoadingOrderTags(true);
      const response = await client.get("/types/tags");
      console.log("response:", response);
      setOrderTags(response.data);
      setIsLoadingOrderTags(false);
    } catch (error) {
      setIsLoadingOrderTags(false);
      console.error("Error fetching screen profiles:", error);
    }
  }, [client]);

  useEffect(() => {
    if (token) {
      fetchEntitiesData();
      fetchOrderTagsData();
    }
  }, [token, fetchEntitiesData, fetchOrderTagsData]);

  const contextValue = {
    screenInfo,
    setScreenInfo,
    token,
    setToken,
    client,
    entities,
    fetchEntitiesData,
    orderTags,
    fetchOrderTagsData,
    isLoading
  };

  // Sync token state with local storage
  useEffect(() => {
    const storedToken = localStorage.getItem(LOCALSTORAGE_KEY_SCREEN_TOKEN);
    if (storedToken !== token) {
      setTokenState(storedToken);
    }

    // Fetch screen meta data
    if (token) {
      fetchAndSetScreenMetaData()
    }
  }, [token]);

  return (
    <ScreenInfoContext.Provider value={contextValue}>
      {children}
    </ScreenInfoContext.Provider>
  );
};
