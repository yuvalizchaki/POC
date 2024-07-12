import React from 'react';
import { Typography, SxProps } from '@mui/material';
import moment from 'moment';
import { useCurrentTime } from '../../hooks/useCurrentTime';

interface AppClockProps {
    format: string;
    sx?: SxProps;
};

export const AppClock: React.FC<AppClockProps> = ({ format, sx }) => {
    const time = useCurrentTime();
    
    return (
        <>
            <Typography variant='body1' sx={{ ...sx }}>{moment(time).format(format)}</Typography>
        </>
    );
};
