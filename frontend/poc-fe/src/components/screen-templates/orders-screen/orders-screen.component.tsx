import { ScreenAddedDto } from "../../../types/signalR.types";
import { OrderDto } from "../../../types/crmTypes.types";
import { useCallback, useState } from "react";
import { useSignalR } from "../../../hooks/useSignalR";
import { API_BASE_URL, API_SCREEN_HUB_URL } from "../../../config";

interface OrdersScreenProps {
  screenInfo: ScreenAddedDto;
}

export const OrdersScreen = ({ screenInfo }: OrdersScreenProps) => {
  const [orders, setOrders] = useState<OrderDto[]>([]);

  const fetchOrders = useCallback(async () => {
    try {
        console.log('[DEBUG] sending: ', `${API_BASE_URL}/orders`);
      const response = await fetch(`${API_BASE_URL}/orders`);
      console.log('[DEBUG] get orders response: ', response);
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
        console.log('[DEBUG] connected to screen hub')
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
    },
  });
  return (
    <>
      <div>
        <p>Screen ID: {screenInfo.id}</p>
        <p>Screen Profile ID: {screenInfo.screenProfileId}</p>
      </div>
      <div>
        <h3>Orders: </h3>
        {orders.length > 0 
        ? orders.map((order: OrderDto) => (
          <p key={order.id}>
            Order ID: {order.id}, Customer ID: {order.customerId}
            {order.clientName && `, Client Name: ${order.clientName}`}
          </p>
        ))
    : '[No Orders]'}
      </div>
    </>
  );
};
