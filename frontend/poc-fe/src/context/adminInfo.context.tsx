// adminInfo.context.tsx
import React, {
  createContext,
  ReactNode,
  useCallback,
  useState,
  useMemo,
  useEffect,
} from "react";
import axios, { AxiosResponse } from "axios";
import { useNavigate } from "react-router-dom";
import { API_BASE_URL, LOCALSTORAGE_KEY_ADMIN_TOKEN } from "../config";
import {
  ScreenProfile,
  ScreenProfileAddData,
  ScreeProfileUpdateData,
} from "../types/screenProfile.types";

interface AdminInfoProviderProps {
  children: ReactNode;
}

export interface AdminInfoContextType {
  token: string | null;
  setToken: (token: string | null) => void;
  loginAdmin: (username: string, password: string) => Promise<void>;
  logoutAdmin: () => void;
  isLoggedIn: () => boolean;
  pairScreen: (code: string, screenProfileId: number) => Promise<AxiosResponse>;
  removeScreen: (id: number) => Promise<AxiosResponse>;
  createScreenProfile: (
    screenProfileData: ScreenProfileAddData
  ) => Promise<AxiosResponse>;
  updateScreenProfile: (
    screenProfileId: number,
    screeProfileData: ScreeProfileUpdateData
  ) => Promise<AxiosResponse>;
  deleteScreenProfile: (screenProfileId: number) => Promise<AxiosResponse>;
  getAllScreenProfiles: () => Promise<ScreenProfile[]>;
}

export const AdminInfoContext = createContext<AdminInfoContextType>({
  token: null,
  setToken: () => {},
  loginAdmin: async () => {},
  logoutAdmin: () => {},
  isLoggedIn: () => false,
  pairScreen: async () => {
    return {} as AxiosResponse;
  },
  removeScreen: async () => {
    return {} as AxiosResponse;
  },
  createScreenProfile: async () => {
    return {} as AxiosResponse;
  },
  updateScreenProfile: async () => {
    return {} as AxiosResponse;
  },
  deleteScreenProfile: async () => {
    return {} as AxiosResponse;
  },
  getAllScreenProfiles: async () => [],
});

export const AdminInfoProvider: React.FC<AdminInfoProviderProps> = ({
  children,
}) => {
  const [token, setTokenState] = useState<string | null>(() => {
    // Initialize state from local storage
    return localStorage.getItem(LOCALSTORAGE_KEY_ADMIN_TOKEN);
  });
  const navigate = useNavigate();

  const setToken = useCallback((newToken: string | null) => {
    setTokenState(newToken);
    if (newToken !== null) {
      localStorage.setItem(LOCALSTORAGE_KEY_ADMIN_TOKEN, newToken);
    } else {
      localStorage.removeItem(LOCALSTORAGE_KEY_ADMIN_TOKEN);
    }
  }, []);

  const loggedOutClient = useMemo(
    () => axios.create({ baseURL: API_BASE_URL }),
    []
  );

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

  const loginAdmin = useCallback(
    async (username: string, password: string) => {
      try {
        const response = await loggedOutClient({
          method: "post",
          url: "/admin",
          data: { username, password },
        });
        const token = response.data;
        setToken(token);
        navigate("/admin/dashboard");
      } catch (error) {
        console.error("Login failed:", error);
        throw error;
      }
    },
    [loggedOutClient, setToken, navigate]
  );

  const logoutAdmin = useCallback(() => {
    setToken(null);
  }, []);

  const isLoggedIn = useCallback(() => {
    return !!client.defaults.headers.common["Authorization"];
  }, [client]);

  const pairScreen = useCallback(
    (code: string, screenProfileId: number) => {
      return client({
        method: "post",
        url: "/screens",
        data: {
          pairingCode: code,
          screenProfileId: screenProfileId,
        },
      });
    },
    [client]
  );

  const removeScreen = useCallback(
    (id: number) => {
      return client({
        method: "delete",
        url: "/screens/" + id,
      });
    },
    [client]
  );

  const createScreenProfile = useCallback(
    (screeProfileData: ScreenProfileAddData) => {
      return client({
        method: "post",
        url: "/screen-profiles",
        data: screeProfileData,
      });
    },
    [client]
  );

  const updateScreenProfile = useCallback(
    (id: number, screeProfileData: ScreeProfileUpdateData) => {
      return client({
        method: "put",
        url: "/screen-profiles/" + id,
        data: screeProfileData,
      });
    },
    [client]
  );

  const deleteScreenProfile = useCallback(
    (id: number) => {
      return client({
        method: "delete",
        url: "/screen-profiles/" + id,
      });
    },
    [client]
  );

  const getAllScreenProfiles = useCallback(async (): Promise<
    ScreenProfile[]
  > => {
    try {
      const response = await client.get("/screen-profiles");
      console.log("response:", response);
      return response.data;
    } catch (error) {
      console.error("Error fetching screen profiles:", error);
      return [];
    }
  }, [client]);

  const contextValue: AdminInfoContextType = {
    token,
    setToken,
    loginAdmin,
    logoutAdmin,
    isLoggedIn,
    pairScreen,
    removeScreen,
    createScreenProfile,
    updateScreenProfile,
    deleteScreenProfile,
    getAllScreenProfiles,
  };

  // Sync token state with local storage
  useEffect(() => {
    const storedToken = localStorage.getItem(LOCALSTORAGE_KEY_ADMIN_TOKEN);
    if (storedToken !== token) {
      setTokenState(storedToken);
    }
  }, [token]);

  return (
    <AdminInfoContext.Provider value={contextValue}>
      {children}
    </AdminInfoContext.Provider>
  );
};
