import { BaseOrderItemDto } from "./baseOrderItem.dto";

export interface OrderItemDto extends BaseOrderItemDto {
    ProductId: number;
    InventoryId: number;
    ProductName?: string;
    ProductDescription?: string;
}