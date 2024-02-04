import axios, { Method } from "axios";

/** Modify this URL */
const WEBHOOK_URL = "http://localhost:5177/webhook";

export const sendToWebHook = async (
  route: string,
  method: Method,
  data?: unknown
): Promise<void> => {
  try {
    const url = `${WEBHOOK_URL}${route}`;
    console.log(`Webhook ${method} request sent to ${url}`);
    await axios({
      method,
      url,
      data,
    });
    console.log(`Webhook request sent successfully`);
  } catch (error) {
    console.error("Error sending request to webhook");
  }
};
