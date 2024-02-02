import { BaseOrderItemDto } from './baseOrderItem.dto';

export interface PeopleOrderItemDto extends BaseOrderItemDto {
    PeopleProfileId: number;
    PeopleId: number;
    PeopleProfileName?: string;
    PeopleProfileDescription?: string;
}
