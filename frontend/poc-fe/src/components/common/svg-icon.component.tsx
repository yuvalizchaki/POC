import React from "react";
import { Box, SxProps } from "@mui/material";

interface AppSvgIconProps {
  url: string;
  sx?: SxProps;
}

export const AppSvgIcon: React.FC<AppSvgIconProps> = ({ url, sx }) => {
  return <Box component="img" src={`/${url}`} sx={{ ...sx }} />;
};
