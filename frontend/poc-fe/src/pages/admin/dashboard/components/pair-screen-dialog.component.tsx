import {
    Box,
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    FormHelperText,
    Grid,
    TextField,
} from "@mui/material";
import { useState } from "react";
import { useForm, Controller } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import { useAdminInfoContext } from "../../../../hooks/useAdminInfoContext";
import { MuiOtpInput } from "mui-one-time-password-input";
import { AxiosError } from "axios";
import { PairScreenDto } from "../../../../types/screenProfile.types";

const CODE_LENGTH = 6;

const schema = z.object({
    name: z.string().min(1),
    pairingCode: z.string().length(CODE_LENGTH, "OTP must be exactly 6 characters"),
    screenProfileId: z.number().gt(0)
});

interface PairScreenDialogProps {
    isOpen: boolean;
    onSubmitted: () => void;
    onCancel: () => void;
    profileId: number;
}

export const PairScreenDialog = ({
    isOpen,
    onSubmitted,
    onCancel,
    profileId,
}: PairScreenDialogProps) => {
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const { pairScreen } = useAdminInfoContext();
    const { control, handleSubmit, reset } = useForm<PairScreenDto>({
        resolver: zodResolver(schema),
        defaultValues: {
            name: "",
            pairingCode: "",
            screenProfileId: profileId
        },
    });

    const handleAddScreen = async (data: PairScreenDto) => {
        setIsLoading(true);
        setError(null);
        try {
            await pairScreen(data);
            onSubmitted();
            reset();
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

    const handleDialogClose = () => {
        if (!isLoading) {
            reset();
            setError(null);
            onCancel();
        }
    };

    return (
        <Dialog open={isOpen} onClose={handleDialogClose}>
            <DialogTitle id="responsive-dialog-title">Enter Pair Code</DialogTitle>
            <DialogContent>
                <Box component="form" onSubmit={handleSubmit(handleAddScreen)} sx={{ mt: 2 }}>
                    <Grid container spacing={2}>
                        <Grid item xs={12}>
                            <Controller
                                name="name"
                                control={control}
                                rules={{
                                    required: {
                                        value: true,
                                        message: "Name is required"
                                    },
                                    maxLength: 20,
                                }}
                                render={({ field, fieldState }) => (
                                    <TextField
                                        {...field}
                                        label="Name"
                                        fullWidth
                                        error={!!fieldState.error}
                                        helperText={fieldState.error?.message?.toString()}
                                    />
                                )}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <Controller
                                name="pairingCode"
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
                        </Grid>
                    </Grid>
                </Box>
            </DialogContent>
            <DialogActions sx={{ display: "flex" }}>
                <div style={{ flex: "1 0 0" }} />
                <Button
                    disableElevation
                    color="error"
                    variant="text"
                    onClick={handleDialogClose}
                    disabled={isLoading}
                >
                    Cancel
                </Button>
                <Button
                    disableElevation
                    variant="contained"
                    sx={{ ml: 2 }}
                    disabled={isLoading}
                    onClick={handleSubmit(handleAddScreen)}
                >
                    Save
                </Button>
            </DialogActions>
        </Dialog>
    );
};
