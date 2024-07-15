import { useMemo } from "react";
import Box from "@mui/material/Box";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import Paper from "@mui/material/Paper";
import Divider from "@mui/material/Divider";
import { Typography } from "@mui/material";
import { StyledTableCell, StyledTableRow } from "./styled";
import {
  AppEntity,
  InventoryItem,
  orderTransportTypeDisplayMap,
} from "../../../types/crmTypes.types";
import { getColor } from "../../../util/screen-util";
import { AppClock } from "../../common/app-clock.component";
import { AnimatePresence } from "framer-motion";
import { useScreenInfoContext } from "../../../hooks/useScreenInfoContext";
import { flattenAppEntities } from "../../../util/global-util";

interface OrdersDisplayProps {
  inventoryItems: InventoryItem[];
  isPaging: boolean;
  currentPage: number;
  pageSize: number;
}

const InventoryDisplay = ({
  inventoryItems,
  isPaging,
  currentPage,
  pageSize,
}: OrdersDisplayProps) => {
  const { screenInfo, entities } = useScreenInfoContext();

  const flatEntities = useMemo(() => flattenAppEntities(entities), [entities]);
  const flatEntitiesMap = useMemo(
    () =>
      flatEntities.reduce(
        (acc, e) => ({ ...acc, [e.Id]: e }),
        {} as { [id: number]: AppEntity }
      ),
    [flatEntities]
  );

  // const orderTagsMap = useMemo(() => orderTags.reduce((acc, e) => ({ ...acc, [e.Id]: e }), {} as { [id: number]: OrderTag }), [orderTags]);
  // DEBUG TESTS
  // const [mask, setMask] = useState<boolean[]>(Array(fixedOrders.length).fill(false));

  const pagedItems = useMemo(() => {
    if (isPaging) {
      const start = currentPage * pageSize;
      const end = start + pageSize;
      return inventoryItems.slice(start, end);
    }
    return inventoryItems;
  }, [inventoryItems, isPaging, pageSize, currentPage]);

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

          <AnimatePresence>
            <TableBody>
              {pagedItems.map((item) => (
                <StyledTableRow
                  key={`${item.crmInventoryItem.id}_${item.transportType}`}
                  layoutId={item.crmInventoryItem.id.toString()}
                  initial={{ opacity: 0 }}
                  animate={{ opacity: 1, x: 0 }}
                  exit={{ opacity: 0, x: 50 }}
                  transition={{ duration: 0.5, type: "spring" }}
                  sx={{ opacity: 1 }}
                >
                  <StyledTableCell>
                    {flatEntitiesMap[item.crmInventoryItem.departmentId]?.Name}
                  </StyledTableCell>
                  <StyledTableCell>{item.crmInventoryItem.id}</StyledTableCell>
                  <StyledTableCell>
                    <span
                      style={{
                        color: getColor(`transport_${item.transportType}`),
                      }}
                    >
                      {orderTransportTypeDisplayMap[item.transportType]}
                    </span>
                  </StyledTableCell>
                  <StyledTableCell>
                    {item.crmInventoryItem.productName}
                  </StyledTableCell>
                  <StyledTableCell>
                    {item.crmInventoryItem.amount}
                  </StyledTableCell>
                  <StyledTableCell>
                    {item.crmInventoryItem.isBundle ? "✔️" : ""}
                  </StyledTableCell>
                </StyledTableRow>
              ))}
            </TableBody>
          </AnimatePresence>
        </Table>
      </TableContainer>
      {isPaging && (
        <Typography variant="caption">{`Showing items ${
          currentPage * pageSize + 1
        } - ${Math.min(
          (currentPage + 1) * pageSize,
          inventoryItems.length
        )} out of ${inventoryItems.length}`}</Typography>
      )}
      <Divider sx={{ mt: 2 }} />
      <Box sx={{ justifyContent: "space-between", mt: 2 }}>
        <Typography variant="body1">
          Total Inventory Items: {inventoryItems.length}
        </Typography>
        {/* <Typography variant="body1">Total Unique Orders: {uniqueOrders.length}</Typography> */}
      </Box>
    </Box>
  );
};

export default InventoryDisplay;
