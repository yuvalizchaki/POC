import { Navigate } from "react-router-dom";
import { OrdersScreen } from "../../components/screen-templates";
import { useScreenInfo } from "../../hooks/useScreenInfo";

const ScreenContentPage = () => {
  const { screenInfo } = useScreenInfo();

  if (!screenInfo) {
    return <Navigate to={"/screen/pair"} replace />;
  }

  return (
    <>
      <OrdersScreen screenInfo={screenInfo} />
    </>
  );
};

export default ScreenContentPage;