using IntegrationTests.Helpers;
using Shouldly;
using WebApi.Dtos;

namespace IntegrationTests
{
    public class CompaniesControllerTests : BaseIntegrationTest
    {
        public CompaniesControllerTests(TestEnvironmentWebApplicationFactory factory)
            : base(factory)
        {
        }

        [Fact]
        public async void GetCompanies_WhenCalled_ShouldReturnAllCompanies()
        {
            // Act
            // This request retrieves the companies created during migration.
            var companies = await GetAsync<List<CompanyResponse>>("/api/Companies");

            // Assert
            companies.ShouldNotBeNull();
            companies.Count().ShouldBe(2);
        }

        [Fact]
        public async void CreateCompany_GivenCompanyData_ShouldCreateCompany()
        {
            // Arrange
            var company = new CompanyCreateRequest
            {
                Name = "Autonemo",
                Address = "Bonani ahmed tower",
                Employees =
                [
                    new EmployeeCreateRequest
                    {
                        Name = "Aurpan",
                        Level = WebApi.Entities.Enums.Level.SDEL2,
                        Salary = 90000
                    },
                ],
                BonusList =
                [
                    new BonusCreateRequest
                    {
                        Title = "Performance",
                        Active = true,
                        AmountPercentage = 20,
                        Month = WebApi.Entities.Enums.Month.Aug,
                        EmployeeLevel = WebApi.Entities.Enums.Level.SDEL2
                    }
                ]
            };

            // Act
            var companyId = await PostAsync<CompanyCreateRequest, int>("/api/Companies", company);

            var createdCompany = await GetAsync<CompanyResponse>($"/api/Companies/{companyId}");

            // Assert
            createdCompany.ShouldNotBeNull();
            createdCompany.Name.ShouldBe(company.Name);
            createdCompany.Address.ShouldBe(company.Address);
            createdCompany.BonusList!.Count().ShouldBe(company.BonusList.Count());
            createdCompany.Employees!.Count().ShouldBe(company.Employees.Count());
        }

    }
}