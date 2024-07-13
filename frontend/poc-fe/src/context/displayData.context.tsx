import React, { createContext, ReactNode, useCallback, useState } from "react";
import { InventoryItem, OrderDto } from "../types/crmTypes.types";
import { API_SCREEN_HUB_URL } from "../config";
import { useScreenInfoContext } from "../hooks/useScreenInfoContext";
import { useSignalR } from "../hooks/useSignalR";
import { useNavigate } from "react-router-dom";
import { DisplayTemplateType } from "../types/screenProfile.types";

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
                    console.log('Refetching Data');
                    refetchData();
                },
                orderAdded: (orderAddedDtos) => {
                    setOrders((prevOrders) => {
                        let shouldFetchLatest = false;

                        orderAddedDtos.forEach((orderAddedDto) => {
                            if (prevOrders.some((order) =>
                                order.crmOrder.id === orderAddedDto.crmOrder.id
                                && order.transportType === orderAddedDto.transportType)
                            ) {
                                console.warn("Attempted to add an existing order. Fetching latest orders.");
                                shouldFetchLatest = true;
                            }
                        });

                        if (shouldFetchLatest) {
                            fetchAndSetOrders();
                            return prevOrders;
                        }

                        return [...prevOrders, ...orderAddedDtos];
                    });

                },
                orderDeleted: (orderDeletedDtos) => {
                    setOrders((prevOrders) => {
                        let shouldFetchLatest = false;

                        orderDeletedDtos.forEach((orderDeletedDto) => {
                            if (!prevOrders.some((order) => order.crmOrder.id === orderDeletedDto)) {
                                console.warn("Attempted to delete a non-existing order. Fetching latest orders.");
                                shouldFetchLatest = true;
                            }
                        });

                        if (shouldFetchLatest) {
                            fetchAndSetOrders();
                            return prevOrders;
                        }

                        return prevOrders.filter((order) => !orderDeletedDtos.includes(order.crmOrder.id));
                    });
                },
                orderUpdated: (orderUpdatedDtos) => {
                    setOrders((prevOrders) => {
                        const updatedOrders = [...prevOrders];
                        let shouldFetchLatest = false;

                        orderUpdatedDtos.forEach((orderUpdatedDto) => {
                            const orderIndex = prevOrders.findIndex((order) =>
                                order.crmOrder.id === orderUpdatedDto.crmOrder.id
                                && order.transportType === orderUpdatedDto.transportType
                            );
                            if (orderIndex === -1) {
                                console.warn("Attempted to update a non-existing order. Fetching latest orders.");
                                shouldFetchLatest = true;
                            } else {
                                updatedOrders[orderIndex] = orderUpdatedDto;
                            }
                        });

                        if (shouldFetchLatest) {
                            fetchAndSetOrders();
                            return prevOrders;
                        }

                        return updatedOrders;
                    });

                },
                profileUpdated: () => {
                    console.log('Profile Updated, refetching data');
                    refetchData();
                },
                inventoryUpdated: () => {
                    console.log('Inventory Updated, refetching data');
                    refetchData();
                },
                screenRemoved: () => {
                    console.log("Screen Removed by Admin");
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
