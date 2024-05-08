import { Stack } from "@mui/material";
import { ScreenProfile } from "../../../types/screenProfile.types";
import { getAllScreenProfiles } from "../../../services/adminService";
import { useState } from "react";
import { AddScreenProfileComponent } from "./components/addScreenProfileComponent";
import { ProfileComponent } from "./components/profileComponent";

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

function AdminDashboard() {
  const [profiles, setProfiles] = useState<ScreenProfile[]>([]);
  const fetchScreenProfiles = () => {
    getAllScreenProfiles().then((data) => {
      setProfiles(data);
    });
  };
  return (
    <Stack direction="column" spacing={2}>
      <AddScreenProfileComponent
        style={style}
        fetchScreenProfiles={fetchScreenProfiles}
      />
      {profiles.map((p) => {
        return (
          <ProfileComponent
            style={style}
            profile={p}
            fetchScreenProfiles={fetchScreenProfiles}
          />
        );
      })}
    </Stack>
  );
}

export default AdminDashboard;
