import { OrderDto } from "./crmTypes.types";
import { ScreenDto } from "./screenProfile.types";

export type SignalRHandler<T> = (payload: T) => void;

// #region    /======================================== Guest Hub Commands ========================================\

export type ScreenAddedDto = string;

export type PairingCodeDto = string

// Aligned with backend hub command strings
export interface GuestHubHandlers {
  screenAdded: SignalRHandler<ScreenAddedDto>;
  pairCode: SignalRHandler<PairingCodeDto>;
}


// #endregion \======================================== Guest Hub Commands ========================================/
// #region    /======================================== Screen Hub Commands ========================================\

export type RefreshDataDto = void;
export type OrderAddedDto = OrderDto[];
export type OrderUpdatedDto = OrderDto[];
export type ProfileUpdatedDto = void; // Assuming no payload for profileUpdated
export type OrderDeletedDto = number[];
export type InventoryUpdatedDto = void; // Assuming no payload for inventoryUpdated
export type ScreenRemovedDto = ScreenDto;

// Aligned with backend hub command strings
export interface ScreenHubHandlers {
  refreshData: SignalRHandler<RefreshDataDto>;
  orderAdded: SignalRHandler<OrderAddedDto>;
  orderDeleted: SignalRHandler<OrderDeletedDto>;
  orderUpdated: SignalRHandler<OrderUpdatedDto>;
  profileUpdated: SignalRHandler<ProfileUpdatedDto>;
  inventoryUpdated: SignalRHandler<InventoryUpdatedDto>;
  screenRemoved: SignalRHandler<ScreenRemovedDto>;
}

// #endregion \======================================== Screen Hub Commands ========================================/
// #region    /======================================== Admin Hub Commands ========================================\

export type ScreenConnectedDto = string;
export type ScreenDisconnectedDto = string;

// Aligned with backend hub command strings
export interface ScreenHubHandlers {
  screenConnected: SignalRHandler<ScreenConnectedDto>;
  screenDisconnected: SignalRHandler<ScreenDisconnectedDto>;
}

// #endregion \======================================== Admin Hub Commands ========================================/


export interface SignalRCommands { }

export type SignalRHandlers = GuestHubHandlers | ScreenHubHandlers;
