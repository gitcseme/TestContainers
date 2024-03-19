using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Repositories;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApi.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepository<Company, int> _repository;

        public CompanyService(IRepository<Company, int> repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateCompanyAsync(CompanyCreateRequest request)
        {
            var companyToCreate = new Company
            {
                Name = request.Name,
                Address = request.Address,
                BonusList = request.BonusList?.Select(b => new Bonus
                {
                    Title = b.Title,
                    Active = b.Active,
                    AmountPercentage = b.AmountPercentage,
                    EmployeeLevel = b.EmployeeLevel,
                    Month = b.Month
                }).ToList(),
                Employees = request.Employees?.Select(e => new Employee
                {
                    Name = e.Name,
                    EmployeeId = e.EmployeeId,
                    Level = e.Level,
                    Salary = e.Salary,
                }).ToList(),
            };

            await _repository.AddAsync(companyToCreate);
            return companyToCreate.Id;
        }

        public async Task<CompanyResponse?> GetByIdAsync(int id)
        {
            var company = await _repository.GetAll()
                .Where(c => c.Id == id)
                .Include(c => c.Employees)
                .Include(c => c.BonusList)
                .FirstOrDefaultAsync();

            return new CompanyResponse
            {
                Id = company.Id,
                Name = company.Name,
                Address = company.Address,
                Employees = company?.Employees?.Select(e => new EmployeeResponse
                {
                    Name = e.Name,
                    EmployeeId = e.EmployeeId,
                    Level = e.Level,
                    Salary = e.Salary,
                }),
                BonusList = company?.BonusList?.Select(b => new BonusResponse
                {
                    Title = b.Title,
                    Active = b.Active,
                    AmountPercentage = b.AmountPercentage,
                    EmployeeLevel = b.EmployeeLevel,
                    Month = b.Month
                })
            };
        }

        public async Task<IEnumerable<CompanyResponse>> GetCompaniesAsync()
        {
            var companies = await _repository.GetAll()
                .Select(c => new CompanyResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    Employees = c.Employees.Select(e => new EmployeeResponse
                    {
                        Name = e.Name,
                        EmployeeId = e.EmployeeId,
                        Level = e.Level,
                        Salary = e.Salary,
                    }),
                    BonusList = c.BonusList.Select(b => new BonusResponse
                    {
                        Title = b.Title,
                        Active = b.Active,
                        AmountPercentage = b.AmountPercentage,
                        EmployeeLevel = b.EmployeeLevel,
                        Month = b.Month
                    })
                })
                .ToListAsync();

            return companies;
        }
    }
}
