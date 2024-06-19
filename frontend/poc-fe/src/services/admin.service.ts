// import axios from "axios";

// import { API_BASE_URL } from "../config";
// import { ScreenProfile } from "../types/screenProfile.types";

// //********************************** clients **********************************//
// const loggedOutClient = axios.create({
//   baseURL: API_BASE_URL,
// });

// const adminClient = axios.create({
//   baseURL: API_BASE_URL,
// });

// //********************************** auth **********************************//
// export const loginAdmin = async (username: string, password: string) => {
//   try {
//     const response = await loggedOutClient({
//       method: "post",
//       url: "/admin",
//       data: { username, password },
//     });
//     const token = response.data;
//     adminClient.defaults.headers.common["Authorization"] = `Bearer ${token}`;
//     return token;
//   } catch (error) {
//     console.error("Login failed:", error);
//     throw error;
//   }
// };

// export const logoutAdmin = () => {
//   adminClient.defaults.headers.common["Authorization"] = "";
// };

// export const isLoggedIn = () => {
//   console.log(adminClient.defaults.headers.common)
//   return !!adminClient.defaults.headers.common["Authorization"];
// };

// //********************************** screen **********************************//
// export const pairScreen = (code: string, screenProfileId: number) => {
//   return adminClient({
//     method: "post",
//     url: "/screens",
//     data: {
//       pairingCode: code,
//       screenProfileId: screenProfileId,
//     },
//   });
// };

// export const removeScreen = (id: number) => {
//   return adminClient({
//     method: "delete",
//     url: "/screens/" + id,
//   });
// };

// // export const getScreen = () => {}
// // export const getAllScreens = () => {}

// //********************************** screen profile **********************************//
// export const createScreenProfile = (nameArg: string) => {
//   return adminClient({
//     method: "post",
//     url: "/screen-profiles",
//     data: {
//       name: nameArg,
//     },
//   });
// };

// // export const getScreenProfile = (id: number) => {}

// export const updateScreenProfile = () => {};

// export const deleteScreenProfile = (id: number) => {
//   return adminClient({
//     method: "delete",
//     url: "/screen-profiles/" + id,
//   });
// };

// export const getAllScreenProfiles = async (): Promise<ScreenProfile[]> => {
//   try {
//     const response = await adminClient.get("/screen-profiles");
//     console.log("response:", response);
//     return response.data;
//   } catch (error) {
//     console.error("Error fetching screen profiles:", error);
//     return [];
//   }
// };

// //********************************** orders **********************************//
// export const getAllOrders = () => {};
// export const getOrderById = () => {};

// //********************************** webhook **********************************//
// export const webhookOrderAdded = () => {};
// export const webhookOrderUpdated = () => {};
// export const WebhookOrderDeleted = () => {};

export default {};
