import { OrderDto } from "../../../types/crmTypes.types";
import { useCallback, useState } from "react";
import { useSignalR } from "../../../hooks/useSignalR";
import { API_BASE_URL, API_SCREEN_HUB_URL } from "../../../config";
import { ScreenInfo } from "../../../types/screenInfo.types";
import { Box, Divider, Paper, Typography } from "@mui/material";

interface OrdersScreenProps {
  screenInfo: ScreenInfo;
}

export const OrdersScreen = ({ screenInfo }: OrdersScreenProps) => {
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
    },
  });
  return (
    <>
      <Paper variant="outlined" sx={{ p: 2, overflow: "auto" }}>
        <Typography variant="h4" gutterBottom>
          Screen Information
        </Typography>
        <Box
          display="flex"
          justifyContent="space-between"
          alignItems="center"
          mb={2}
        >
          <Typography variant="h6">Screen ID: {screenInfo.id}</Typography>
          <Typography variant="h6">
            Screen Profile ID: {screenInfo.screenProfileId}
          </Typography>
        </Box>
        <Divider sx={{ my: 2 }} />
        <Typography variant="h5" gutterBottom>
          Orders:
        </Typography>
        {orders.length > 0 ? (
          <Box
            display="flex"
            flexWrap="wrap"
            justifyContent="flex-start"
            alignItems="start"
            gap={2}
          >
            {orders.map((order: OrderDto) => (
              <Box key={order.id} sx={{ minWidth: 300 }}>
                <Paper variant="outlined" sx={{ p: 2, height: "100%" }}>
                  <Typography variant="subtitle1">
                    Order ID: {order.id}
                  </Typography>
                  <Typography variant="subtitle1">
                    Customer ID: {order.customerId}
                  </Typography>
                  {order.clientName && (
                    <Typography variant="subtitle1">
                      Client Name: {order.clientName}
                    </Typography>
                  )}
                </Paper>
              </Box>
            ))}
          </Box>
        ) : (
          <Typography>No Orders</Typography>
        )}
      </Paper>
    </>
  );
};
