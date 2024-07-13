import React, { createContext, ReactNode, useCallback, useState } from "react";
import { InventoryItem, OrderDto } from "../types/crmTypes.types";
import { API_SCREEN_HUB_URL } from "../config";
import { useScreenInfoContext } from "../hooks/useScreenInfoContext";
import { useSignalR } from "../hooks/useSignalR";
import { useNavigate } from "react-router-dom";
import { DisplayTemplateType } from "../types/screenProfile.types";
import moment from "moment";

interface DisplayDataProviderProps {
    children: ReactNode;
}

export interface DisplayDataContextType {
    orders: OrderDto[];
    fetchAndSetOrders: () => Promise<void>;
    inventoryItems: InventoryItem[];
    fetchAndSetInventoryItems: () => Promise<void>;
}

export const OrdersDataContext = createContext<DisplayDataContextType>({
    orders: [],
    inventoryItems: [],
    fetchAndSetOrders: async () => { },
    fetchAndSetInventoryItems: async () => { }
});

export const DisplayDataProvider: React.FC<DisplayDataProviderProps> = ({ children }) => {
    const { screenInfo, setScreenInfo, client, token, setToken } = useScreenInfoContext();
    const [orders, setOrders] = useState<OrderDto[]>([]);
    const [inventoryItems, setInventoryItems] = useState<InventoryItem[]>([]);
    const navigate = useNavigate();

    const fetchAndSetOrders = useCallback(async () => {
        try {
            const response = await client.get("/orders");
            setOrders(response.data);
        } catch (error) {
            console.error("Failed to fetch orders:", error);
        }
    }, [client]);


    const fetchAndSetInventoryItems = useCallback(async () => {
        try {
            const response = await client.get("/inventory-items");
            setInventoryItems(response.data);
        } catch (error) {
            console.error("Failed to fetch inventory items:", error);
        }
    }, [client]);

    const redirectToGuest = useCallback(() => {
        navigate("/screen/pair", { replace: true })
    }, [navigate])

    const refetchData = useCallback(() => {
        if (screenInfo?.displayConfig.displayTemplate === DisplayTemplateType.Inventory) {
            fetchAndSetInventoryItems();
        } else {
            fetchAndSetOrders();
        }
    }, [fetchAndSetOrders, fetchAndSetInventoryItems]);

    useSignalR({
        connectParams: {
            hubUrl: API_SCREEN_HUB_URL,
            token: `${token}`,
            onConnect: () => {
                fetchAndSetOrders();
            },
            onConnectError: () => {
                redirectToGuest();
            },
            commandHandlers: {
                refreshData: () => {
                    console.log(`${moment()} Refetching Data`);
                    refetchData();
                },
                profileUpdated: () => {
                    console.log(`${moment()} Profile Updated, refetching data`);
                    refetchData();
                },
                screenRemoved: () => {
                    console.log(`${moment()}  Screen Removed by Admin`);
                    setToken(null);
                    setScreenInfo(null);
                },
            },
        },
    });

    return (
        <OrdersDataContext.Provider value={{ orders, fetchAndSetOrders, inventoryItems, fetchAndSetInventoryItems }}>
            {children}
        </OrdersDataContext.Provider>
    );
};
