import { TableCell, TableRow, styled, tableCellClasses } from "@mui/material";
import { motion } from 'framer-motion';

export const StyledTableCell = styled(TableCell)(() => ({
    [`&.${tableCellClasses.head}`]: {
        backgroundColor: "#bfbfbf",
        fontWeight: "bold",
        // color: theme.palette.common.white,
    },
    // [`&.${tableCellClasses.body}`]: {
    //     fontSize: 14,
    // },
}));

export const StyledTableRow = styled(motion(TableRow))(({ theme }) => ({
    '&:nth-of-type(odd)': {
        backgroundColor: "#eeeeee",
    },
    // hide last border
    '&:last-child td, &:last-child th': {
        border: 0,
    }
}));