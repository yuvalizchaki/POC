import { OrderDto } from "../types/order.dto";
import _mockOrders from "../data/mockOrders.json";

/** Type Fix */
const orders = _mockOrders as unknown as OrderDto[];

export const getAllOrders = (count?: number): OrderDto[] => {
    return count ? orders.slice(0, count) : orders;
};

export const findOrderById = (orderId: string): OrderDto | undefined => {
    return orders.find(order => order.Id.toString() === orderId);
};

export const addOrder = (): OrderDto => {
    const newOrder = orders[Math.floor(Math.random() * orders.length)];
    return newOrder; // Simulate adding an order
};

export const updateOrder = (orderId: string, updateData: Partial<OrderDto>): OrderDto | undefined => {
    const orderIndex = orders.findIndex(order => order.Id.toString() === orderId);
    if (orderIndex !== -1) {
        const updatedOrder = {...orders[orderIndex], ...updateData};
        return updatedOrder; // Simulate updating an order
    }
    return undefined;
};

export const deleteOrder = (orderId: string): boolean => {
    const orderIndex = orders.findIndex(order => order.Id.toString() === orderId);
    return orderIndex !== -1; // Simulate deleting an order
};
