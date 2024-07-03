import { Navigate } from "react-router-dom";
import { OrdersScreen } from "../../components/screen-templates";
import { useScreenInfoContext } from "../../hooks/useScreenInfoContext";

const ScreenContentPage = () => {
  const { token, screenInfo } = useScreenInfoContext();
  // TODO: Fetch screenInfo
  if (!token) {
    return <Navigate to={"/screen/pair"} replace />;
  }

  return (
    <>
      <OrdersScreen
        screenInfo={
          screenInfo ?? { id: 1, screenProfileId: 1 }
        } /* TODO: Remove this ?? */
      />
    </>
  );
};

export default ScreenContentPage;
