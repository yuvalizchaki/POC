import { Navigate } from "react-router-dom";
import { OrdersScreen } from "../../components/screen-templates";
import { useScreenInfo } from "../../hooks/useScreenInfo";

const ScreenContentPage = () => {
  const { token, screenInfo } = useScreenInfo();
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
