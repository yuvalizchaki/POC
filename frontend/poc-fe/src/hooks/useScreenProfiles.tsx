import { useCallback, useEffect, useState } from "react";
import { ScreenProfile } from "../types/screenProfile.types";
import { useAdminInfo } from "./useAdminInfo";

export const useScreenProfiles = () => {
  const [profiles, setProfiles] = useState<ScreenProfile[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const { getAllScreenProfiles } = useAdminInfo();

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

  return { profiles, isLoading, refetch };
};
