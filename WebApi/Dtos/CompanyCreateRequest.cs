using WebApi.Entities;

namespace WebApi.Dtos;

public class CompanyCreateRequest 
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Address { get; set; }

    public IEnumerable<EmployeeCreateRequest> Employees { get; set; } = [];
    public IEnumerable<BonusCreateRequest> BonusList { get; set; } = [];
}