import { useMemo } from "react";
import Box from "@mui/material/Box";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import Paper from "@mui/material/Paper";
import Divider from "@mui/material/Divider";
import { Chip, Typography } from "@mui/material";
import { StyledTableCell, StyledTableRow } from "./styled";
import {
  OrderDto,
  OrderTransportType,
  orderStatusDisplayMap,
  orderTransportTypeDisplayMap,
} from "../../../types/crmTypes.types";
import moment from "moment";
import { getColor } from "../../../util/screen-util";
import { AppClock } from "../../common/app-clock.component";
import { AnimatePresence } from "framer-motion";
import { useScreenInfoContext } from "../../../hooks/useScreenInfoContext";

interface OrdersDisplayProps {
  orders: OrderDto[];
  isPaging: boolean;
  currentPage: number;
  pageSize: number;
}

const OrdersDisplay = ({
  orders,
  isPaging,
  currentPage,
  pageSize,
}: OrdersDisplayProps) => {
  const { screenInfo } = useScreenInfoContext();
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

  const uniqueOrders = useMemo(
    () => [
      ...new Map(orders.map((order) => [order.crmOrder.id, order])).values(),
    ],
    [orders]
  );
  // console.log('[DEBUG] UPDATE!');

  const pagedOrders = useMemo(() => {
    if (isPaging) {
      const start = currentPage * pageSize;
      const end = start + pageSize;
      return orders.slice(start, end);
    }
    return orders;
  }, [orders, isPaging, pageSize, currentPage]);

  return (
    <Box
      sx={{
        p: 2,
        height: "100%",
        boxSizing: "border-box",
        display: "flex",
        flexDirection: "column",
      }}
    >
      <Box sx={{ display: "flex", alignItems: "center", mb: 2 }}>
        {screenInfo && (
          <Typography variant="body1">
            {screenInfo.displayConfig.label}
          </Typography>
        )}
        <Box sx={{ flex: "1 0 0" }} />
        <AppClock format="DD/MM/YYYY hh:mm:ss" />
      </Box>
      <Divider />
      <TableContainer
        variant="outlined"
        component={Paper}
        sx={{ mt: 2, flex: 1 }}
      >
        <Table sx={{ minWidth: 650, overflow: "hidden" }} size="small">
          <TableHead sx={{ position: "sticky" }}>
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
              {pagedOrders.map((order) => (
                <StyledTableRow
                  key={`${order.crmOrder.id}_${order.transportType}`}
                  layoutId={`${order.crmOrder.id}_${order.transportType}`}
                  initial={{ opacity: 0 }}
                  animate={{ opacity: 1, x: 0 }}
                  exit={{ opacity: 0, x: 50 }}
                  transition={{ duration: 0.5, type: "spring" }}
                  sx={{ opacity: 1 }}
                >
                  <StyledTableCell component="th" scope="row">
                    {moment(
                      order.transportType === OrderTransportType.Incoming
                        ? order.crmOrder.endDate
                        : order.crmOrder.endDate
                    ).format("DD/MM/YYYY hh:mm")}
                  </StyledTableCell>
                  <StyledTableCell>{order.crmOrder.id}</StyledTableCell>
                  <StyledTableCell>
                    <span
                      style={{
                        color: getColor(`transport_${order.transportType}`),
                      }}
                    >
                      {orderTransportTypeDisplayMap[order.transportType]}
                    </span>
                  </StyledTableCell>
                  <StyledTableCell>
                    <Chip
                      sx={{
                        width: "12ch",
                        height: 50,
                        backgroundColor: getColor(
                          `status_${order.crmOrder.status}`
                        ),
                        color: "#ffffff",
                      }}
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
      {isPaging && (
        <Typography variant="caption">{`Showing orders ${
          currentPage * pageSize + 1
        } - ${Math.min((currentPage + 1) * pageSize, orders.length)} out of ${
          orders.length
        }`}</Typography>
      )}
      <Divider sx={{ mt: 2 }} />
      <Box sx={{ justifyContent: "space-between", mt: 2 }}>
        <Typography variant="body1">Total Orders: {orders.length}</Typography>
        <Typography variant="body1">
          Total Unique Orders: {uniqueOrders.length}
        </Typography>
      </Box>
    </Box>
  );
};

export default OrdersDisplay;
