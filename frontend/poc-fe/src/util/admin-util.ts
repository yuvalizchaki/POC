import { AdminData } from "../types/adminData.types";

export const parseAdminData = (data: string | null): AdminData | null => {
  if (!data) return null;

  try {
    const parsedData = JSON.parse(atob(data));
    if (
      typeof parsedData.username === "string" &&
      typeof parsedData.companyId === "number" &&
      typeof parsedData.token === "string"
    ) {
      return parsedData;
    } else {
      throw new Error("Invalid data format");
    }
  } catch (error) {
    console.error("Failed to parse admin data:", error);
    throw new Error("Failed to parse admin data");
  }
};

export const encodeAdminData = (adminData: AdminData): string => {
  if (
    typeof adminData.username === "string" &&
    typeof adminData.companyId === "number" &&
    typeof adminData.token === "string"
  ) {
    try {
      const stringifiedData = JSON.stringify(adminData);
      return btoa(stringifiedData);
    } catch (error) {
      console.error("Failed to encode admin data:", error);
      throw new Error("Failed to encode admin data");
    }
  } else {
    throw new Error("Invalid admin data format");
  }
};
