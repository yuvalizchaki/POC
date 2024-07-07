import {
    Box,
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    FormHelperText,
} from "@mui/material";
import React, { useState } from "react";
import { useForm, Controller } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import { useAdminInfoContext } from "../../../../hooks/useAdminInfoContext";
import { MuiOtpInput } from "mui-one-time-password-input";
import { AxiosError } from "axios";

const CODE_LENGTH = 6;

const schema = z.object({
    code: z.string().length(CODE_LENGTH, "OTP must be exactly 6 characters"),
});

type FormValues = z.infer<typeof schema>;

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
    const { control, handleSubmit, reset } = useForm<FormValues>({
        resolver: zodResolver(schema),
        defaultValues: {
            code: "",
        },
    });

    const handleAddScreen = async ({ code }: FormValues) => {
        setIsLoading(true);
        setError(null);
        try {
            await pairScreen(code, profileId);
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
                            onClick={handleDialogClose}
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
                </Box>
            </DialogContent>
        </Dialog>
    );
};
