import { Outlet } from "react-router-dom";
import { AdminInfoProvider } from "../../context/adminInfo.context";

const AdminPage = () => {
  return (
    <AdminInfoProvider>
      <Outlet />
    </AdminInfoProvider>
  );
};

export default AdminPage;
