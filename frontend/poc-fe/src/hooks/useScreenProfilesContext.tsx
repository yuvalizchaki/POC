import { useContext } from "react";
import { ScreenProfilesContext } from "../context/screenProfiles.context";

export const useScreenProfilesContext = () => {
  const context = useContext(ScreenProfilesContext);
  if (context === undefined) {
    throw new Error(
      "useScreenProfilesContext must be used within a ScreenProfilesProvider"
    );
  }
  return context;
};
