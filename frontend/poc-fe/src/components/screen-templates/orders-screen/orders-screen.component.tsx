import { Box, Divider, Paper, Typography } from "@mui/material";
import { OrderDto } from "../../../types/crmTypes.types";
import { OrdersDataProvider } from "../../../context/ordersData.context";
import { useOrdersData } from "../../../hooks/useOrdersData";
import { useScreenInfoContext } from "../../../hooks/useScreenInfoContext";

interface OrdersScreenProps {
}

export const OrdersScreen = ({ }: OrdersScreenProps) => {
  const { screenInfo } = useScreenInfoContext();
  const { orders } = useOrdersData();
  console.log('[DEBUG] screenInfo: ', screenInfo);
  return (
    <OrdersDataProvider>
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
          <Typography variant="h6">Screen ID: {screenInfo?.id}</Typography>
          <Typography variant="h6">
            Screen Profile ID: {screenInfo?.screenProfileId}
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
    </OrdersDataProvider>
  );
};
