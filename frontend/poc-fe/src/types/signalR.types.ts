import { OrderDto } from "./crmTypes.types";
import { ScreenDto } from "./screenProfile.types";

export type SignalRHandler<T> = (payload: T) => void;

// #region    /======================================== Guest Hub Commands ========================================\

export interface ScreenAddedDto {
  id: number;
  pairingCode: string;
  screenProfileId: number;
}

export type PairingCodeDto = string

// Aligned with backend hub command strings
export interface GuestHubHandlers {
  screenAdded: SignalRHandler<ScreenAddedDto>;
  pairCode : SignalRHandler<PairingCodeDto>;
}


// #endregion \======================================== Guest Hub Commands ========================================/

// #region    /======================================== Screen Hub Commands ========================================\

export type OrderAddedDto = OrderDto;
export type OrderUpdatedDto = OrderDto;
export type OrderDeletedDto = number;
export type ScreenRemovedDto = ScreenDto;

// Aligned with backend hub command strings
export interface ScreenHubHandlers {
  orderAdded: SignalRHandler<OrderAddedDto>;
  orderUpdated: SignalRHandler<OrderUpdatedDto>;
  orderDeleted: SignalRHandler<OrderDeletedDto>;
  screenRemoved: SignalRHandler<ScreenRemovedDto>;
}

// #endregion \======================================== Screen Hub Commands ========================================/

export interface SignalRCommands {}

export type SignalRHandlers = GuestHubHandlers | ScreenHubHandlers;
