import { Stack } from "@mui/material";
import { AddScreenProfileComponent } from "./components/add-screen-profile.component";
import { ProfileComponent } from "./components/screen-profile.component";
import LoadingPage from "../../loading.page";
import { ScreenProfilesProvider } from "../../../context/screenProfiles.context";
import { useScreenProfilesContext } from "../../../hooks/useScreenProfilesContext";

const AdminDashboardContent = () => {
  const { profiles, isLoading } = useScreenProfilesContext();

  return (
    <Stack direction="column" spacing={2}>
      <AddScreenProfileComponent />
      {isLoading ? (
        <LoadingPage />
      ) : (
        profiles.map((profile) => (
          <ProfileComponent key={profile.id} profile={profile} />
        ))
      )}
    </Stack>
  );
};

const AdminDashboard = () => {
  return (
    <ScreenProfilesProvider>
      <AdminDashboardContent />
    </ScreenProfilesProvider>
  );
};

export default AdminDashboard;
