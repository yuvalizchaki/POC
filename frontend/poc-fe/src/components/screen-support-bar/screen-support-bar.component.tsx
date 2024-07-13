import { Box, Button, IconButton, Paper, Stack } from "@mui/material"
import { motion } from "framer-motion"
import { useState, useRef } from "react"

import LogoutIcon from '@mui/icons-material/Logout';
import RefreshIcon from '@mui/icons-material/Refresh';

import { useScreenInfoContext } from "../../hooks/useScreenInfoContext";
import { useNavigate } from "react-router-dom";

const MotionBox = motion(Box);

interface ScreenSupportBarProps { }

export const ScreenSupportBar = ({ }: ScreenSupportBarProps) => {
    const [visible, setVisible] = useState(false);
    const timeoutRef = useRef<number | null>(null);

    const { token, setToken, setScreenInfo } = useScreenInfoContext();

    const navigate = useNavigate();

    const handleVisibility = () => {
        if (timeoutRef.current) {
            clearTimeout(timeoutRef.current);
        }
        setVisible(true);
        timeoutRef.current = setTimeout(() => setVisible(false), 5000);
    };

    const handleMouseExit = () => {
        if (timeoutRef.current) {
            clearTimeout(timeoutRef.current);
        }
        setVisible(false);
    };

    const handleRefresh = () => {
        navigate("/");
    }

    const handleLogout = () => {
        console.log("Screen Logout");
        setToken(null);
        setScreenInfo(null);
    }

    return (
        <Box
            sx={{ position: 'absolute', width: '100%' }}
            onMouseEnter={handleVisibility}
            onMouseLeave={handleMouseExit}
            onClick={handleVisibility}
        >
            <MotionBox
                initial={{ y: -100 }}
                animate={{ y: visible ? 0 : -100 }}
                transition={{ ease: "easeInOut" }}
                sx={(theme) => ({
                    bgcolor: theme.palette.grey[800],
                    boxShadow: `${theme.palette.grey[800]} 0px 4px 12px`,
                    width: '100%'

                })}
            >
                <Stack direction="row"
                    padding={2}
                    spacing={2}
                    sx={{
                        height: 80,
                        boxSizing: 'border-box'
                    }}

                >
                    <div style={{ flex: '1 0 0' }} />
                    <Button
                        startIcon={<RefreshIcon sx={{ width: 'auto', height: 30 }} />}
                        variant="contained"
                        size="small"
                        color="info"
                        onClick={handleRefresh}
                    >
                        Refresh
                    </Button>
                    {token && <Button
                        startIcon={<LogoutIcon sx={{ width: 'auto', height: 30 }} />}
                        variant="contained"
                        size="small"
                        color="error"
                        onClick={handleLogout}
                    >
                        Disconnect
                    </Button>}
                </Stack>
            </MotionBox>
        </Box>
    );
}
