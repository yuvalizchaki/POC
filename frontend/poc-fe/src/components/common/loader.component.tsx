import { useEffect, useState } from "react";
import { AppSvgIcon } from "./svg-icon.component";
import { SxProps } from "@mui/material";

interface AppLoaderProps {
  fadeInTime?: number;
  sx?: SxProps;
}

export const AppLoader: React.FC<AppLoaderProps> = ({ fadeInTime, sx }) => {
  const [showIcon, setShowIcon] = useState(fadeInTime ? false : true);

  useEffect(() => {
    if (fadeInTime) {
      const timer = setTimeout(() => {
        setShowIcon(true);
      }, fadeInTime * 1000);

      return () => clearTimeout(timer);
    }
  }, [fadeInTime]);

  return (
    <AppSvgIcon
      url="loading-pulse.svg"
      sx={{
        width: 50,
        height: 50,
        opacity: showIcon ? 1 : 0,
        transition: fadeInTime ? `opacity ${fadeInTime}s` : undefined,
        ...sx,
      }}
    />
  );
};
