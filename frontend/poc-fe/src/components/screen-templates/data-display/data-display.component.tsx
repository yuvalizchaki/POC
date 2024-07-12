import { Box, Divider, Paper, Typography } from "@mui/material";
import { OrderDto } from "../../../types/crmTypes.types";
import { DisplayDataProvider } from "../../../context/displayData.context";
import { useDisplayDataContext as useDisplayDataContext } from "../../../hooks/useDisplayDataContext";
import { useScreenInfoContext } from "../../../hooks/useScreenInfoContext";
import OrdersDisplay from "./orders-display.component";

interface DataDisplayImpProps {
}

const DataDisplayImp = ({ }: DataDisplayImpProps) => {
  const { screenInfo } = useScreenInfoContext();
  const { orders } = useDisplayDataContext();
  console.log('[DEBUG] !! orders: ', orders);
  return (
      <OrdersDisplay orders={orders} />
  );
};

export const DataDisplay = () => {
  return <DisplayDataProvider>
    <DataDisplayImp />
  </DisplayDataProvider>
}