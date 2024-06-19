import { Navigate } from "react-router-dom";
import { useAdminInfo } from "../../context/adminInfo.context";

const AdminLogoutPage = () => {
  const { logoutAdmin } = useAdminInfo();
  logoutAdmin();
  return <Navigate to="/admin" />;
};
export default AdminLogoutPage;
