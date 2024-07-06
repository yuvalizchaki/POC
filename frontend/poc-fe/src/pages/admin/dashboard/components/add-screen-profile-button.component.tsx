import { Box, Button, Typography } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import { useState } from "react";
import { useScreenProfilesContext } from "../../../../hooks/useScreenProfilesContext";
import { ScreenProfileDialog } from "./screen-profile-dialog.component";

// interface AddScreenProfileButtonProps {}

export const AddScreenProfileButton = () => {
  const [addScreenProfileOpen, setAddScreenProfileOpen] = useState(false);

  const { refetch } = useScreenProfilesContext();

  const handleAddScreenProfileOpen = () => {
    setAddScreenProfileOpen(true);
  };

  const handleAddScreenProfileSubmitted = () => {
    setAddScreenProfileOpen(false);
    refetch();
  };
  const handleAddScreenProfileClose = () => {
    setAddScreenProfileOpen(false);
  };

  return (
    <Box display="flex" alignItems="center">
      <Typography variant="h5">Screen Profiles</Typography>
      <div style={{ flex: "1 0 0" }} />
      <Button
        variant="outlined"
        startIcon={<AddIcon />}
        onClick={handleAddScreenProfileOpen}
      >
        Add Profile
      </Button>
      <ScreenProfileDialog
        isOpen={addScreenProfileOpen}
        mode="create"
        onSubmitted={handleAddScreenProfileSubmitted}
        onCancel={handleAddScreenProfileClose}
      />
    </Box>
  );
};
