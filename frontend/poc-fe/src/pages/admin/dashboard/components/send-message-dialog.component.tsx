import React, { useState } from "react";
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  TextField,
  MenuItem,
} from "@mui/material";
import { useAdminInfoContext } from "../../../../hooks/useAdminInfoContext";
import { AdminMessageDto } from "../../../../types/adminData.types";

interface SendMessageDialogProps {
  isOpen: boolean;
  onCancel: () => void;
  screenProfileId: number;
  screenProfileName: string;
}

const SendMessageDialog = ({
  isOpen,
  onCancel,
  screenProfileId,
  screenProfileName,
}: SendMessageDialogProps) => {


  const { sendMessage } = useAdminInfoContext();
  const [message, setMessage] = useState<string>("");
  const [displayTime, setDisplayTime] = useState<number>(5000);

  
  const handleCancel = () => {
    setMessage("");
    setDisplayTime(5000);
    onCancel();
  }

  const handleSave = () => {
    const dto: AdminMessageDto = {
      message,
      displayTime,
    };
    sendMessage(screenProfileId, dto);
    handleCancel(); // Optionally reset message and displayTime here if needed
  };

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
        {screenProfileName}
      </DialogTitle>
      <DialogContent>
        <TextField
          fullWidth
          label="Message"
          multiline
          rows={4}
          value={message}
          onChange={(e) => setMessage(e.target.value)}
          margin="dense"
        />
        <TextField
          select
          fullWidth
          label="Display Time"
          value={displayTime}
          onChange={(e) => setDisplayTime(parseInt(e.target.value))}
          margin="dense"
        >
          <MenuItem value={5000}>5 Seconds</MenuItem>
          <MenuItem value={10000}>10 Seconds</MenuItem>
          <MenuItem value={30000}>30 Seconds</MenuItem>
        </TextField>
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
          onClick={handleSave}
        >
          Save
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default SendMessageDialog;
