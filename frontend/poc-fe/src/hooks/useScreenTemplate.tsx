import { useCallback, useState } from "react";
import { ScreenInfo } from "../types/screenInfo.types";
import { useScreenInfo } from "./useScreenInfo";
import { OrderDto } from "../types/crmTypes.types";
import { API_BASE_URL, API_SCREEN_HUB_URL } from "../config";
import { useSignalR } from "./useSignalR";

interface ScreenTemplateProps {
  screenInfo: ScreenInfo;
}

export const useScreenTemplate = ({ screenInfo }: ScreenTemplateProps) => {
  const { setScreenInfo } = useScreenInfo();

  const [orders, setOrders] = useState<OrderDto[]>([]);

  const fetchOrders = useCallback(async () => {
    try {
      console.log("[DEBUG] sending: ", `${API_BASE_URL}/orders`);
      const response = await fetch(`${API_BASE_URL}/orders`);
      console.log("[DEBUG] get orders response: ", response);
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      const data = await response.json();
      setOrders(data);
    } catch (error) {
      console.error("Failed to fetch orders:", error);
    }
  }, []);

  useSignalR({
    hubUrl: API_SCREEN_HUB_URL,
    onConnect: () => {
      console.log("[DEBUG] connected to screen hub");
      fetchOrders();
    },
    commandHandlers: {
      orderAdded: (orderAddedDto) => {
        setOrders((prevOrders) => {
          if (prevOrders.some((order) => order.id === orderAddedDto.id)) {
            console.warn(
              "Attempted to add an existing order. Fetching latest orders."
            );
            fetchOrders();
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
            fetchOrders();
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
            fetchOrders();
            return prevOrders;
          }
          return prevOrders.filter((order) => order.id !== orderDeletedDto);
        });
      },
      screenRemoved: (/* screenRemovedDto */) => {
        // TODO: Check this logic
        console.log("[DEBUG] Disconnected");
        setScreenInfo(null);
      },
    },
  });

  return { orders, screenInfo };
};
