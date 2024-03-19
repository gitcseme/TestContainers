using WebApi.Dtos;

namespace WebApi.Services;

public interface ICompanyService
{
    Task<int> CreateCompanyAsync(CompanyCreateRequest request);
    Task<CompanyResponse?> GetByIdAsync(int id);
    Task<IEnumerable<CompanyResponse>> GetCompaniesAsync();
}