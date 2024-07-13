export enum OrderStatus {
  Draft = 1,
  Hidden = 2,
  Approved = 3,
  Completed = 4,
  Canceled = 5,
  Returned = 6,
  Ready = 7,
}
export const orderStatusDisplayMap = {
  [OrderStatus.Draft]: 'Draft',
  [OrderStatus.Hidden]: 'Hidden',
  [OrderStatus.Approved]: 'Approved',
  [OrderStatus.Completed]: 'Completed',
  [OrderStatus.Canceled]: 'Canceled',
  [OrderStatus.Returned]: 'Returned',
  [OrderStatus.Ready]: 'Ready',
};
export const orderStatusList = Object.entries(orderStatusDisplayMap).map(([value, label]) => ({ value: Number(value), label }));

export enum OrderTransportType {
  Incoming = 1,
  Outgoing = 2
}
export const orderTransportTypeDisplayMap = {
  [OrderTransportType.Outgoing]: "Outgoing",
  [OrderTransportType.Incoming]: "Incoming"
}

interface BaseOrderDto {
  id: number;
  companyId: number;
}

export interface CrmOrderDto extends BaseOrderDto {
  departmentId: number;
  customerId: number;
  clientName: string;
  firstName?: string;
  lastName?: string;
  startDate: string;
  endDate: string;
  departDate?: string;
  returnDate?: string;
  status: OrderStatus;
  isPickup: boolean;
  createdOn: string;
  updatedOn?: string;

  // Navigation properties
  orderItems: CrmInventoryItem[];
  serviceOrderItems: CrmServiceItemDto[];
  peopleOrderItems: CrmPeopleOrderItemDto[];
  oneTimeOrderItems: CrmOneTimeOrderItemDto[];
}
export interface OrderDto {
  crmOrder: CrmOrderDto;
  transportType: OrderTransportType;
}

interface CrmInventoryItem {
  id: number;
  departmentId: number;
  amount: number;
  price: number;
  productId: number;
  productName: string;
  productDescription: string;
  isBundle: boolean;
  productImages: string[];
}
export interface InventoryItem {
  crmInventoryItem: CrmInventoryItem;
  transportType: OrderTransportType;
}
interface CrmServiceItemDto {
  id: number;
  departmentId: number;
  quantity: number;
  serviceProfileId: number;
  serviceId: number;
  serviceProfileName: string;
  serviceProfileDescription: string;
  isBundle: boolean;
  serviceProfileImages: string[];
  icon?: number;
  amount: number;
  price: number;
}

interface CrmPeopleOrderItemDto {
  id: number;
  peopleProfileId: number;
  departmentId: number;
  peopleId: number;
  peopleProfileName: string;
  peopleProfileDescription: string;
  isBundle: boolean;
  peopleProfileImages: string[];
  amount: number;
  price: number;
}

interface CrmOneTimeOrderItemDto {
  id: number;
  departmentId: number;
  amount: number;
  price: number;
  name: string;
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