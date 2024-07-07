namespace POC.Infrastructure.Adapters;

public interface ITypesAdapter
{
    public Task<string> FetchCompanyTypesAsync();
    public Task<string> FetchTagsTypesAsync();
}