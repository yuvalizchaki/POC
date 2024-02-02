export type SignalRHandler<T> = (payload: T) => void;

export interface ScreenAddedDto {
  id: number;
  ip: string;
  screenProfileId: number;
}

export interface SignalRCommands {}

export interface SignalRHandlers {
  screenAdded: SignalRHandler<ScreenAddedDto>;
}
