export interface BaseOrderItemDto {
    Id: number;
    OrderId: number;
    DepartmentId: number;
    SortOrder?: number;
    Amount: number;
    Price: number;
    Remarks?: string;
    IconStatus: number;
}
