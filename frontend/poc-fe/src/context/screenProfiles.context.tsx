import {
  createContext,
  useCallback,
  useEffect,
  useState,
  ReactNode,
} from "react";
import { ScreenProfile } from "../types/screenProfile.types";
import { useAdminInfoContext } from "../hooks/useAdminInfoContext";

interface ScreenProfilesContextType {
  profiles: ScreenProfile[];
  isLoading: boolean;
  refetch: () => void;
}

export const ScreenProfilesContext = createContext<
  ScreenProfilesContextType | undefined
>(undefined);

export const ScreenProfilesProvider = ({
  children,
}: {
  children: ReactNode;
}) => {
  const [profiles, setProfiles] = useState<ScreenProfile[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const { getAllScreenProfiles } = useAdminInfoContext();

  const fetchScreenProfiles = useCallback(() => {
    getAllScreenProfiles().then((data) => {
      setProfiles(data);
      setIsLoading(false);
    });
  }, [getAllScreenProfiles]);

  const refetch = useCallback(() => {
    getAllScreenProfiles().then((data) => {
      setProfiles(data);
    });
  }, [getAllScreenProfiles]);

  useEffect(() => {
    fetchScreenProfiles();
  }, [fetchScreenProfiles]);

  return (
    <ScreenProfilesContext.Provider value={{ profiles, isLoading, refetch }}>
      {children}
    </ScreenProfilesContext.Provider>
  );
};
