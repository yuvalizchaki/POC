import React, { createContext, ReactNode, useCallback, useState } from "react";
import { ScreenInfo } from "../types/screenInfo.types";

interface ScreenInfoProviderProps {
  children: ReactNode;
}

export interface ScreenInfoContextType {
  screenInfo: ScreenInfo | null;
  setScreenInfo: (info: ScreenInfo | null) => void;
}

export const ScreenInfoContext = createContext<ScreenInfoContextType>({
  screenInfo: null,
  setScreenInfo: () => {},
});

export const ScreenInfoProvider: React.FC<ScreenInfoProviderProps> = ({
  children,
}) => {
  const [_screenInfo, _setScreenInfo] = useState<ScreenInfo | null>(null);

  const setScreenInfo = useCallback((screenInfo: ScreenInfo | null) => {
    _setScreenInfo(screenInfo);
  }, []);

  const contextValue = {
    screenInfo: _screenInfo,
    setScreenInfo,
  };

  return (
    <ScreenInfoContext.Provider value={contextValue}>
      {children}
    </ScreenInfoContext.Provider>
  );
};
