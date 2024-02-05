import { useContext } from "react";
import {
  ScreenInfoContext,
  ScreenInfoContextType,
} from "../context/screenInfo.context";

export const useScreenInfo = (): ScreenInfoContextType => {
  const context = useContext(ScreenInfoContext);

  if (context === undefined) {
    throw new Error("useScreenInfo must be used within a ScreenInfoProvider");
  }

  return context;
};
