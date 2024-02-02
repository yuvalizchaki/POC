import { BaseOrderItemDto } from './baseOrderItem.dto';

export interface ServiceOrderItemDto extends BaseOrderItemDto {
    ServiceProfileId: number;
    ServiceId: number;
    ServiceProfileName?: string;
    ServiceProfileDescription?: string;
}
