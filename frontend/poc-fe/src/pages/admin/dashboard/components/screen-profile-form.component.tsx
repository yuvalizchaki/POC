import {
  Box,
  Checkbox,
  Chip,
  Divider,
  FormControlLabel,
  FormHelperText,
  Grid,
  MenuItem,
  Paper,
  Select,
  TextField,
  Typography,
} from "@mui/material";
import {
  Controller,
  ControllerRenderProps,
  useFormContext,
} from "react-hook-form";
import {
  DisplayTemplateType,
  ScreenProfileFormFields,
  TimeMode,
  TimeUnit,
  timeIncludeList,
  timeModeList,
  timeUnitList,
  timeUnitPluralList,
} from "../../../../types/screenProfile.types";
import React, { useEffect, useMemo } from "react";
import { useScreenProfilesContext } from "../../../../hooks/useScreenProfilesContext";
import {
  AppEntity,
  OrderTag,
  orderStatusList,
  orderStatusDisplayMap,
} from "../../../../types/crmTypes.types";
import { flattenAppEntities } from "../../../../util/global-util";

// #region    ======================================== ScreenPorfileForm ========================================
interface TimeRangePickerProps extends ControllerRenderProps {
  label?: string;
  error?: boolean;
  helperText?: string;
}
const TimeRangePicker = React.forwardRef<
  HTMLInputElement,
  TimeRangePickerProps
>(({ label, error, helperText, value, onChange }, ref) => {
  return (
    <>
      {label && <Typography variant="body1">{label}</Typography>}
      <Select
        value={value.mode}
        onChange={(e) => {
          const newMode = e.target.value as TimeMode;
          onChange({ ...value, mode: newMode });
        }}
        inputProps={{ sx: { pr: 0 } }}
        IconComponent={() => null}
        sx={{ height: 30 }}
      >
        {timeModeList.map((option) => (
          <MenuItem key={option.value} value={option.value}>
            {option.label}
          </MenuItem>
        ))}
      </Select>
      {value.mode === TimeMode.Fixed && (
        <>
          <Select
            value={value.amount >= 0 ? "+" : "-"}
            onChange={(e) => {
              const isAdd = e.target.value === "+";
              const newAmount = isAdd
                ? Math.abs(value.amount)
                : -Math.abs(value.amount);
              onChange({ ...value, amount: newAmount });
            }}
            inputProps={{ sx: { pr: 0 } }}
            IconComponent={() => null}
            sx={{ height: 30 }}
          >
            <MenuItem value="+">+</MenuItem>
            <MenuItem value="-">-</MenuItem>
          </Select>
          <TextField
            type="number"
            value={Math.abs(value.amount)}
            onChange={(e) => {
              const newAmount = parseInt(e.target.value, 10);
              const isAdd = value.amount >= 0;
              onChange({ ...value, amount: isAdd ? newAmount : -newAmount });
            }}
            inputProps={{ min: 0, max: 30 }}
            InputProps={{ sx: { height: 30 } }}
            sx={{ maxWidth: 100 }}
          />
        </>
      )}
      <Select
        value={value.unit}
        onChange={(e) => {
          const newUnit = e.target.value as TimeUnit;
          onChange({ ...value, unit: newUnit });
        }}
        slotProps={{ input: { sx: { pr: 0 } } }}
        IconComponent={() => null}
        sx={{ height: 30 }}
      >
        {(value.mode === TimeMode.Fixed
          ? timeUnitPluralList
          : timeUnitList
        ).map((option) => (
          <MenuItem key={option.value} value={option.value}>
            {option.label}
          </MenuItem>
        ))}
      </Select>
      {error && <FormHelperText error>{helperText}</FormHelperText>}
    </>
  );
});

// #endregion ======================================== ScreenPorfileForm ========================================

// #region    ======================================== ScreenPorfileForm ========================================

interface ScreenPorfileFormProps {}

