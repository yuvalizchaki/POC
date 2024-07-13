import { Navigate } from "react-router-dom";
import { OrdersScreen } from "../../components/screen-templates";
import { useScreenInfoContext } from "../../hooks/useScreenInfoContext";

const ScreenContentPage = () => {
  const { token } = useScreenInfoContext();
  
  // TODO: Fetch screenInfo
  if (!token) {
    return <Navigate to={"/screen/pair"} replace />;
  }

  return (
    <>
        <OrdersScreen />
    </>
  );
};

export default ScreenContentPage;
