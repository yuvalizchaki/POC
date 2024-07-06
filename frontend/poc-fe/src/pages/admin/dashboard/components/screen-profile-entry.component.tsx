import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Divider,
  IconButton,
  Stack,
  Typography,
  Box,
  FormHelperText,
} from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import DeleteIcon from "@mui/icons-material/Delete";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import React, { useState } from "react";
import { useForm, Controller } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import { ScreenProfile } from "../../../../types/screenProfile.types";
import { ScreenComponent } from "./screen.component";
import { useAdminInfoContext } from "../../../../hooks/useAdminInfoContext";
import { useScreenProfilesContext } from "../../../../hooks/useScreenProfilesContext";
import { MuiOtpInput } from "mui-one-time-password-input";
import { AxiosError } from "axios";

const CODE_LENGTH = 6;

interface ScreenProfileEntryProps {
  profile: ScreenProfile;
}

const schema = z.object({
  code: z.string().length(CODE_LENGTH, "OTP must be exactly 6 characters"),
});

type FormValues = z.infer<typeof schema>;

export const ScreenProfileEntry = ({ profile }: ScreenProfileEntryProps) => {
  const [isExpanded, setIsExpanded] = useState<boolean>(false);
  const [addScreenOpen, setAddScreenOpen] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const { pairScreen, deleteScreenProfile } = useAdminInfoContext();
  const { refetch } = useScreenProfilesContext();
  const { control, handleSubmit, reset } = useForm<FormValues>({
    resolver: zodResolver(schema),
    defaultValues: {
      code: "",
    },
  });

  const handleAddScreenOpen = () => {
    setAddScreenOpen(true);
    setIsExpanded(true);
  };

  const handleAddScreenClose = () => {
    setAddScreenOpen(false);
    reset();
    setError(null);
  };

  const handleAddScreen = async ({ code }: FormValues) => {
    setIsLoading(true);
    setError(null);
    try {
      await pairScreen(code, profile.id);
      refetch();
      handleAddScreenClose();
    } catch (err) {
      if (err instanceof AxiosError && err.response?.data?.errors?.error) {
        setError(err.response.data.errors.error);
      } else {
        setError("Unknown Error");
      }
    } finally {
      setIsLoading(false);
    }
  };

  const handleDeleteScreenProfile = (id: number) => {
    deleteScreenProfile(id).then(() => {
      refetch();
    });
  };

  return (
    <React.Fragment key={profile.id}>
      <Accordion
        expanded={isExpanded}
        onChange={() => setIsExpanded((v) => !v)}
        variant="outlined"
        sx={{ borderRadius: 2 }}
        onClick={(e: React.MouseEvent<HTMLDivElement>) => {
          if (e.target !== e.currentTarget) {
            e.preventDefault();
          }
        }}
      >
        <AccordionSummary
          expandIcon={<ExpandMoreIcon />}
          sx={{
            [`.MuiAccordionSummary-expandIconWrapper:not(.Mui-expanded)`]: {
              transform: "rotate(90deg)",
            },
            [`.MuiAccordionSummary-expandIconWrapper.Mui-expanded`]: {
              transform: "rotate(0deg)",
            },
          }}
        >
          <Stack direction="row" alignItems="center" width="100%">
            <Typography>{profile.name}</Typography>
            <div style={{ flex: "1 0 0" }} />
            <Button
              size="small"
              variant="text"
              startIcon={<AddIcon />}
              onClick={(e: React.MouseEvent<HTMLButtonElement>) => {
                e.stopPropagation();
                handleAddScreenOpen();
              }}
            >
              Add Screen
            </Button>
            <IconButton
              color="error"
              size="small"
              onClick={(e: React.MouseEvent<HTMLButtonElement>) => {
                e.stopPropagation();
                handleDeleteScreenProfile(profile.id);
              }}
            >
              <DeleteIcon />
            </IconButton>
          </Stack>
        </AccordionSummary>
        <AccordionDetails>
          <Stack direction="column" spacing={1}>
            {profile.screens.map((s, i) => (
              <React.Fragment key={s.id}>
                {i > 0 && <Divider />}
                <ScreenComponent screen={s} />
              </React.Fragment>
            ))}
          </Stack>
        </AccordionDetails>
      </Accordion>
      <Dialog
        open={addScreenOpen}
        onClose={() => {
          if (!isLoading) {
            handleAddScreenClose();
          }
        }}
      >
        <DialogTitle id="responsive-dialog-title">Enter Pair Code</DialogTitle>
        <DialogContent>
          <form onSubmit={handleSubmit(handleAddScreen)}>
            <Controller
              name="code"
              control={control}
              render={({ field, fieldState }) => (
                <Box>
                  <MuiOtpInput
                    sx={{ gap: 1 }}
                    {...field}
                    length={CODE_LENGTH}
                    TextFieldsProps={{
                      disabled: isLoading,
                    }}
                  />
                  {fieldState.error && (
                    <FormHelperText error>
                      {fieldState.error.message}
                    </FormHelperText>
                  )}
                </Box>
              )}
            />
            {(error || isLoading) && (
              <FormHelperText error={!!error}>
                {error ? error : isLoading ? "Loading..." : null}
              </FormHelperText>
            )}
            <DialogActions sx={{ display: "flex" }}>
              <div style={{ flex: "1 0 0" }} />
              <Button
                disableElevation
                color="error"
                variant="text"
                onClick={handleAddScreenClose}
                disabled={isLoading}
              >
                Cancel
              </Button>
              <Button
                type="submit"
                disableElevation
                variant="contained"
                sx={{ ml: 2 }}
                disabled={isLoading}
              >
                Save
              </Button>
            </DialogActions>
          </form>
        </DialogContent>
      </Dialog>
    </React.Fragment>
  );
};
