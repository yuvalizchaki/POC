import React, { createContext, ReactNode, useCallback, useMemo, useState } from "react";
import { OrderDto } from "../types/crmTypes.types";
import { API_SCREEN_HUB_URL } from "../config";
import { useScreenInfoContext } from "../hooks/useScreenInfoContext";
import { useSignalR } from "../hooks/useSignalR";
import { useNavigate } from "react-router-dom";

interface OrdersDataProviderProps {
    children: ReactNode;
}

export interface OrdersDataContextType {
    orders: OrderDto[];
    fetchAndSetOrders: () => Promise<void>;
}

export const OrdersDataContext = createContext<OrdersDataContextType>({
    orders: [],
    fetchAndSetOrders: async () => { },
});

export const OrdersDataProvider: React.FC<OrdersDataProviderProps> = ({ children }) => {
    const { setScreenInfo, client, token } = useScreenInfoContext();
    const [orders, setOrders] = useState<OrderDto[]>([]);
    const navigate = useNavigate();

    const fetchAndSetOrders = useCallback(async () => {
        try {
            const response = await client.get("/orders");
            setOrders(response.data);
        } catch (error) {
            console.error("Failed to fetch orders:", error);
        }
    }, [client]);

    const redirectToGuest = useCallback(() => {
        navigate("/screen/pair", { replace: true })
    }, [navigate])

    useSignalR({
        connectParams: {
            hubUrl: API_SCREEN_HUB_URL,
            token: `${token}`,
            onConnect: () => {
                console.log("[DEBUG] connected to screen hub");
                fetchAndSetOrders();
            },
            onConnectError: () => {
                redirectToGuest();
            },
        },
        commandHandlers: {
            orderAdded: (orderAddedDto) => {
                setOrders((prevOrders) => {
                    if (prevOrders.some((order) => order.id === orderAddedDto.id)) {
                        console.warn("Attempted to add an existing order. Fetching latest orders.");
                        fetchAndSetOrders();
                        return prevOrders;
                    }
                    return [...prevOrders, orderAddedDto];
                });
            },
            orderUpdated: (orderUpdatedDto) => {
                setOrders((prevOrders) => {
                    const orderIndex = prevOrders.findIndex((order) => order.id === orderUpdatedDto.id);
                    if (orderIndex === -1) {
                        console.warn("Attempted to update a non-existing order. Fetching latest orders.");
                        fetchAndSetOrders();
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
                        console.warn("Attempted to delete a non-existing order. Fetching latest orders.");
                        fetchAndSetOrders();
                        return prevOrders;
                    }
                    return prevOrders.filter((order) => order.id !== orderDeletedDto);
                });
            },
            screenRemoved: () => {
                console.log("[DEBUG] Disconnected");
                setScreenInfo(null);
            },
        },
    });

    const contextValue = useMemo(
        () => ({ orders, fetchAndSetOrders }),
        [orders, fetchAndSetOrders]
    );

    return (
        <OrdersDataContext.Provider value={contextValue}>
            {children}
        </OrdersDataContext.Provider>
    );
};
