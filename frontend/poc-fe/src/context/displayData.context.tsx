import React, { createContext, ReactNode, useCallback, useEffect, useRef, useState } from "react";
import { InventoryItem, OrderDto } from "../types/crmTypes.types";
import { API_SCREEN_HUB_URL } from "../config";
import { useScreenInfoContext } from "../hooks/useScreenInfoContext";
import { useSignalR } from "../hooks/useSignalR";
import { useNavigate } from "react-router-dom";
import { DisplayTemplateType } from "../types/screenProfile.types";
import moment from "moment";
import { ScreenMetaData } from "../types/screenMetaData.types";

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
    const { screenInfo, setScreenInfo, client, token, setToken, fetchAndSetScreenMetaData } = useScreenInfoContext();
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

    /** Closure error fix */
    const screenInfoWrapped = useRef<ScreenMetaData | null>(screenInfo);
    useEffect(() => {
        screenInfoWrapped.current = screenInfo
    }, [screenInfo]);

    const refetchData = useCallback((newMetaDate?: ScreenMetaData) => {
        /** This is to solve a bug where the refetchData closure is changed but binded to the previous one. using the latest meta data fixes it */
        const metaData = newMetaDate ?? screenInfoWrapped.current
        if (metaData?.displayConfig.displayTemplate === DisplayTemplateType.Inventory) {
            fetchAndSetInventoryItems();
        } else {
            fetchAndSetOrders();
        }
    }, [screenInfoWrapped, fetchAndSetOrders, fetchAndSetInventoryItems]);

    useSignalR({
        connectParams: {
            hubUrl: API_SCREEN_HUB_URL,
            token: `${token}`,
            onConnect: () => {
                refetchData();
            },
            onConnectError: () => {
                redirectToGuest();
            },
            commandHandlers: {
                refreshData: () => {
                    console.log(`${moment()} Refetching Data`);
                    refetchData();
                },
                profileUpdated: async () => {
                    console.log(`${moment()} Profile Updated, refetching data`);
                    const newMetaDate = await fetchAndSetScreenMetaData();
                    refetchData(newMetaDate);
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
