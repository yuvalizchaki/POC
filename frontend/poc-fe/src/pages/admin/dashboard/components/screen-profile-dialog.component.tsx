import { useForm, SubmitHandler, FormProvider } from "react-hook-form";
import { useEffect } from "react";
import {
    DisplayTemplateType,
    ScreenProfile,
    ScreenProfileFormFields,
    TimeInclude,
    TimeMode,
    TimeUnit,
} from "../../../../types/screenProfile.types";
import {
    Box,
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
} from "@mui/material";
import { ScreenPorfileForm } from "./screen-profile-form.component";
import { useAdminInfoContext } from "../../../../hooks/useAdminInfoContext";

interface BaseScreenProfileDialogProps {
    isOpen: boolean;
    mode: "create" | "update";
    onSubmit?: (data: ScreenProfileFormFields) => void;
    onSubmitted?: (data: ScreenProfileFormFields) => void;
    onCancel: () => void;
}

interface CreateModeProps extends BaseScreenProfileDialogProps {
    mode: "create";
    screenProfileData?: undefined;
}

interface UpdateModeProps extends BaseScreenProfileDialogProps {
    mode: "update";
    screenProfileData: ScreenProfile;
}

type ScreenProfileDialogProps = CreateModeProps | UpdateModeProps;

export const ScreenProfileDialog = ({
    isOpen,
    mode,
    onSubmit,
    onSubmitted,
    onCancel,
    screenProfileData,
}: ScreenProfileDialogProps) => {
    const { createScreenProfile, updateScreenProfile } = useAdminInfoContext();

    const form = useForm<ScreenProfileFormFields>({
        defaultValues: {
            name: "",
            companyId: 1, // TODO: Change this from hard coded to infer from admin context
            screenProfileFiltering: {
                displayConfig: {
                    displayTemplate: DisplayTemplateType.Orders,
                    isPaging: true,
                },
                orderFiltering: {
                    timeRanges: {
                        from: {
                            mode: TimeMode.Start,
                            unit: TimeUnit.Day,
                            amount: 0,
                        },
                        to: {
                            mode: TimeMode.End,
                            unit: TimeUnit.Day,
                            amount: 0,
                        },
                        include: TimeInclude.Both,
                    },
                },
            },
        },
    });

    const { reset, watch, handleSubmit } = form;

    useEffect(() => {
        if (mode === "update" && screenProfileData) {
            reset(screenProfileData);
        }
    }, [mode, screenProfileData, reset]);

    const handleCancel = () => {
        form.reset();
        onCancel();
    };

    const onSubmitCallback: SubmitHandler<ScreenProfileFormFields> = async (data) => {
        try {
            onSubmit?.(data);
            if (mode === "create") {
                await createScreenProfile(data);
            } else {
                await updateScreenProfile(screenProfileData.id, data);
            }
            form.reset();
            onSubmitted?.(data);
        } catch (error) {
            console.error("Failed to save profile:", error);
        }
    };

    const screenProfileName = watch("name");

    return (
        <Dialog open={isOpen} onClose={handleCancel}>
            <DialogTitle
                sx={{
                    maxWidth: "100%",
                    textOverflow: "ellipsis",
                    whiteSpace: "nowrap",
                    overflow: "hidden",
                }}
            >
                {`${mode === "create" ? "Create" : "Update"} ${screenProfileName || "Undefined"
                    }`}
            </DialogTitle>
            <DialogContent>
                <FormProvider {...form}>
                    <Box component="form" onSubmit={handleSubmit(onSubmitCallback)} sx={{ mt: 1 }}>
                        <ScreenPorfileForm />
                    </Box>
                </FormProvider>
            </DialogContent>
            <DialogActions>
                <Button disableElevation color="error" variant="text" onClick={handleCancel}>
                    Cancel
                </Button>
                <Button
                    disableElevation
                    variant="contained"
                    sx={{ ml: 2 }}
                    onClick={handleSubmit(onSubmitCallback)}
                >
                    Save
                </Button>
            </DialogActions>
        </Dialog>
    );
};
