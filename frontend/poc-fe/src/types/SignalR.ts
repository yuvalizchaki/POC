import { OrderDto } from "./CrmTypes";

export type SignalRHandler<T> = (payload: T) => void;

// #region    /======================================== Guest Hub Commands ========================================\

export interface ScreenAddedDto {
  id: number;
  ip: string;
  screenProfileId: number;
}

// Aligned with backend hub command strings
export interface GuestHubHandlers {
  screenAdded: SignalRHandler<ScreenAddedDto>;
}

// #endregion \======================================== Guest Hub Commands ========================================/

// #region    /======================================== Screen Hub Commands ========================================\

export type OrderAddedDto = OrderDto;
export type OrderUpdatedDto = OrderDto;
export type OrderDeletedDto = number;

// Aligned with backend hub command strings
export interface ScreenHubHandlers {
  orderAdded: SignalRHandler<OrderAddedDto>;
  orderUpdated: SignalRHandler<OrderUpdatedDto>;
  orderDeleted: SignalRHandler<OrderDeletedDto>;
}

// #endregion \======================================== Screen Hub Commands ========================================/

export interface SignalRCommands {}

export type SignalRHandlers = GuestHubHandlers | ScreenHubHandlers;
