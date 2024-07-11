import { useContext } from "react";
import { OrdersDataContext, OrdersDataContextType } from "../context/ordersData.context";

export const useOrdersDataContext = (): OrdersDataContextType => {
    const context = useContext(OrdersDataContext);

    if (context === undefined) {
        throw new Error("useOrdersData must be used within an OrdersDataProvider");
    }

    return context;
};
