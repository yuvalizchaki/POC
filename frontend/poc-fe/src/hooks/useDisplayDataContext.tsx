import { useContext } from "react";
import { OrdersDataContext, DisplayDataContextType as DisplayDataContextType } from "../context/displayData.context";

export const useDisplayDataContext = (): DisplayDataContextType => {
    const context = useContext(OrdersDataContext);

    if (context === undefined) {
        throw new Error("useDisplayDataContext must be used within an DisplayDataProvider");
    }

    return context;
};
