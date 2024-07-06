import { createTheme, Theme } from '@mui/material';

const baseTheme: Theme = createTheme({
    direction: 'ltr',
    palette: {
        primary: {
            light: '#63ccff',
            main: '#009be5',
            dark: '#006db3',
        },
        secondary: {
            main: '#004d72',
        },
    },
    typography: {
        h5: {
            fontWeight: 500,
            fontSize: 26,
            letterSpacing: 0.5,
        },
    },
    shape: {
        borderRadius: 8,
    },
    components: {
        MuiInputBase: {
            defaultProps: {
                disableInjectingGlobalStyles: true, // <-- This is a tip from MUI about performace (https://mui.com/material-ui/react-text-field/ + https://mui.com/material-ui/api/input-base/#InputBase-prop-disableInjectingGlobalStyles)
            },
        },
        MuiTab: {
            defaultProps: {
                disableRipple: true,
            },
        },
    },
    mixins: {
        toolbar: {
            minHeight: 48,
        },
    },
});

// This theme split is important since lot's properties depands on base theme declaration.
export const theme: Theme = {
    ...baseTheme,
    palette: {
        ...baseTheme.palette,
        // Custom colors: ------------------------------------------ //
        T_StatusReady: baseTheme.palette.augmentColor({
            color: {
                main: '#734193',
                //light: '#00ff00',
            },
            //name: 'T_StatusReady',
        }),
        T_YellowMarker: baseTheme.palette.augmentColor({
            color: { main: '#ffff0020' },
        }),
        T_OrderNumber: baseTheme.palette.augmentColor({
            color: { main: '#00618f' },
        }),
        // --------------------------------------------------------- //
    },
    components: {
        //MuiDrawer: {
        //    styleOverrides: {
        //        paper: {
        //            backgroundColor: '#081627',
        //        },
        //    },
        //},
        MuiButton: {
            styleOverrides: {
                root: {
                    textTransform: 'none',
                },
                contained: {
                    boxShadow: 'none',
                    '&:active': {
                        boxShadow: 'none',
                    },
                },
            },
        },
        MuiTabs: {
            styleOverrides: {
                root: {
                    marginLeft: baseTheme.spacing(1),
                },
                indicator: {
                    height: 3,
                    borderTopLeftRadius: 3,
                    borderTopRightRadius: 3,
                    backgroundColor: baseTheme.palette.common.white,
                },
            },
        },
        MuiTab: {
            styleOverrides: {
                root: {
                    textTransform: 'none',
                    margin: '0 5px',
                    minWidth: 0,
                    padding: 0,
                    [baseTheme.breakpoints.up('md')]: {
                        padding: '15px',
                        minWidth: 0,
                    },
                },
            },
        },
        MuiIconButton: {
            styleOverrides: {
                root: {
                    padding: baseTheme.spacing(1),
                },
            },
        },
        MuiTooltip: {
            styleOverrides: {
                tooltip: {
                    borderRadius: 4,
                },
            },
        },
        MuiDivider: {
            styleOverrides: {
                root: {
                    backgroundColor: 'rgb(255,255,255,0.15)',
                },
            },
        },
        MuiListItemButton: {
            styleOverrides: {
                root: {
                    '&.Mui-selected': {
                        color: '#4fc3f7',
                    },
                },
            },
        },
        MuiListItemText: {
            styleOverrides: {
                primary: {
                    fontSize: 14,
                    fontWeight: baseTheme.typography.fontWeightMedium,
                },
            },
        },
        MuiListItemIcon: {
            styleOverrides: {
                root: {
                    color: 'inherit',
                    minWidth: 'auto',
                    marginRight: baseTheme.spacing(2),
                    '& svg': {
                        fontSize: 20,
                    },
                },
            },
        },
        MuiAvatar: {
            styleOverrides: {
                root: {
                    width: 32,
                    height: 32,
                },
            },
        },

        // @ts-ignore  (It's working but somehow not supported in this version)
        //MuiDataGrid: {
        //    styleOverrides: {
        //        root: {
        //            '& .MuiDataGrid-columnSeparator': {
        //                direction: 'ltr', background: 'blue',
        //            },
        //            '& .MuiDataGrid-columnHeaderTitle': {
        //                direction: 'ltr', background: 'blue',
        //            },
        //            '& .MuiDataGrid-columnsContainer': {
        //                direction: 'ltr', background: 'green',
        //            },
        //            '& .MuiDataGrid-virtualScroller': {
        //                direction: 'ltr', background: 'yellow',
        //            }
        //        }
        //    }
        //},
    },
};