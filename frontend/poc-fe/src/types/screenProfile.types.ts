import { OrderStatus } from "./crmTypes.types";

export interface ScreenProfile {
  // TODO: Implement
  id: number;
  name: string;
  screens: ScreenDto[];
}

export interface ScreenProfileAddData {
  name?: string;
  companyId: number;
  screenProfileFiltering: {
    orderTimeRange: {
      startDate: string;
      endDate: string;
    };
    orderStatusses?: OrderStatus[];
    isPickup?: boolean;
    isSale?: boolean;
    entityIds?: number[];
  };
}

export interface ScreeProfileUpdateData {
  // TODO: Implement
  name: string;
}

export interface ScreenDto {
  // TODO: Implement
  id: number;
  screenProfileId: number;
}
