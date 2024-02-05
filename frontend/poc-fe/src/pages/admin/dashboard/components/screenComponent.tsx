import { IconButton, Stack, Typography } from "@mui/material";

import DesktopAccessDisabledIcon from "@mui/icons-material/DesktopAccessDisabled";
import ConnectedTvIcon from "@mui/icons-material/ConnectedTv";

import CloseIcon from "@mui/icons-material/Close";
import { Screen } from "../../../../types/screenProfile.types";
interface ScreenProps {
  screen: Screen;
}

export const ScreenComponent = ({ screen }: ScreenProps) => {
  return (
    <Stack
        direction="row"
        justifyContent="space-between"
        alignItems="center"
        spacing={2}
    >
        <Typography>{screen.ip}</Typography>
        <Stack direction="row" spacing={2}>
        <IconButton>
          <DesktopAccessDisabledIcon />
        </IconButton>
        <IconButton>
          <ConnectedTvIcon />
        </IconButton>
        <IconButton>
          <CloseIcon />
        </IconButton>
      </Stack>
    </Stack>
  );
};
