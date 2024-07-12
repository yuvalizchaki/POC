import { DisplayConfigDto } from "./screenProfile.types"

export interface ScreenMetaData {
  displayConfig: DisplayConfigDto;
  isInventory: boolean;
  name: string;
  screenId: number;
}