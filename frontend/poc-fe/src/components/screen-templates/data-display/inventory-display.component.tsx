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
import { AppEntity, InventoryItem, OrderTag, orderTransportTypeDisplayMap } from '../../../types/crmTypes.types';
import moment from 'moment';
import { getColor } from '../../../util/screen-util';
import { AppClock } from '../../common/app-clock.component';
import { motion, AnimatePresence } from 'framer-motion';
import { useScreenProfilesContext } from '../../../hooks/useScreenProfilesContext';
import { useScreenInfoContext } from '../../../hooks/useScreenInfoContext';
import { flattenAppEntities } from '../../../util/global-util';

interface OrdersDisplayProps {
    inventoryItems: InventoryItem[];
}

const InventoryDisplay = ({ inventoryItems }: OrdersDisplayProps) => {
    const { entities, orderTags } = useScreenInfoContext();

    const flatEntities = useMemo(() => flattenAppEntities(entities), [entities]);
    const flatEntitiesMap = useMemo(() => flatEntities.reduce((acc, e) => ({ ...acc, [e.Id]: e }), {} as { [id: number]: AppEntity }), [flatEntities]);

    const orderTagsMap = useMemo(() => orderTags.reduce((acc, e) => ({ ...acc, [e.Id]: e }), {} as { [id: number]: OrderTag }), [orderTags]);
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

    // const fixedInventoryItems = useMemo(() => {
    //     return [...inventoryItems].sort((a, b) =>
    //         new Date(a.transportType === OrderTransportType.Incoming ? a.crmOrder.endDate : a.crmOrder.endDate).getTime() -
    //         new Date(b.transportType === OrderTransportType.Incoming ? b.crmOrder.endDate : b.crmOrder.endDate).getTime()
    //     );
    // }, [inventoryItems]);
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
                            <StyledTableCell>Department</StyledTableCell>
                            <StyledTableCell>Id</StyledTableCell>
                            <StyledTableCell>Type</StyledTableCell>
                            <StyledTableCell>Product</StyledTableCell>
                            <StyledTableCell>Amount</StyledTableCell>
                            <StyledTableCell>Bundle?</StyledTableCell>
                        </StyledTableRow>
                    </TableHead>

                    <TableBody>
                        <AnimatePresence>
                            {inventoryItems.map((item) => (
                                <StyledTableRow
                                    key={`${item.crmInventoryItem.id}_${item.transportType}`}
                                    layoutId={item.crmInventoryItem.id.toString()}
                                    initial={{ opacity: 0, scale: 1 }}
                                    animate={{ opacity: 1, scale: 1 }}
                                    exit={{ opacity: 1, maxHeight: 0 }}
                                    transition={{ duration: 1, type: 'spring' }}
                                >
                                    <StyledTableCell>{flatEntitiesMap[item.crmInventoryItem.departmentId]?.Name}</StyledTableCell>
                                    <StyledTableCell>{item.crmInventoryItem.id}</StyledTableCell>
                                    <StyledTableCell><span style={{ color: getColor(`transport_${item.transportType}`) }}>{orderTransportTypeDisplayMap[item.transportType]}</span></StyledTableCell>
                                    <StyledTableCell>{item.crmInventoryItem.productName}</StyledTableCell>
                                    <StyledTableCell>{item.crmInventoryItem.amount}</StyledTableCell>
                                    <StyledTableCell>{item.crmInventoryItem.isBundle ? '✔️' : ''}</StyledTableCell>
                                </StyledTableRow>
                            ))}
                        </AnimatePresence>

                    </TableBody>

                </Table>
            </TableContainer>

            <Divider sx={{ mt: 2 }} />
            <Box sx={{ justifyContent: 'space-between', mt: 2 }}>
                <Typography variant="body1">Total Inventory Items: {inventoryItems.length}</Typography>
                {/* <Typography variant="body1">Total Unique Orders: {uniqueOrders.length}</Typography> */}
            </Box>
        </Box>
    );
};

export default InventoryDisplay;
