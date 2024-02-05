import { Suspense } from "react";
import { Outlet } from "react-router-dom";
import LoadingPage from "./loading.page";

const MainPage = () => {
  return (
    <Suspense fallback={<LoadingPage />}>
      <Outlet />
    </Suspense>
  );
};

export default MainPage;
