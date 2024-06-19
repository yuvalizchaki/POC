import { useCallback, useState } from "react";
import { ScreenInfo } from "../types/screenInfo.types";
import { OrderDto } from "../types/crmTypes.types";
import { useScreenInfo } from "./useScreenInfo";
import { API_SCREEN_HUB_URL } from "../config";
import { useSignalR } from "./useSignalR";

interface ScreenTemplateProps {
  screenInfo: ScreenInfo;
}

export const useScreenTemplate = ({ screenInfo }: ScreenTemplateProps) => {
  const { setScreenInfo, fetchOrders } = useScreenInfo();

  const [orders, setOrders] = useState<OrderDto[]>([]);

  const fetchAndSetOrders = useCallback(async () => {
    try {
      const response = await fetchOrders();
      setOrders(response.data);
    } catch (error) {
      console.error("Failed to fetch orders:", error);
    }
  }, [fetchOrders]);

  useSignalR({
    hubUrl: API_SCREEN_HUB_URL,
    onConnect: () => {
      console.log("[DEBUG] connected to screen hub");
      fetchAndSetOrders();
    },
    commandHandlers: {
      orderAdded: (orderAddedDto) => {
        setOrders((prevOrders) => {
          if (prevOrders.some((order) => order.id === orderAddedDto.id)) {
            console.warn(
              "Attempted to add an existing order. Fetching latest orders."
            );
            fetchAndSetOrders();
            return prevOrders;
          }
          return [...prevOrders, orderAddedDto];
        });
      },
      orderUpdated: (orderUpdatedDto) => {
        setOrders((prevOrders) => {
          const orderIndex = prevOrders.findIndex(
            (order) => order.id === orderUpdatedDto.id
          );
          if (orderIndex === -1) {
            console.warn(
              "Attempted to update a non-existing order. Fetching latest orders."
            );
            fetchAndSetOrders();
            return prevOrders;
          }
          const updatedOrders = [...prevOrders];
          updatedOrders[orderIndex] = orderUpdatedDto;
          return updatedOrders;
        });
      },
      orderDeleted: (orderDeletedDto) => {
        setOrders((prevOrders) => {
          if (!prevOrders.some((order) => order.id === orderDeletedDto)) {
            console.warn(
              "Attempted to delete a non-existing order. Fetching latest orders."
            );
            fetchAndSetOrders();
            return prevOrders;
          }
          return prevOrders.filter((order) => order.id !== orderDeletedDto);
        });
      },
      screenRemoved: (/* screenRemovedDto */) => {
        console.log("[DEBUG] Disconnected");
        setScreenInfo(null);
      },
    },
  });

  return { orders, screenInfo };
};
