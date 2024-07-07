import { Stack } from "@mui/material";
import { AddScreenProfileButton } from "./components/add-screen-profile-button.component";
import { ScreenProfileEntry } from "./components/screen-profile-entry.component";
import LoadingPage from "../../loading.page";
import { ScreenProfilesProvider } from "../../../context/screenProfiles.context";
import { useScreenProfilesContext } from "../../../hooks/useScreenProfilesContext";

const AdminDashboardContent = () => {
  const { profiles, isLoading } = useScreenProfilesContext();

  return (
    <Stack direction="column" spacing={2}>
      <AddScreenProfileButton />
      {isLoading ? (
        <LoadingPage />
      ) : (
        profiles.map((profile) => (
          <ScreenProfileEntry key={profile.id} profile={profile} />
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
