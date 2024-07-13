import { Box, Divider, Paper, Typography } from "@mui/material";
import { OrderDto } from "../../../types/crmTypes.types";
import { DisplayDataProvider } from "../../../context/displayData.context";
import { useDisplayDataContext as useDisplayDataContext } from "../../../hooks/useDisplayDataContext";
import { useScreenInfoContext } from "../../../hooks/useScreenInfoContext";
import OrdersDisplay from "./orders-display.component";
import InventoryDisplay from "./inventory-display.component";
import { DisplayTemplateType } from "../../../types/screenProfile.types";

interface DataDisplayImpProps {
}

const DataDisplayImp = ({ }: DataDisplayImpProps) => {
  const { screenInfo } = useScreenInfoContext();
  const { orders, inventoryItems } = useDisplayDataContext();
  
  return (screenInfo?.displayConfig.displayTemplate === DisplayTemplateType.Inventory
    ? <InventoryDisplay inventoryItems={inventoryItems} />
    : <OrdersDisplay orders={orders} />
  );
};

export const DataDisplay = () => {
  return <DisplayDataProvider>
    <DataDisplayImp />
  </DisplayDataProvider>
}