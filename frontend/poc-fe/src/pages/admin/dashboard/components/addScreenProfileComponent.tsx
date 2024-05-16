import {
  Box,
  Button,
  Grid,
  Modal,
  SxProps,
  TextField,
  Typography,
} from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import { createScreenProfile } from "../../../../services/adminService";
import { useState, ChangeEvent } from "react";

interface AddScreenProfileProps {
  sx: SxProps;
  fetchScreenProfiles: () => void;
}

export const AddScreenProfileComponent = ({
  sx,
  fetchScreenProfiles,
}: AddScreenProfileProps) => {
  const [addScreenProfileOpen, setAddScreenProfileOpen] = useState(false);
  const [name, setName] = useState("");

  const handleAddScreenProfileOpen = () => {
    setAddScreenProfileOpen(true);
  };

  const handleAddScreenProfileClose = () => {
    setAddScreenProfileOpen(false);
  };

  const handleAddScreenProfile = () => {
    createScreenProfile(name).then(() => {
      fetchScreenProfiles();
    });
    setAddScreenProfileOpen(false);
  };

  const handleNameChange = (
    event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    setName(event.target.value);
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
      <Modal
        open={addScreenProfileOpen}
        onClose={handleAddScreenProfileClose}
        aria-labelledby="parent-modal-title"
        aria-describedby="parent-modal-description"
      >
        <Box sx={{ ...sx, width: 400 }}>
          <Grid container spacing={2}>
            <Grid item xs={6}>
              <TextField
                variant="outlined"
                fullWidth
                label="Name"
                value={name}
                onChange={handleNameChange} // Call the function to update the 'name' state
              />
            </Grid>
            <Grid item xs={6} sx={{ display: "flex" }}>
              <div style={{ flex: "1 0 0" }} />
              <Button
                disableElevation
                color="error"
                variant="text"
                onClick={handleAddScreenProfileClose}
              >
                Cancel
              </Button>
              <Button
                disableElevation
                variant="contained"
                sx={{ ml: 2 }}
                onClick={handleAddScreenProfile}
              >
                Save
              </Button>
            </Grid>
          </Grid>
        </Box>
      </Modal>
    </Box>
  );
};
