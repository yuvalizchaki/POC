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
  TextField,
  Typography,
} from "@mui/material";
import { ScreenProfile } from "../../../types/screenProfile.types";

import AddIcon from "@mui/icons-material/Add";
// import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import {
  createScreenProfile,
  deleteScreenProfile,
  getAllScreenProfiles,
  pairScreen,
} from "../../../services/adminService";
import { ScreenComponent } from "./components/screenComponent";
import { useState, useEffect, ChangeEvent } from "react";
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

  const [addScreenProfileOpen, setAddScreenProfileOpen] = useState(false);
  const [addScreenOpen, setAddScreenOpen] = useState(false);

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

  const [code, setCode] = useState("");
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
  useEffect(() => {
    fetchScreenProfiles();
  }, []);
  return (
    <Stack direction="column" spacing={2}>
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
          <Box sx={{ ...style, width: 400 }}>
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
      {profiles.map((p) => {
        return (
          <>
            <Accordion variant="outlined" sx={{ borderRadius: 2 }}>
              <AccordionSummary expandIcon={<ExpandMoreIcon />}>
                <Stack direction="row" alignItems="center" width="100%">
                  <Typography>{p.name}</Typography>
                  <div style={{ flex: "1 0 0" }} />
                  <Button
                    size="small"
                    variant="text"
                    startIcon={<AddIcon />}
                    onClick={handleAddScreenOpen}
                  >
                    Add Screen
                  </Button>
                  <Modal
                    open={addScreenOpen}
                    onClose={handleAddScreenClose}
                    aria-labelledby="parent-modal-title"
                    aria-describedby="parent-modal-description"
                  >
                    <Box sx={{ ...style, width: 400 }}>
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
                              handleAddScreen(p.id);
                            }}
                          >
                            Save
                          </Button>
                        </Grid>
                      </Grid>
                    </Box>
                  </Modal>
                  <IconButton
                    color="error"
                    size="small"
                    onClick={() => {
                      handleDeleteScreenProfile(p.id);
                    }}
                  >
                    <DeleteIcon />
                  </IconButton>
                </Stack>
              </AccordionSummary>
              <AccordionDetails>
                <Stack direction="column" spacing={1}>
                  {p.screens.map((s, i) => (
                    <>
                      {i > 0 && <Divider />}
                      <ScreenComponent screen={s} fetchScreenProfiles={fetchScreenProfiles}/>
                    </>
                  ))}
                </Stack>
              </AccordionDetails>
            </Accordion>
          </>
        );
      })}
    </Stack>
  );
}

export default AdminDashboard;
