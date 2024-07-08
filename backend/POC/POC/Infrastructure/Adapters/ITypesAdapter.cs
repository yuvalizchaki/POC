namespace POC.Infrastructure.Adapters;

public interface ITypesAdapter
{
    public Task<string> FetchCompanyTypesAsync(int companyId = 1);
    public Task<string> FetchTagsTypesAsync(int companyId = 1);
}