export const ScreenPorfileForm = ({}: ScreenPorfileFormProps) => {
  const { entities, orderTags } = useScreenProfilesContext();

  const flatEntities = useMemo(() => flattenAppEntities(entities), [entities]);
  const flatEntitiesMap = useMemo(
    () =>
      flatEntities.reduce(
        (acc, e) => ({ ...acc, [e.Id]: e }),
        {} as { [id: number]: AppEntity }
      ),
    [flatEntities]
  );

  // const orderTagsMap = useMemo(
  //   () =>
  //     orderTags.reduce(
  //       (acc, e) => ({ ...acc, [e.Id]: e }),
  //       {} as { [id: number]: OrderTag }
  //     ),
  //   [orderTags]
  // );

  const {
    control,
    getValues,
    setValue,
    watch,
    formState: { errors },
  } = useFormContext<ScreenProfileFormFields>();
  // console.log('[DEBUG] errors: ', errors);

  const isPagingEnabled = watch(
    "screenProfileFiltering.displayConfig.isPaging"
  );

  /** Inventory Panel Conditional Rendering*/
  const displayTemplateValue = watch(
    "screenProfileFiltering.displayConfig.displayTemplate"
  );
  useEffect(() => {
    // console.log('[DEBUG] Display template changed to: ', displayTemplateValue);
    if (displayTemplateValue !== DisplayTemplateType.Inventory) {
      setValue("screenProfileFiltering.inventoryFiltering", undefined);
    }
  }, [displayTemplateValue]);

  // console.log('[DEBUG] Form Values: ', getValues());

  return (
    <>
      <Grid container spacing={2}>
        <Grid item xs={12}>
          <Controller
            name="name"
            control={control}
            rules={{
              required: {
                value: true,
                message: "Name is required",
              },
              maxLength: 20,
            }}
            render={({ field, fieldState }) => (
              <TextField
                {...field}
                label="Name"
                fullWidth
                error={!!errors.name}
                helperText={fieldState?.error?.message?.toString()}
              />
            )}
          />
        </Grid>
        <Grid item xs={12}>
          <Controller
            name="screenProfileFiltering.displayConfig.displayTemplate"
            control={control}
            render={({ field, fieldState }) => (
              <TextField
                select
                label="Display Type"
                fullWidth
                {...field}
                value={field.value}
                onChange={(v) => {
                  if (
                    Number(v.target.value) === DisplayTemplateType.Inventory
                  ) {
                    const inventoryFilteringValue = getValues(
                      "screenProfileFiltering.inventoryFiltering"
                    );
                    if (
                      inventoryFilteringValue === null ||
                      inventoryFilteringValue === undefined
                    ) {
                      setValue("screenProfileFiltering.inventoryFiltering", {});
                    }
                  } else {
                    setValue(
                      "screenProfileFiltering.inventoryFiltering",
                      undefined
                    );
                  }
                  field.onChange(v);
                }}
                error={
                  !!errors.screenProfileFiltering?.displayConfig
                    ?.displayTemplate
                }
                helperText={fieldState?.error?.message?.toString()}
              >
                <MenuItem value={DisplayTemplateType.Orders}>Orders</MenuItem>
                <MenuItem value={DisplayTemplateType.Inventory}>
                  Inventory
                </MenuItem>
              </TextField>
            )}
          />
        </Grid>
        <Grid item xs={12}>
          <Controller
            name="screenProfileFiltering.displayConfig.label"
            control={control}
            rules={{
              required: {
                value: true,
                message: "Display Label is required",
              },
              maxLength: 20,
            }}
            render={({ field, fieldState }) => (
              <TextField
                fullWidth
                {...field}
                label="Display Label"
                error={!!errors.name}
                helperText={fieldState?.error?.message?.toString()}
              />
            )}
          />
        </Grid>
        {/* <Grid item xs={12}>
          <Controller
            name="screenProfileFiltering.displayConfig.isDarkMode"
            control={control}
            render={({ field }) => (
              <FormControlLabel
                control={<Checkbox {...field} checked={field.value} />}
                label="Enable Dark Mode"
              />
            )}
          />
        </Grid> */}
        <Grid item xs={12}>
          <Controller
            name="screenProfileFiltering.displayConfig.isPaging"
            control={control}
            render={({ field }) => (
              <FormControlLabel
                control={<Checkbox {...field} checked={field.value} />}
                label="Cycle Screens"
              />
            )}
          />
        </Grid>
        {isPagingEnabled && (
          <Grid item xs={12}>
            <Controller
              name="screenProfileFiltering.displayConfig.pagingRefreshTime"
              control={control}
              render={({ field }) => (
                <TextField
                  label="Screen Cycle Time"
                  fullWidth
                  select
                  {...field}
                >
                  <MenuItem value={5000}>5 seconds</MenuItem>
                  <MenuItem value={15000}>15 seconds</MenuItem>
                  <MenuItem value={30000}>30 seconds</MenuItem>
                  <MenuItem value={60000}>1 minute</MenuItem>
                </TextField>
              )}
            />
          </Grid>
        )}

        {/* #region    ================================================== Orders Filters ================================================== */}
        <Grid item xs={12}>
          <Divider>
            <Chip label="Order Filters" />
          </Divider>
        </Grid>
        <Grid item xs={12}>
          <Paper
            variant="outlined"
            sx={{
              display: "flex",
              flexDirection: "row",
              gap: 1,
              p: 2,
              boxSizing: "content-box",
              alignItems: "center",
              flexWrap: "wrap",
              overflow: "auto",
            }}
          >
            <Typography variant="body1">Show</Typography>
            <Controller
              name="screenProfileFiltering.orderFiltering.timeRanges.include"
              control={control}
              render={({ field }) => (
                <Select
                  {...field}
                  slotProps={{ input: { sx: { pr: 0 } } }}
                  IconComponent={() => null}
                  sx={{ height: 30 }}
                >
                  {timeIncludeList.map((option) => (
                    <MenuItem key={option.value} value={option.value}>
                      {option.label}
                    </MenuItem>
                  ))}
                </Select>
              )}
            />
            <Typography variant="body1">orders</Typography>
            <Controller
              name="screenProfileFiltering.orderFiltering.timeRanges.from"
              control={control}
              render={({ field, fieldState }) => (
                <TimeRangePicker
                  label="from"
                  {...field}
                  error={
                    !!errors.screenProfileFiltering?.orderFiltering?.timeRanges
                      ?.from
                  }
                  helperText={fieldState?.error?.message?.toString()}
                />
              )}
            />
            <Controller
              name="screenProfileFiltering.orderFiltering.timeRanges.to"
              control={control}
              render={({ field, fieldState }) => (
                <TimeRangePicker
                  label="to"
                  {...field}
                  error={
                    !!errors.screenProfileFiltering?.orderFiltering?.timeRanges
                      ?.to
                  }
                  helperText={fieldState?.error?.message?.toString()}
                />
              )}
            />
          </Paper>
        </Grid>
        <Grid item xs={12}>
          <Controller
            name="screenProfileFiltering.orderFiltering.orderStatuses"
            control={control}
            render={({ field, fieldState }) => (
              <TextField
                label="Order Statuses"
                select
                SelectProps={{
                  multiple: true,
                  renderValue: (selectedStatuses: unknown) => (
                    <Box sx={{ display: "flex", flexWrap: "wrap", gap: 0.5 }}>
                      {(selectedStatuses as number[]).map((status) => (
                        <Chip
                          key={status}
                          label={
                            orderStatusDisplayMap[
                              status as keyof typeof orderStatusDisplayMap
                            ] ?? "???"
                          }
                        />
                      ))}
                    </Box>
                  ),
                }}
                fullWidth
                {...field}
                value={field.value ?? []}
                error={
                  !!errors.screenProfileFiltering?.orderFiltering?.orderStatuses
                }
                helperText={fieldState?.error?.message?.toString()}
              >
                {orderStatusList.map((status) => (
                  <MenuItem key={status.value} value={status.value}>
                    {status.label}
                  </MenuItem>
                ))}
              </TextField>
            )}
          />
        </Grid>
        <Grid item xs={12}>
          <Controller
            name="screenProfileFiltering.orderFiltering.entityIds"
            control={control}
            render={({ field, fieldState }) => (
              <TextField
                label="Order Entities"
                select
                SelectProps={{
                  multiple: true,
                  renderValue: (entityIds: unknown) => (
                    <Box sx={{ display: "flex", flexWrap: "wrap", gap: 0.5 }}>
                      {(entityIds as number[]).map((id) => (
                        <Chip
                          key={id}
                          label={flatEntitiesMap[id]?.Name ?? "???"}
                        />
                      ))}
                    </Box>
                  ),
                }}
                fullWidth
                {...field}
                value={field.value ?? []}
                error={
                  !!errors.screenProfileFiltering?.orderFiltering?.entityIds
                }
                helperText={fieldState?.error?.message?.toString()}
              >
                {flatEntities.map((e) => (
                  <MenuItem key={e.Id} value={e.Id}>
                    {e.Name}
                  </MenuItem>
                ))}
              </TextField>
            )}
          />
        </Grid>
        {/* <Grid item xs={12}>
          <Controller
            name="screenProfileFiltering.orderFiltering.tags"
            control={control}
            render={({ field, fieldState }) => (
              <TextField
                label="Order Tags"
                select
                SelectProps={{
                  multiple: true,
                  renderValue: (tagIds: unknown) => (
                    <Box sx={{ display: "flex", flexWrap: "wrap", gap: 0.5 }}>
                      {(tagIds as number[]).map((id) => (
                        <Chip
                          key={id}
                          label={orderTagsMap[id]?.Name ?? "???"}
                        />
                      ))}
                    </Box>
                  ),
                }}
                fullWidth
                {...field}
                value={field.value ?? []}
                error={!!errors.screenProfileFiltering?.orderFiltering?.tags}
                helperText={fieldState?.error?.message?.toString()}
              >
                {orderTags.map((e) => (
                  <MenuItem key={e.Id} value={e.Id}>
                    {e.Name}
                  </MenuItem>
                ))}
              </TextField>
            )}
          />
        </Grid> */}
        {/* #endregion ================================================== Orders Filters ================================================== */}
        {/* #region    ================================================== Inventory Filters ================================================== */}
        {displayTemplateValue === DisplayTemplateType.Inventory && (
          <>
            <Grid item xs={12}>
              <Divider>
                <Chip label="Inventory Filters" />
              </Divider>
            </Grid>
            <Grid item xs={12}>
              <Controller
                name="screenProfileFiltering.inventoryFiltering.entityIds"
                control={control}
                render={({ field, fieldState }) => (
                  <TextField
                    label="Inventory Entities"
                    select
                    SelectProps={{
                      multiple: true,
                      renderValue: (entityIds: unknown) => (
                        <Box
                          sx={{ display: "flex", flexWrap: "wrap", gap: 0.5 }}
                        >
                          {(entityIds as number[]).map((id) => (
                            <Chip
                              key={id}
                              label={flatEntitiesMap[id]?.Name ?? "???"}
                            />
                          ))}
                        </Box>
                      ),
                    }}
                    fullWidth
                    {...field}
                    value={field.value ?? []}
                    error={
                      !!errors.screenProfileFiltering?.inventoryFiltering
                        ?.entityIds
                    }
                    helperText={fieldState?.error?.message?.toString()}
                  >
                    {flatEntities.map((e) => (
                      <MenuItem key={e.Id} value={e.Id}>
                        {e.Name}
                      </MenuItem>
                    ))}
                  </TextField>
                )}
              />
            </Grid>
          </>
        )}
        {/* #endregion ================================================== Inventory Filters ================================================== */}
      </Grid>
    </>
  );
};
// #endregion ======================================== ScreenPorfileForm ========================================
