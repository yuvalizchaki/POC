import { IconButton, Stack, Typography } from "@mui/material";
// import DesktopAccessDisabledIcon from "@mui/icons-material/DesktopAccessDisabled";
import ConnectedTvIcon from "@mui/icons-material/ConnectedTv";
import CloseIcon from "@mui/icons-material/Close";
import { ScreenDto } from "../../../../types/screenProfile.types";
import { useAdminInfoContext } from "../../../../hooks/useAdminInfoContext";
import { useScreenProfilesContext } from "../../../../hooks/useScreenProfilesContext";

interface ScreenEntryProps {
  screen: ScreenDto;
}

export const ScreenEntry = ({ screen }: ScreenEntryProps) => {
  const { removeScreen } = useAdminInfoContext();
  const { refetch } = useScreenProfilesContext();
  const handleRemoveScreen = () => {
    removeScreen(screen.id).then(() => {
      refetch();
    });
  };
  return (
    <Stack
      direction="row"
      // justifyContent="space-between"
      alignItems="center"
      spacing={2}
    >
      <Typography>{screen.name}</Typography>
      <div style={{ flex: "1 0 0" }} />

      <ConnectedTvIcon color="success" />
      <IconButton onClick={handleRemoveScreen} size="small">
        <CloseIcon />
      </IconButton>
    </Stack>
  );
};
