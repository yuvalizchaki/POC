export enum OrderStatus {
  Draft = 1,
  Hidden = 2,
  Approved = 3,
  Completed = 4,
  Canceled = 5,
  Returned = 6,
  Ready = 7,
}
export const orderStatusMap = {
  [OrderStatus.Draft]: 'Draft',
  [OrderStatus.Hidden]: 'Hidden',
  [OrderStatus.Approved]: 'Approved',
  [OrderStatus.Completed]: 'Completed',
  [OrderStatus.Canceled]: 'Canceled',
  [OrderStatus.Returned]: 'Returned',
  [OrderStatus.Ready]: 'Ready',
};
export const orderStatusList = Object.entries(orderStatusMap).map(([value, label]) => ({ value: Number(value), label }));


export interface OrderDto {
  id: number;
  customerId: number;
  clientName?: string;
}

export interface AppEntity {
  Id: number;
  CompanyId: number;
  Name: string;
  Description: string;
  IsOwnInventory: boolean;
  Childs: AppEntity[];
}

export interface OrderTag {
  Id: number;
  Name: string;
}