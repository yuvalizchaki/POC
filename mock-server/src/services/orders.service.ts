import { OrderDto } from "../types/order.dto";
import _mockOrders from "../data/mockOrders.json";

/** Type Fix */
const mockOrders = _mockOrders as unknown as OrderDto[];
const orders: OrderDto[] = [];

export const getAllOrders = (count?: number): OrderDto[] => {
  return count ? orders.slice(0, count) : orders;
};

export const findOrderById = (orderId: string): OrderDto | undefined => {
  return orders.find((order) => order.Id.toString() === orderId);
};

export const addOrder = (): OrderDto => {
  let newOrder: OrderDto;
  do {
    newOrder = mockOrders[Math.floor(Math.random() * mockOrders.length)];
  } while (orders.find((order) => order.Id === newOrder.Id));

  orders.push(newOrder);
  return newOrder; // Simulate adding a non-duplicate order
};

export const updateOrder = (
  orderId: string,
  updateData: Partial<OrderDto>
): OrderDto | undefined => {
  const orderIndex = orders.findIndex(
    (order) => order.Id.toString() === orderId
  );
  if (orderIndex !== -1) {
    const updatedOrder = { ...orders[orderIndex], ...updateData };
    orders[orderIndex] = updatedOrder;
    return updatedOrder; // Simulate updating an order
  }
  return undefined;
};

export const deleteOrder = (orderId: string): boolean => {
  const orderIndex = orders.findIndex(
    (order) => order.Id.toString() === orderId
  );
  if (orderIndex !== -1) {
    orders.splice(orderIndex, 1);
    return true;
  }
  return false;
};
