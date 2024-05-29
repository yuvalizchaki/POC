// TODO: remove

import * as React from 'react';
import Box from '@mui/material/Box';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import Paper from '@mui/material/Paper';
import Divider from '@mui/material/Divider';
import { getColor, ordersData } from './demo_data';
import { Chip, Typography, lighten } from '@mui/material';
import { StyledTableCell, StyledTableRow } from './styled';

const ScreenDemoOrdersTemplate = () => {
    return (
        <Box sx={{ padding: 2 }}>
            <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                <Typography variant="body1">(Los Angeles) Today Orders (27/05)</Typography>
                <Box sx={{ flex: '1 0 0' }} />
                <Typography variant="body1">28/05/2024 - 11:30:05</Typography>
            </Box>
            <Divider />
            <TableContainer component={Paper} sx={{ mt: 2 }}>
                <Table sx={{ minWidth: 650 }} size="small" aria-label="order table">
                    <TableHead>
                        <StyledTableRow>
                            <StyledTableCell>Time</StyledTableCell>
                            <StyledTableCell>Id</StyledTableCell>
                            <StyledTableCell>Type</StyledTableCell>
                            <StyledTableCell>Status</StyledTableCell>
                            <StyledTableCell>Client Name</StyledTableCell>
                            <StyledTableCell>Address</StyledTableCell>
                        </StyledTableRow>
                    </TableHead>
                    <TableBody>
                        {ordersData.map((order) => (
                            <StyledTableRow
                                key={order.id}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 }, }}
                                style={order.highlight ? {
                                    backgroundColor: `${lighten('#fefe00', 0.5)} !important`
                                } : undefined}
                            >
                                <StyledTableCell component="th" scope="row">
                                    {order.time}
                                </StyledTableCell>
                                <StyledTableCell>{order.id}</StyledTableCell>
                                <StyledTableCell><span style={{ color: getColor(order.type) }}>{order.type} </span></StyledTableCell>
                                <StyledTableCell><Chip sx={{ width: '12ch', height: 50, backgroundColor: getColor(order.status), color: '#ffffff' }} label={order.status} /></StyledTableCell>
                                <StyledTableCell>{order.client_name}</StyledTableCell>
                                <StyledTableCell>{order.address}</StyledTableCell>
                            </StyledTableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
            <Divider sx={{ mt: 2 }} />
            <Box sx={{ justifyContent: 'space-between', mt: 2 }}>
                <Typography variant="body1">Total Orders: {ordersData.length}</Typography>
                <Typography variant="body1">Total Unique Orders: {ordersData.length}</Typography>
            </Box>
        </Box>
    );
}

export default ScreenDemoOrdersTemplate