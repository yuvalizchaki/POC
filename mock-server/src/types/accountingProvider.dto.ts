type ProviderType = number; // 1 | 2
export interface AccountingProviderDto {
    Id: number;
    AccountingId?: string;
    DocumentUrl?: string;
    ProviderType: ProviderType[]; // Assuming ProviderType is an enum or another type
}