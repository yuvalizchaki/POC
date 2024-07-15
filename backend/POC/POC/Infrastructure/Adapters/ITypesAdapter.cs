namespace POC.Infrastructure.Adapters;

public interface ITypesAdapter
{
    public Task<string> FetchCompanyTypesAsync(int companyId);
    public Task<string> FetchTagsTypesAsync(int companyId);
}