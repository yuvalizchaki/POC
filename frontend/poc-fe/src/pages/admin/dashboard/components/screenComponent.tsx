import { IconButton, Stack, Typography } from "@mui/material";

// import DesktopAccessDisabledIcon from "@mui/icons-material/DesktopAccessDisabled";
import ConnectedTvIcon from "@mui/icons-material/ConnectedTv";

import CloseIcon from "@mui/icons-material/Close";
import { ScreenDto } from "../../../../types/screenProfile.types";
import { removeScreen } from "../../../../services/adminService";
interface ScreenProps {
  screen: ScreenDto;
  fetchScreenProfiles: () => void;
}

export const ScreenComponent = ({
  screen,
  fetchScreenProfiles,
}: ScreenProps) => {
  const handleRemoveScreen = () => {
    removeScreen(screen.id).then(() => {
      fetchScreenProfiles();
    });
  };
  return (
    <Stack
      direction="row"
      // justifyContent="space-between"
      alignItems="center"
      spacing={2}
    >
      <Typography>Screen #{screen.id}</Typography>
      <div style={{ flex: "1 0 0" }} />

      <ConnectedTvIcon color="success" />
      <IconButton onClick={handleRemoveScreen} size="small">
        <CloseIcon />
      </IconButton>
    </Stack>
  );
};
