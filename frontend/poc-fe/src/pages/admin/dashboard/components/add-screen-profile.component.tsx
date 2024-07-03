import { Box, Button, Grid, Modal, TextField, Typography } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import { useState, ChangeEvent } from "react";
import { useAdminInfoContext } from "../../../../hooks/useAdminInfoContext";
import { useScreenProfilesContext } from "../../../../hooks/useScreenProfilesContext";
import {
  CreateScreenProfileDto,
  DisplayTemplateType,
  TimeMode,
  TimeUnit,
} from "../../../../types/screenProfile.types";

// interface AddScreenProfileProps {}

export const AddScreenProfileComponent = () => {
  const [addScreenProfileOpen, setAddScreenProfileOpen] = useState(false);
  const [name, setName] = useState("");
  const { createScreenProfile } = useAdminInfoContext();

  const { refetch } = useScreenProfilesContext();

  const handleAddScreenProfileOpen = () => {
    setAddScreenProfileOpen(true);
  };

  const handleAddScreenProfileClose = () => {
    setAddScreenProfileOpen(false);
  };

  const handleAddScreenProfile = () => {
    // TODO: Add other information and fields
    const data: CreateScreenProfileDto = {
      name: name,
      companyId: 1, // <-- TODO: Remove This!
      screenProfileFiltering: {
        orderFiltering: {
          from: {
            mode: TimeMode.Start,
            unit: TimeUnit.Day,
            amount: 0,
          },
          to: {
            mode: TimeMode.End,
            unit: TimeUnit.Day,
            amount: 0,
          },
        },
        displayConfig: {
          displayTemplate: DisplayTemplateType.Table,
          isPaging: false,
        },
        inventoryFiltering: {},
      },
    };
    createScreenProfile(data).then(() => {
      refetch();
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
      <Modal open={addScreenProfileOpen} onClose={handleAddScreenProfileClose}>
        <Box sx={{ width: 400 }}>
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
