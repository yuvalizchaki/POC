import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  Box,
  Button,
  Divider,
  Grid,
  IconButton,
  Modal,
  Stack,
  SxProps,
  TextField,
  Typography,
} from "@mui/material";
// import DesktopAccessDisabledIcon from "@mui/icons-material/DesktopAccessDisabled";
import AddIcon from "@mui/icons-material/Add";
// import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import { ScreenProfile } from "../../../../types/screenProfile.types";
import React, { useState, ChangeEvent } from "react";
import { ScreenComponent } from "./screenComponent";
import { useAdminInfo } from "../../../../context/adminInfo.context";

interface ProfileProps {
  sx: SxProps;
  profile: ScreenProfile;
  fetchScreenProfiles: () => void;
}

export const ProfileComponent = ({
  sx,
  profile,
  fetchScreenProfiles,
}: ProfileProps) => {
  const [addScreenOpen, setAddScreenOpen] = useState(false);
  const [code, setCode] = useState("");

  const { pairScreen, deleteScreenProfile } = useAdminInfo();

  const handleCodeChange = (
    event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    setCode(event.target.value);
  };

  const handleAddScreenOpen = () => {
    setAddScreenOpen(true);
  };

  const handleAddScreenClose = () => {
    setAddScreenOpen(false);
  };

  const handleAddScreen = (id: number) => {
    pairScreen(code, id).then(() => {
      fetchScreenProfiles();
    });
    setAddScreenOpen(false);
  };

  const handleDeleteScreenProfile = (id: number) => {
    deleteScreenProfile(id).then(() => {
      fetchScreenProfiles();
    });
  };

  return (
    <React.Fragment key={profile.id}>
      <Accordion
        variant="outlined"
        sx={{ borderRadius: 2 }}
        onClick={(e: React.MouseEvent<HTMLDivElement>) => {
          console.log("[DEBUG] e: ", e);
          if (e.target !== e.currentTarget) {
            e.preventDefault();
          }
        }}
      >
        <AccordionSummary
          expandIcon={<ExpandMoreIcon />}
          sx={{
            [`.MuiAccordionSummary-expandIconWrapper:not(.Mui-expanded)`]: {
              transform: "rotate(90deg)",
            },
            [`.MuiAccordionSummary-expandIconWrapper.Mui-expanded`]: {
              transform: "rotate(0deg)",
            },
          }}
        >
          <Stack direction="row" alignItems="center" width="100%">
            <Typography>{profile.name}</Typography>
            <div style={{ flex: "1 0 0" }} />
            <Button
              size="small"
              variant="text"
              startIcon={<AddIcon />}
              onClick={(e: React.MouseEvent<HTMLButtonElement>) => {
                e.stopPropagation();
                handleAddScreenOpen();
              }}
            >
              Add Screen
            </Button>
            <IconButton
              color="error"
              size="small"
              onClick={(e: React.MouseEvent<HTMLButtonElement>) => {
                e.stopPropagation();
                handleDeleteScreenProfile(profile.id);
              }}
            >
              <DeleteIcon />
            </IconButton>
          </Stack>
        </AccordionSummary>
        <AccordionDetails>
          <Stack direction="column" spacing={1}>
            {profile.screens.map((s, i) => (
              <React.Fragment key={s.id}>
                {i > 0 && <Divider />}
                <ScreenComponent
                  screen={s}
                  fetchScreenProfiles={fetchScreenProfiles}
                />
              </React.Fragment>
            ))}
          </Stack>
        </AccordionDetails>
      </Accordion>
      <Modal
        open={addScreenOpen}
        onClose={handleAddScreenClose}
        aria-labelledby="parent-modal-title"
        aria-describedby="parent-modal-description"
      >
        <Box sx={{ ...sx, width: 400 }}>
          <Grid container spacing={2}>
            <Grid item xs={6}>
              <TextField
                variant="outlined"
                fullWidth
                label="code"
                value={code}
                onChange={handleCodeChange} // Call the function to update the 'code' state
              />
            </Grid>
            <Grid item xs={6} sx={{ display: "flex" }}>
              <div style={{ flex: "1 0 0" }} />
              <Button
                disableElevation
                color="error"
                variant="text"
                onClick={handleAddScreenClose}
              >
                Cancel
              </Button>
              <Button
                disableElevation
                variant="contained"
                sx={{ ml: 2 }}
                onClick={() => {
                  handleAddScreen(profile.id);
                }}
              >
                Save
              </Button>
            </Grid>
          </Grid>
        </Box>
      </Modal>
    </React.Fragment>
  );
};
