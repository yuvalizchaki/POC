import React, { useEffect, useMemo, useState } from 'react';
import Box from '@mui/material/Box';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import Paper from '@mui/material/Paper';
import Divider from '@mui/material/Divider';
import { Chip, Typography, lighten } from '@mui/material';
import { StyledTableCell, StyledTableRow } from './styled';
import { OrderDto, OrderStatus, OrderTransportType, orderStatusDisplayMap, orderTransportTypeDisplayMap } from '../../../types/crmTypes.types';
import moment from 'moment';
import { getColor } from '../../../util/screen-util';
import { AppClock } from '../../common/app-clock.component';
import { motion, AnimatePresence } from 'framer-motion';

interface OrdersDisplayProps {
    orders: OrderDto[];
}

const OrdersDisplay = ({ orders }: OrdersDisplayProps) => {
    // DEBUG TESTS
    // const [mask, setMask] = useState<boolean[]>(Array(fixedOrders.length).fill(false));

    // useEffect(() => {
    //     const interval = setInterval(() => {
    //         setMask((prevMask) => {
    //             // Flip a random index in the mask
    //             const flipIndex = Math.floor(Math.random() * prevMask.length);
    //             const newMask = [...prevMask];
    //             newMask[flipIndex] = !newMask[flipIndex];
    //             return newMask;
    //         });
    //     }, 2000);

    //     return () => clearInterval(interval);
    // }, [fixedOrders]);

    // const randomSubset = useMemo(() => {
    //     return fixedOrders.filter((_, index) => mask[index])
    // }, [mask, fixedOrders]);

    const fixedOrders = useMemo(() => {
        return [...orders].sort((a, b) =>
            new Date(a.transportType === OrderTransportType.Incoming ? a.crmOrder.endDate : a.crmOrder.endDate).getTime() -
            new Date(b.transportType === OrderTransportType.Incoming ? b.crmOrder.endDate : b.crmOrder.endDate).getTime()
        );
    }, [orders]);

    const uniqueOrders = useMemo(() => [...new Map(fixedOrders.map(order => [order.crmOrder.id, order])).values()], [fixedOrders]);
    // console.log('[DEBUG] UPDATE!');
    return (
        <Box sx={{ p: 2, height: '100%', boxSizing: 'border-box', display: 'flex', flexDirection: 'column' }}>
            <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                <Typography variant="body1">SHOW LABEL</Typography>
                <Box sx={{ flex: '1 0 0' }} />
                <AppClock format='DD/MM/YYYY hh:mm:ss' />
            </Box>
            <Divider />
            <TableContainer variant="outlined" component={Paper} sx={{ mt: 2, flex: 1 }}>
                <Table sx={{ minWidth: 650, overflow: 'hidden' }} size="small">
                    <TableHead>
                        <StyledTableRow>
                            <StyledTableCell>Time</StyledTableCell>
                            <StyledTableCell>Id</StyledTableCell>
                            <StyledTableCell>Type</StyledTableCell>
                            <StyledTableCell>Status</StyledTableCell>
                            <StyledTableCell>Client Name</StyledTableCell>
                        </StyledTableRow>
                    </TableHead>
                    <TableBody>
                        <AnimatePresence mode="sync">
                            {fixedOrders.map((order) => (
                                <StyledTableRow
                                    key={`${order.crmOrder.id}_${order.transportType}`}
                                    layoutId={`${order.crmOrder.id}_${order.transportType}`}
                                    initial={{ opacity: 0 }}
                                    animate={{ opacity: 1, x: 0 }}
                                    exit={{ opacity: 0, x: 100 }}
                                    transition={{ duration: 0.8, type: "spring" }}
                                    sx={{ opacity: 1 }}
                                >
                                    <StyledTableCell component="th" scope="row">
                                        {moment(order.transportType === OrderTransportType.Incoming ? order.crmOrder.endDate : order.crmOrder.endDate).format('DD/MM/YYYY hh:mm')}
                                    </StyledTableCell>
                                    <StyledTableCell>{order.crmOrder.id}</StyledTableCell>
                                    <StyledTableCell><span style={{ color: getColor(`transport_${order.transportType}`) }}>{orderTransportTypeDisplayMap[order.transportType]}</span></StyledTableCell>
                                    <StyledTableCell>
                                        <Chip
                                            sx={{ width: '12ch', height: 50, backgroundColor: getColor(`status_${order.crmOrder.status}`), color: '#ffffff' }}
                                            label={orderStatusDisplayMap[order.crmOrder.status]}
                                        />
                                    </StyledTableCell>
                                    <StyledTableCell>{order.crmOrder.clientName}</StyledTableCell>
                                </StyledTableRow>
                            ))}
                        </AnimatePresence>
                    </TableBody>
                </Table>
            </TableContainer>

            <Divider sx={{ mt: 2 }} />
            <Box sx={{ justifyContent: 'space-between', mt: 2 }}>
                <Typography variant="body1">Total Orders: {fixedOrders.length}</Typography>
                <Typography variant="body1">Total Unique Orders: {uniqueOrders.length}</Typography>
            </Box>
        </Box>
    );
};

export default OrdersDisplay;
