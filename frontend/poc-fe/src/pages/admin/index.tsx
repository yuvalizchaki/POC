import { Outlet } from "react-router-dom";
import { AdminInfoProvider } from "../../context/adminInfo.context";
import { SignalRProvider } from "../../context/signalR.context";
import { API_BASE_URL } from "../../config";

const AdminPage = () => {
  return (
    <SignalRProvider baseUrl={API_BASE_URL}>
      <AdminInfoProvider>
        <Outlet />
      </AdminInfoProvider>
    </SignalRProvider>
  );
};

export default AdminPage;
