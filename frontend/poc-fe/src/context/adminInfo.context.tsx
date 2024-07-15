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
  CreateScreenProfileDto,
  UpdateScreenProfileDto,
  PairScreenDto,
  ScreenDto,
} from "../types/screenProfile.types";
import { AppEntity, OrderTag } from "../types/crmTypes.types";
import { AdminData, AdminMessageDto } from "../types/adminData.types";
import { encodeAdminData, parseAdminData } from "../util/admin-util";

interface AdminInfoProviderProps {
  children: ReactNode;
}

export interface AdminInfoContextType {
  adminData: AdminData | null;
  setAdminData: (adminData: AdminData | null) => void;
  loginAdmin: (username: string, password: string) => Promise<void>;
  logoutAdmin: () => void;
  isLoggedIn: () => boolean;
  pairScreen: (data: PairScreenDto) => Promise<AxiosResponse>;
  sendMessage: (
    screenProfileId: number,
    data: AdminMessageDto
  ) => Promise<AxiosResponse>;
  removeScreen: (id: number) => Promise<AxiosResponse>;
  createScreenProfile: (
    screenProfileData: CreateScreenProfileDto
  ) => Promise<AxiosResponse>;
  updateScreenProfile: (
    screenProfileId: number,
    screeProfileData: UpdateScreenProfileDto
  ) => Promise<AxiosResponse>;
  deleteScreenProfile: (screenProfileId: number) => Promise<AxiosResponse>;
  getAllScreenProfiles: () => Promise<ScreenProfile[]>;
  fetchEntities: () => Promise<AppEntity>;
  fetchOrderTags: () => Promise<OrderTag[]>;
  fetchConnectedScreens: () => Promise<ScreenDto[]>;
}

export const AdminInfoContext = createContext<AdminInfoContextType>({
  adminData: null,
  setAdminData: () => {},
  loginAdmin: async () => {},
  logoutAdmin: () => {},
  isLoggedIn: () => false,
  pairScreen: async () => {
    return {} as AxiosResponse;
  },
  sendMessage: async () => {
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
  fetchEntities: async () => {
    return {} as AppEntity;
  },
  fetchOrderTags: async () => [],
  fetchConnectedScreens: async () => [],
});

export const AdminInfoProvider: React.FC<AdminInfoProviderProps> = ({
  children,
}) => {
  const [adminData, setAdminDataState] = useState<AdminData | null>(() => {
    // Initialize state from local storage
    return parseAdminData(localStorage.getItem(LOCALSTORAGE_KEY_ADMIN_TOKEN));
  });
  const navigate = useNavigate();

  const setAdminData = useCallback((newAdminData: AdminData | null) => {
    setAdminDataState(newAdminData);
    if (newAdminData !== null) {
      localStorage.setItem(
        LOCALSTORAGE_KEY_ADMIN_TOKEN,
        encodeAdminData(newAdminData)
      );
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
    if (adminData) {
      axiosInstance.defaults.headers.common[
        "Authorization"
      ] = `Bearer ${adminData.token}`;
    } else {
      axiosInstance.defaults.headers.common["Authorization"] = "";
    }
    return axiosInstance;
  }, [adminData]);

  const loginAdmin = useCallback(
    async (username: string, password: string) => {
      try {
        const response = await loggedOutClient({
          method: "post",
          url: "/admin",
          data: { username, password },
        });
        const adminData = response.data;
        setAdminData(adminData);
        navigate("/admin/dashboard");
      } catch (error) {
        console.error("Login failed:", error);
        throw error;
      }
    },
    [loggedOutClient, setAdminData, navigate]
  );

  const logoutAdmin = useCallback(() => {
    setAdminData(null);
  }, []);

  const isLoggedIn = useCallback(() => {
    return !!client.defaults.headers.common["Authorization"];
  }, [client]);

  const pairScreen = useCallback(
    (data: PairScreenDto) => {
      return client({
        method: "post",
        url: "/screens",
        data,
      });
    },
    [client]
  );

  const sendMessage = useCallback(
    (screenProfileId: number, data: AdminMessageDto) => {
      return client({
        method: "post",
        url: `/admin/message-screens/${screenProfileId}`,
        data,
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
    (screeProfileData: CreateScreenProfileDto) => {
      return client({
        method: "post",
        url: "/screen-profiles",
        data: screeProfileData,
      });
    },
    [client]
  );

  const updateScreenProfile = useCallback(
    (id: number, screeProfileData: UpdateScreenProfileDto) => {
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
      // console.log("[DEBUG] response:", response);
      return response.data;
    } catch (error) {
      console.error("Error fetching screen profiles:", error);
      return [];
    }
  }, [client]);

  const fetchEntities = useCallback(async (): Promise<AppEntity> => {
    try {
      // TODO: Implement Correctly
      const response = await client.get("/types/company");
      // console.log("[DEBUG] response:", response);
      return response.data;
    } catch (error) {
      console.error("Error fetching screen profiles:", error);
      return {} as AppEntity;
    }
  }, [client]);

  const fetchOrderTags = useCallback(async (): Promise<OrderTag[]> => {
    try {
      // TODO: Implement Correctly
      const response = await client.get("/types/tags");
      // console.log("[DEBUG] response:", response);
      return response.data;
    } catch (error) {
      console.error("Error fetching screen profiles:", error);
      return [];
    }
  }, [client]);

  const fetchConnectedScreens = useCallback(async (): Promise<ScreenDto[]> => {
    try {
      // TODO: Implement Correctly
      const response = await client.get("/admin/connected-screens");
      // console.log("[DEBUG] response:", response);
      return response.data;
    } catch (error) {
      console.error("Error fetching screen profiles:", error);
      return [];
    }
  }, [loggedOutClient, setAdminData, navigate]);

  const contextValue: AdminInfoContextType = {
    adminData,
    setAdminData,
    loginAdmin,
    logoutAdmin,
    isLoggedIn,
    pairScreen,
    sendMessage,
    removeScreen,
    createScreenProfile,
    updateScreenProfile,
    deleteScreenProfile,
    getAllScreenProfiles,
    fetchEntities,
    fetchOrderTags,
    fetchConnectedScreens,
  };

  // Sync token state with local storage
  useEffect(() => {
    const storedAdminData = parseAdminData(
      localStorage.getItem(LOCALSTORAGE_KEY_ADMIN_TOKEN)
    );
    if (storedAdminData?.token !== adminData?.token) {
      setAdminDataState(storedAdminData);
    }
  }, [adminData]);

  return (
    <AdminInfoContext.Provider value={contextValue}>
      {children}
    </AdminInfoContext.Provider>
  );
};
