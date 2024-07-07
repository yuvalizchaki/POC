import {
  createContext,
  useCallback,
  useEffect,
  useState,
  ReactNode,
} from "react";
import { ScreenProfile } from "../types/screenProfile.types";
import { useAdminInfoContext } from "../hooks/useAdminInfoContext";
import { AppEntity, OrderTag } from "../types/crmTypes.types";

interface ScreenProfilesContextType {
  profiles: ScreenProfile[];
  entities: AppEntity;
  orderTags: OrderTag[];
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
  const [entities, setEntities] = useState<AppEntity>({
    Id: 1,
    CompanyId: 1,
    Name: "ראשי",
    Description: "General company department",
    IsOwnInventory: false,
    Childs: [
      {
        Id: 4,
        CompanyId: 1,
        Name: "חיפה",
        Description: "",
        IsOwnInventory: true,
        Childs: []
      },
      {
        Id: 5,
        CompanyId: 1,
        Name: "קסריה",
        Description: "",
        IsOwnInventory: true,
        Childs: [
          {
            Id: 8,
            CompanyId: 1,
            Name: "נוי וקישוט",
            Description: "",
            IsOwnInventory: true,
            Childs: []
          }
        ]
      },
      {
        Id: 14,
        CompanyId: 1,
        Name: "ירושלים",
        Description: "",
        IsOwnInventory: true,
        Childs: []
      },
      {
        Id: 15,
        CompanyId: 1,
        Name: "NEW YORK",
        Description: "",
        IsOwnInventory: true,
        Childs: []
      }
    ]
  }); // TODO: Set this to empty array once is implemented
  const [orderTags, setOrderTags] = useState<OrderTag[]>([]); // TODO: Set this to empty array once is implemented
  const [isLoadingProfiles, setIsLoadingProfiles] = useState<boolean>(true);
  const [isLoadingEntities, setIsLoadingEntities] = useState<boolean>(true);
  const [isLoadingOrderTags, setIsLoadingOrderTags] = useState<boolean>(true);

  const { getAllScreenProfiles, fetchEntities, fetchOrderTags } = useAdminInfoContext();

  const fetchScreenProfiles = useCallback(() => {
    setIsLoadingProfiles(true);
    getAllScreenProfiles().then((data) => {
      setProfiles(data);
      setIsLoadingProfiles(false);
    });
  }, [getAllScreenProfiles]);

  const fetchEntitiesData = useCallback(() => {
    setIsLoadingEntities(true);
    fetchEntities().then((data) => {
      setEntities(data);
      setIsLoadingEntities(false);
    }).catch(() => {
      setIsLoadingEntities(false);
      console.error("Failed to fetch entities");
    });
  }, [fetchEntities]);

  const fetchOrderTagsData = useCallback(() => {
    setIsLoadingOrderTags(true);
    fetchOrderTags().then((data) => {
      setOrderTags(data);
      setIsLoadingOrderTags(false);
    }).catch(() => {
      setIsLoadingOrderTags(false);
      console.error("Failed to fetch order tags");
    });
  }, [fetchOrderTags]);

  const refetch = useCallback(() => {
    getAllScreenProfiles().then((data) => {
      setProfiles(data);
    });
  }, [getAllScreenProfiles]);

  useEffect(() => {
    fetchScreenProfiles();
    fetchEntitiesData();
    fetchOrderTagsData();
  }, [fetchScreenProfiles, fetchEntitiesData, fetchOrderTagsData]);

  const isLoading = isLoadingProfiles || isLoadingEntities || isLoadingOrderTags;

  return (
    <ScreenProfilesContext.Provider value={{ profiles, entities, orderTags, isLoading, refetch }}>
      {children}
    </ScreenProfilesContext.Provider>
  );
};