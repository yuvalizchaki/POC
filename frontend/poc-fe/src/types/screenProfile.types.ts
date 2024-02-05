export interface ScreenProfile {
    id: number;
    name: string;
    screens: Screen[];
  }


export interface Screen {
    id: number;
    screenProfileId: number;
  }