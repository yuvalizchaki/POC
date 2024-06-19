export enum OrderStatus {
  Draft = 1,
  Hidden = 2,
  Approved = 3,
  Completed = 4,
  Canceled = 5,
  Returned = 6,
  Ready = 7,
}

export interface OrderDto {
  id: number;
  customerId: number;
  clientName?: string;
}
