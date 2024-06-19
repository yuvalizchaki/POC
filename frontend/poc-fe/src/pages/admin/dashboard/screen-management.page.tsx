import { Stack } from "@mui/material";
import { AddScreenProfileComponent } from "./components/addScreenProfileComponent";
import { ProfileComponent } from "./components/profileComponent";
import LoadingPage from "../../loading.page";
import { useScreenProfiles } from "../../../hooks/useScreenProfiles";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 400,
  bgcolor: "background.paper",
  border: "2px solid #000",
  boxShadow: 24,
  pt: 2,
  px: 4,
  pb: 3,
};

const AdminDashboard = () => {
  const { profiles, isLoading, refetch } = useScreenProfiles();

  return (
    <Stack direction="column" spacing={2}>
      <AddScreenProfileComponent sx={style} fetchScreenProfiles={refetch} />
      {isLoading ? (
        <LoadingPage />
      ) : (
        profiles.map((p) => (
          <ProfileComponent
            key={p.id}
            sx={style}
            profile={p}
            fetchScreenProfiles={refetch}
          />
        ))
      )}
    </Stack>
  );
};

export default AdminDashboard;
