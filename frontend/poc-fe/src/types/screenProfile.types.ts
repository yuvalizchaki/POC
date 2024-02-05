export interface ScreenProfile {
    id: number;
    name: string;
    screens: Screen[];
  }


export interface Screen {
    id: number;
    ip: string;
    screenProfileId: number;
  }