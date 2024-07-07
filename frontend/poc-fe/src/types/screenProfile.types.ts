import { OrderStatus } from "./crmTypes.types";

// Enums
export enum TimeUnit {
  Hour = 1,
  Day = 2,
  Week = 3,
  Month = 4,
  Year = 5,
}

export const timeUnitMap = {
  [TimeUnit.Hour]: 'hour',
  [TimeUnit.Day]: 'day',
  [TimeUnit.Week]: 'week',
  [TimeUnit.Month]: 'month',
  [TimeUnit.Year]: 'year',
};
export const timeUnitPluralMap = {
  [TimeUnit.Hour]: 'hours',
  [TimeUnit.Day]: 'days',
  [TimeUnit.Week]: 'weeks',
  [TimeUnit.Month]: 'months',
  [TimeUnit.Year]: 'years',
};
export const timeUnitPluralList = Object.entries(timeUnitPluralMap).map(([value, label]) => ({ value: Number(value), label }));
export const timeUnitList = Object.entries(timeUnitMap).map(([value, label]) => ({ value: Number(value), label }));

export enum TimeMode {
  Start = 1,
  End = 2,
  Fixed = 3,
}

export const timeModeMap = {
  [TimeMode.Start]: 'start of',
  [TimeMode.End]: 'end of',
  [TimeMode.Fixed]: 'now',
};
export const timeModeList = Object.entries(timeModeMap).map(([value, label]) => ({ value: Number(value), label }));

export enum TimeInclude {
  Incoming = 1,
  Outgoing = 2,
  Both = 3
}

export const timeIncludeMap = {
  [TimeInclude.Incoming]: 'incoming',
  [TimeInclude.Outgoing]: 'outgoing',
  [TimeInclude.Both]: 'all',
};

export const timeIncludeList = Object.entries(timeIncludeMap).map(([value, label]) => ({ value: Number(value), label }));

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
  include: TimeInclude;
}

interface OrderFilteringDto {
  timeRanges: TimeEncapsulatedDto,
  orderStatuses?: OrderStatus[];
  isPickup?: boolean;
  isSale?: boolean;
  entityIds?: number[];
  tags?: number[];
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
  inventoryFiltering?: InventoryFilteringDto;
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
