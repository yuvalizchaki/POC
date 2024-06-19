// adminInfo.context.tsx
import React, {
  createContext,
  ReactNode,
  useCallback,
  useState,
  useContext,
  useMemo,
} from "react";
import axios, { AxiosResponse } from "axios";
import { useNavigate } from "react-router-dom";
import { API_BASE_URL } from "../config";
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
  const [token, setToken] = useState<string | null>(null);
  const navigate = useNavigate();

  const loggedOutClient = useMemo(
    () => axios.create({ baseURL: API_BASE_URL }),
    []
  );
  const adminClient = useMemo(() => {
    const client = axios.create({ baseURL: API_BASE_URL });
    if (token) {
      client.defaults.headers.common["Authorization"] = `Bearer ${token}`;
    }
    return client;
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
        adminClient.defaults.headers.common[
          "Authorization"
        ] = `Bearer ${token}`;
        navigate("/admin/dashboard");
      } catch (error) {
        console.error("Login failed:", error);
        throw error;
      }
    },
    [loggedOutClient, adminClient, navigate]
  );

  const logoutAdmin = useCallback(() => {
    setToken(null);
    adminClient.defaults.headers.common["Authorization"] = "";
  }, [adminClient]);

  const isLoggedIn = useCallback(() => {
    return !!adminClient.defaults.headers.common["Authorization"];
  }, [adminClient]);

  const pairScreen = useCallback(
    (code: string, screenProfileId: number) => {
      return adminClient({
        method: "post",
        url: "/screens",
        data: {
          pairingCode: code,
          screenProfileId: screenProfileId,
        },
      });
    },
    [adminClient]
  );

  const removeScreen = useCallback(
    (id: number) => {
      return adminClient({
        method: "delete",
        url: "/screens/" + id,
      });
    },
    [adminClient]
  );

  const createScreenProfile = useCallback(
    (screeProfileData: ScreenProfileAddData) => {
      return adminClient({
        method: "post",
        url: "/screen-profiles",
        data: screeProfileData,
      });
    },
    [adminClient]
  );

  const updateScreenProfile = useCallback(
    (id: number, screeProfileData: ScreeProfileUpdateData) => {
      return adminClient({
        method: "put",
        url: "/screen-profiles/" + id,
        data: screeProfileData,
      });
    },
    [adminClient]
  );

  const deleteScreenProfile = useCallback(
    (id: number) => {
      return adminClient({
        method: "delete",
        url: "/screen-profiles/" + id,
      });
    },
    [adminClient]
  );

  const getAllScreenProfiles = useCallback(async (): Promise<
    ScreenProfile[]
  > => {
    try {
      const response = await adminClient.get("/screen-profiles");
      console.log("response:", response);
      return response.data;
    } catch (error) {
      console.error("Error fetching screen profiles:", error);
      return [];
    }
  }, [adminClient]);

  const contextValue: AdminInfoContextType = {
    token,
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

  return (
    <AdminInfoContext.Provider value={contextValue}>
      {children}
    </AdminInfoContext.Provider>
  );
};

export const useAdminInfo = () => useContext(AdminInfoContext);
