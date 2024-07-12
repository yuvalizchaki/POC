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
import { OrderDto, OrderStatus, orderStatusDisplayMap } from '../../../types/crmTypes.types';
import moment from 'moment';
import { getColor } from '../../../util/screen-util';
import { AppClock } from '../../common/app-clock.component';
import { motion, AnimatePresence } from 'framer-motion';

interface OrdersDisplayProps {
    orders: OrderDto[];
}

interface FixedOrderDto extends OrderDto {
    type: 'incoming' | 'outgoing';
}

const OrdersDisplay = ({ orders }: OrdersDisplayProps) => {
    const fixedOrders: FixedOrderDto[] = useMemo(() => orders.map(o => ({
        ...o,
        type: 'incoming'
    })), [orders]);

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
                    <AnimatePresence>
                            {fixedOrders.map((order) => (
                                <StyledTableRow
                                    key={order.id}
                                    layoutId={order.id.toString()}
                                    initial={{ opacity: 0, scale: 1 }}
                                    animate={{ opacity: 1, scale: 1 }}
                                    exit={{ opacity: 1, maxHeight: 0 }}
                                    transition={{ duration: 1, type: 'spring' }}
                                >
                                    <StyledTableCell component="th" scope="row">
                                        {moment(order.type === 'incoming' ? order.endDate : order.endDate).format('DD/MM/YYYY hh:mm')}
                                    </StyledTableCell>
                                    <StyledTableCell>{order.id}</StyledTableCell>
                                    <StyledTableCell><span style={{ color: getColor(order.type) }}>{order.type}</span></StyledTableCell>
                                    <StyledTableCell>
                                        <Chip
                                            sx={{ width: '12ch', height: 50, backgroundColor: getColor(`status_${order.status}`), color: '#ffffff' }}
                                            label={orderStatusDisplayMap[order.status]}
                                        />
                                    </StyledTableCell>
                                    <StyledTableCell>{order.clientName}</StyledTableCell>
                                </StyledTableRow>
                            ))}
                                                </AnimatePresence>

                    </TableBody>

                </Table>
            </TableContainer>

            <Divider sx={{ mt: 2 }} />
            <Box sx={{ justifyContent: 'space-between', mt: 2 }}>
                <Typography variant="body1">Total Orders: {fixedOrders.length}</Typography>
                <Typography variant="body1">Total Unique Orders: {orders.length}</Typography>
            </Box>
        </Box>
    );
};

export default OrdersDisplay;
