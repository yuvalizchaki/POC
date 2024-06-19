import { Navigate } from "react-router-dom";
import { useAdminInfo } from "../../hooks/useAdminInfo";

const AdminLogoutPage = () => {
  const { logoutAdmin } = useAdminInfo();
  logoutAdmin();
  return <Navigate to="/admin" />;
};
export default AdminLogoutPage;
