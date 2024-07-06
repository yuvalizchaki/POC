import { OrderStatus } from "./crmTypes.types";

// Enums
export enum TimeUnit {
  Hour = 0,
  Day = 1,
  Week = 2,
  Month = 3,
  Year = 4,
}

export enum TimeMode {
  Start = 1,
  End = 2,
  Fixed = 3,
}

export enum OrderTags {
  NeedWashing = 36,
  VIP = 3,
}

export enum DisplayTemplateType {
  Orders = 1,
  Inventory = 2
}

// Types
interface TimeRangePart {
  unit: TimeUnit;
  mode: TimeMode;
  amount: number;
}

interface TimeEncapsulatedDto {
  from: TimeRangePart;
  to: TimeRangePart;
}

interface OrderFilteringDto {
  timeRanges: TimeEncapsulatedDto,
  orderStatuses?: OrderStatus[];
  isPickup?: boolean;
  isSale?: boolean;
  entityIds?: number[];
  tags?: OrderTags[];
}

interface InventoryFilteringDto {
  entityIds?: number[];
}

interface DisplayConfigDto {
  isPaging: boolean;
  displayTemplate: DisplayTemplateType;
}

interface ScreenProfileFilteringDto {
  orderFiltering: OrderFilteringDto;
  inventoryFiltering: InventoryFilteringDto;
  inventorySorting?: string[];
  displayConfig: DisplayConfigDto;
}
export interface ScreenProfile {
  id: number;
  name?: string;
  companyId: number;
  screens: ScreenDto[];
  screenProfileFiltering: ScreenProfileFilteringDto;
}

export interface ScreenProfileFormFields {
  name?: string;
  companyId: number;
  screenProfileFiltering: ScreenProfileFilteringDto;
}
export type CreateScreenProfileDto = ScreenProfileFormFields;
export type UpdateScreenProfileDto = ScreenProfileFormFields;

export interface ScreenDto {
  id: number;
  screenProfileId: number;
  // Add other fields if necessary
}
