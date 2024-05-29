import * as React from 'react';
import Box from '@mui/material/Box';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import Paper from '@mui/material/Paper';
import Divider from '@mui/material/Divider';
import { getColor, inventoryData } from './demo_data';
import { Typography, lighten } from '@mui/material';
import { StyledTableCell, StyledTableRow } from './styled';

const ScreenDemoInventoryTemplate = () => {
    return (
        <Box sx={{ padding: 2 }}>
            <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                <Typography variant="body1">(Los Angeles) Inventory Status (28/05)</Typography>
                <Box sx={{ flex: '1 0 0' }} />
                <Typography variant="body1">28/05/2024 - 11:30:05</Typography>
            </Box>
            <Divider />
            <TableContainer component={Paper} sx={{ mt: 2 }}>
                <Table sx={{ minWidth: 650 }} size="small" aria-label="inventory table">
                    <TableHead>
                        <StyledTableRow>
                            <StyledTableCell>Id</StyledTableCell>
                            <StyledTableCell>Type</StyledTableCell>
                            <StyledTableCell>Department</StyledTableCell>
                            <StyledTableCell>Product</StyledTableCell>
                            <StyledTableCell>Amount</StyledTableCell>
                            <StyledTableCell>Bundle?</StyledTableCell>
                            <StyledTableCell>Orders</StyledTableCell>
                        </StyledTableRow>
                    </TableHead>
                    <TableBody>
                        {inventoryData.map((item) => (
                            <StyledTableRow
                                key={item.id}
                                sx={{
                                    '&:last-child td, &:last-child th': { border: 0 },
                                    backgroundColor: item.highlight ? lighten('#fefe00', 0.5) : 'inherit'
                                }}
                            >
                                <StyledTableCell>{item.id}</StyledTableCell>
                                <StyledTableCell><span style={{ color: getColor(item.type) }}>{item.type} </span></StyledTableCell>
                                <StyledTableCell>{item.department}</StyledTableCell>
                                <StyledTableCell>{item.productName}</StyledTableCell>
                                <StyledTableCell>{item.amount}</StyledTableCell>
                                <StyledTableCell>{item.isBundle ? 'Yes' : 'No'}</StyledTableCell>
                                <StyledTableCell>{item.orders}</StyledTableCell>
                            </StyledTableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
            <Divider sx={{ mt: 2 }} />
            <Box sx={{ justifyContent: 'space-between', mt: 2 }}>
                <Typography variant="body1">Total Inventory Items: {inventoryData.length}</Typography>
                <Typography variant="body1">Total Unique Items: {inventoryData.length}</Typography>
            </Box>
        </Box>
    );
}

export default ScreenDemoInventoryTemplate;
