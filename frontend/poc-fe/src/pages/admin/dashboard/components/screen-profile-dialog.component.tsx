import { useForm, SubmitHandler, FormProvider, useWatch } from "react-hook-form";
import {
    DisplayTemplateType,
    ScreenProfile,
    ScreenProfileFormFields,
    TimeMode,
    TimeUnit,
} from "../../../../types/screenProfile.types";
import { Box, Button, Dialog, DialogActions, DialogContent, DialogTitle } from "@mui/material";
import { ScreenPorfileForm } from "./screen-profile-form.component";
import { useAdminInfoContext } from "../../../../hooks/useAdminInfoContext";

interface BaseScreenProfileDialogProps {
    isOpen: boolean;
    mode: 'create' | 'update';
    onSubmit?: (data: ScreenProfileFormFields) => void;
    onSubmitted?: (data: ScreenProfileFormFields) => void;
    onCancel: () => void;
}

interface CreateModeProps extends BaseScreenProfileDialogProps {
    mode: 'create';
    screenProfileData: undefined;
}

interface UpdateModeProps extends BaseScreenProfileDialogProps {
    mode: 'update';
    screenProfileData: ScreenProfile;
}

type ScreenProfileDialogProps = CreateModeProps | UpdateModeProps;


export const ScreenProfileDialog = ({
    isOpen,
    mode,
    onSubmit,
    onSubmitted,
    onCancel,
    screenProfileData
}: ScreenProfileDialogProps) => {

    const { createScreenProfile, updateScreenProfile } = useAdminInfoContext();

    const form = useForm<ScreenProfileFormFields>({
        defaultValues: {
            name: '',
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
                        }
                    }
                },
            },
        },
    });

    const {
        watch,
        handleSubmit,
    } = form;

    const handleCancel = () => {
        form.reset();
        console.log('[DEBUG] CancelingScreen Profile!', form.getValues());
        onCancel();
    }

    const onSubmitCallback: SubmitHandler<ScreenProfileFormFields> = async (data) => {
        try {
            onSubmit?.(data);
            if (mode === 'create') {
                // Call API to create
                console.log('[DEBUG] Creating Screen Profile!', data);
                await createScreenProfile(data);
            } else {
                // Call API to update
                console.log('[DEBUG] Updating Screen Profile!', data);
                await updateScreenProfile(screenProfileData.id, data);
            }
            onSubmitted?.(data);
        } catch (error) {
            console.error('Failed to save profile:', error);
        }
    };

    const screenProfileName = watch('name');

    return (
        <Dialog open={isOpen} onClose={handleCancel}>
            <DialogTitle sx={{
                maxWidth: '100%',
                textOverflow: "ellipsis",
                whiteSpace: "nowrap",
                overflow: "hidden"
            }}>{
                    `${mode === 'create' ? 'Create' : 'Update'} ${screenProfileName || 'Undefined'}`
                }</DialogTitle>
            <DialogContent>
                <FormProvider {...form}>
                    <Box
                        component="form"
                        onSubmit={handleSubmit(onSubmitCallback)}
                        sx={{ mt: 1 }}
                    >
                        <ScreenPorfileForm />
                    </Box>
                </FormProvider>
            </DialogContent>
            <DialogActions>
                <Button
                    disableElevation
                    color="error"
                    variant="text"
                    onClick={handleCancel}
                >
                    Cancel
                </Button>
                <Button
                    disableElevation
                    variant="contained"
                    sx={{ ml: 2 }}
                    type="submit"
                >
                    Save
                </Button>
            </DialogActions>
        </Dialog>
    );
};
