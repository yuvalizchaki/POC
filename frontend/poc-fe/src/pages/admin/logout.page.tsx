import { Navigate } from "react-router-dom";
import { useAdminInfoContext } from "../../hooks/useAdminInfoContext";

const AdminLogoutPage = () => {
  const { logoutAdmin } = useAdminInfoContext();
  logoutAdmin();
  return <Navigate to="/admin" />;
};
export default AdminLogoutPage;
