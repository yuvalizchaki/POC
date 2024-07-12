import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  Button,
  Divider,
  IconButton,
  Stack,
  Typography,
} from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from "@mui/icons-material/Delete";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import React, { useState } from "react";
import { ScreenProfile } from "../../../../types/screenProfile.types";
import { ScreenEntry } from "./screen-entry.component";
import { useAdminInfoContext } from "../../../../hooks/useAdminInfoContext";
import { useScreenProfilesContext } from "../../../../hooks/useScreenProfilesContext";
import { PairScreenDialog } from "./pair-screen-dialog.component";
import { ScreenProfileDialog } from "./screen-profile-dialog.component";

interface ScreenProfileEntryProps {
  profile: ScreenProfile;
}

export const ScreenProfileEntry = ({ profile }: ScreenProfileEntryProps) => {
  const [isExpanded, setIsExpanded] = useState<boolean>(false);
  const [addScreenOpen, setAddScreenOpen] = useState(false);
  const { deleteScreenProfile } = useAdminInfoContext();
  const { refetch } = useScreenProfilesContext();

  // #region    ======================================== Add Screen ========================================
  const handleAddScreenOpen = () => {
    setAddScreenOpen(true);
    setIsExpanded(true);
  };

  const handleAddScreenClose = () => {
    setAddScreenOpen(false);
  };

  const handleAddScreenSubmitted = () => {
    setAddScreenOpen(false);
    refetch();
  };

  const handleDeleteScreenProfile = (id: number) => {
    deleteScreenProfile(id).then(() => {
      refetch();
    });
  };
  // #endregion ======================================== Add Screen ========================================
  // #region    ======================================== Edit Screen Profile ========================================

  const [editScreenProfileOpen, setEditScreenProfileOpen] = useState(false);

  const handleEditScreenProfile = () => {
    if (profile) {
      setEditScreenProfileOpen(true);
    }
  };

  const handleEditScreenProfileClose = () => {
    setEditScreenProfileOpen(false);
  };

  const handleEditScreenProfileSubmitted = () => {
    setEditScreenProfileOpen(false);
    refetch();
  };

  // #endregion ======================================== Edit Screen Profile ========================================

  return (
    <React.Fragment key={profile.id}>
      <Accordion
        expanded={isExpanded}
        onChange={() => setIsExpanded((v) => !v)}
        variant="outlined"
        sx={{ borderRadius: 2 }}
        onClick={(e: React.MouseEvent<HTMLDivElement>) => {
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
            <IconButton
              onClick={(e: React.MouseEvent<HTMLButtonElement>) => {
                e.stopPropagation();
                handleEditScreenProfile();
              }}
            >
              <EditIcon />
            </IconButton>
            <Typography pl={1}>{profile.name}</Typography>
            <div style={{ flex: "1 0 0" }} />
            <Button
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
                <ScreenEntry screen={s} />
              </React.Fragment>
            ))}
          </Stack>
        </AccordionDetails>
      </Accordion>
      <ScreenProfileDialog
        isOpen={editScreenProfileOpen}
        mode="update"
        screenProfileData={profile}
        onSubmitted={handleEditScreenProfileSubmitted}
        onCancel={handleEditScreenProfileClose}
      />
      <PairScreenDialog
        isOpen={addScreenOpen}
        onSubmitted={handleAddScreenSubmitted}
        onCancel={handleAddScreenClose}
        profileId={profile.id}
      />
    </React.Fragment>
  );
};
