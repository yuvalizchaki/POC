import { Box, Grid, TextField } from "@mui/material";
import { Controller, ControllerRenderProps, useFormContext } from "react-hook-form";
import { ScreenProfileFormFields } from "../../../../types/screenProfile.types";
import React from "react";

// #region    ======================================== ScreenPorfileForm ========================================
interface TimeRangePickerProps extends ControllerRenderProps {
  error?: boolean
  helperText?: string
}
const TimeRangePicker = React.forwardRef<HTMLInputElement, TimeRangePickerProps>(({ }, ref) => {
  return <Box ref={ref}>TODO IMPLEMENT</Box>
});





// #endregion ======================================== ScreenPorfileForm ========================================

// #region    ======================================== ScreenPorfileForm ========================================

interface ScreenPorfileFormProps {
}

export const ScreenPorfileForm = ({ }: ScreenPorfileFormProps) => {
  const { control, formState: { errors } } = useFormContext<ScreenProfileFormFields>();
  console.log('[DEBUG] errors: ', errors);
  return (
    <>
      <Grid container spacing={2}>
        <Grid item xs={12}>
          <Controller
            name="name"
            control={control}
            rules={{
              required: true,
              maxLength: 20
            }}
            render={({ field }) => (
              <TextField
                {...field}
                label="Name"
                fullWidth
                error={!!errors.name}
                helperText={errors.name?.message?.toString()}
              />
            )}
          />
        </Grid>
        <Grid item xs={12}>
          <Controller
            name="name"
            control={control}
            render={({ field }) => (
              <TimeRangePicker
                {...field}
                error={!!errors.name}
                helperText={errors.name?.message?.toString()}
              />
            )}
          />
        </Grid>
      </Grid>
    </>
  );
};
// #endregion ======================================== ScreenPorfileForm ========================================