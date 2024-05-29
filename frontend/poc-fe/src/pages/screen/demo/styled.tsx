import { TableCell, TableRow, styled, tableCellClasses } from "@mui/material";

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

export const StyledTableRow = styled(TableRow)(() => ({
    '&:nth-of-type(odd)': {
        backgroundColor: "#eeeeee",
    },
    // hide last border
    '&:last-child td, &:last-child th': {
        border: 0,
    }
}));