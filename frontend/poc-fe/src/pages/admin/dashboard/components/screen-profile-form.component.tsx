import {
  CreateScreenProfileDto,
  UpdateScreenProfileDto,
} from "../../../../types/screenProfile.types";

interface ScreenPorfileFormProps {
  onChange: (
    screenProfile: CreateScreenProfileDto | UpdateScreenProfileDto
  ) => void;
}
export const ScreenPorfileForm = ({ onChange }: ScreenPorfileFormProps) => {
  onChange;
  return <>TODO: IMPLEMENT!</>;
};
