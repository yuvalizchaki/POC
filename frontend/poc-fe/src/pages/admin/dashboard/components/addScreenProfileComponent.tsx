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
import { useState, ChangeEvent } from "react";
import { useAdminInfo } from "../../../../context/adminInfo.context";
import { ScreenProfileAddData } from "../../../../types/screenProfile.types";
import moment from "moment";

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
  const { createScreenProfile } = useAdminInfo();

  const handleAddScreenProfileOpen = () => {
    setAddScreenProfileOpen(true);
  };

  const handleAddScreenProfileClose = () => {
    setAddScreenProfileOpen(false);
  };

  const handleAddScreenProfile = () => {
    // TODO: Add other information and fields
    const data: ScreenProfileAddData = {
      name: name,
      companyId: 1, // <-- TODO: Remove This!
      screenProfileFiltering: {
        orderTimeRange: {
          startDate: moment().startOf("day").toISOString(),
          endDate: moment().endOf("day").toISOString(),
        },
        // orderStatusses: [];
        // isPickup: false;
        // isSale: false;
        // entityIds: [];
      },
    };
    createScreenProfile(data).then(() => {
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
