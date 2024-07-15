import React from "react";
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogContentText,
} from "@mui/material";
import { MsgSentToScreen } from "../types/signalR.types"; // Ensure this path is correct

interface MessagePopupProps {
  message: MsgSentToScreen | null;
}

const MessagePopup: React.FC<MessagePopupProps> = ({ message }) => {
  return (
    <Dialog
      open={!!message}
      onClose={() => {}} // You can modify this if needed to handle dialog close actions
      aria-labelledby="message-dialog-title"
      aria-describedby="message-dialog-description"
    >
      {message && (
        <>
          <DialogTitle id="message-dialog-title">{`Message from ${message.senderName}`}</DialogTitle>
          <DialogContent>
            <DialogContentText id="message-dialog-description">
              {message.message}
            </DialogContentText>
          </DialogContent>
        </>
      )}
    </Dialog>
  );
};

export default MessagePopup;
