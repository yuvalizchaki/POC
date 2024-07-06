import { useContext } from "react";
import {
  AdminInfoContext,
  AdminInfoContextType,
} from "../context/adminInfo.context";

export const useAdminInfoContext = (): AdminInfoContextType => {
  const context = useContext(AdminInfoContext);

  if (context === undefined) {
    throw new Error("useAdminInfo must be used within a AdminInfoProvider");
  }

  return context;
};
