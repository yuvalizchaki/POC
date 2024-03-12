export interface ScreenProfile {
    id: number;
    name: string;
    screens: ScreenDto[];
  }


export interface ScreenDto {
    id: number;
    screenProfileId: number;
  }