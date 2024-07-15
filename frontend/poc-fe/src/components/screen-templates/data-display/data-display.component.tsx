import { useEffect, useState } from "react";
import { OrderTransportType } from "../../../types/crmTypes.types";
import { DisplayDataProvider } from "../../../context/displayData.context";
import { useDisplayDataContext as useDisplayDataContext } from "../../../hooks/useDisplayDataContext";
import { useScreenInfoContext } from "../../../hooks/useScreenInfoContext";
import OrdersDisplay from "./orders-display.component";
import InventoryDisplay from "./inventory-display.component";
import { DisplayTemplateType } from "../../../types/screenProfile.types";
import { useMemo } from "react";

interface DataDisplayImpProps {}

const DataDisplayImp = ({}: DataDisplayImpProps) => {
  const { screenInfo } = useScreenInfoContext();
  const { orders, inventoryItems } = useDisplayDataContext();

  const fixedOrders = useMemo(() => {
    const sortedOrders = [...orders].sort(
      (a, b) =>
        new Date(
          a.transportType === OrderTransportType.Incoming
            ? a.crmOrder.endDate
            : a.crmOrder.endDate
        ).getTime() -
        new Date(
          b.transportType === OrderTransportType.Incoming
            ? b.crmOrder.endDate
            : b.crmOrder.endDate
        ).getTime()
    );

    return sortedOrders;
  }, [orders]);

  // Paging logic ===================================================
  const [currentPage, setCurrentPage] = useState(0);
  const PAGE_SIZE = 5;

  useEffect(() => {
    if (screenInfo?.displayConfig.isPaging) {
      const intervalId = setInterval(() => {
        setCurrentPage(
          (prevPage) =>
            (prevPage + 1) %
            Math.ceil(
              Math.max(fixedOrders.length, inventoryItems.length) / PAGE_SIZE
            )
        );
      }, screenInfo?.displayConfig.pagingRefreshTime || 15000); // default to 10 seconds if not specified

      return () => clearInterval(intervalId);
    }
  }, [
    screenInfo?.displayConfig.isPaging,
    screenInfo?.displayConfig.pagingRefreshTime,
    fixedOrders.length,
    inventoryItems.length,
  ]);

  return screenInfo?.displayConfig.displayTemplate ===
    DisplayTemplateType.Inventory ? (
    <InventoryDisplay
      inventoryItems={inventoryItems}
      isPaging={screenInfo?.displayConfig.isPaging ?? false}
      currentPage={currentPage}
      pageSize={PAGE_SIZE}
    />
  ) : (
    <OrdersDisplay
      orders={fixedOrders}
      isPaging={screenInfo?.displayConfig.isPaging ?? false}
      currentPage={currentPage}
      pageSize={PAGE_SIZE}
    />
  );
};

export const DataDisplay = () => {
  return (
    <DisplayDataProvider>
      <DataDisplayImp />
    </DisplayDataProvider>
  );
};
