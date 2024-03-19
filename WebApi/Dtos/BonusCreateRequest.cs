using WebApi.Entities.Enums;

namespace WebApi.Dtos;

public class BonusCreateRequest
{
    public required string Title { get; set; }
    public decimal AmountPercentage { get; set; }
    public bool Active { get; set; }
    public Month Month { get; set; }
    public Level EmployeeLevel { get; set; }
    public int CompanyId { get; set; }
}

public class BonusResponse
{
    public required string Title { get; set; }
    public decimal AmountPercentage { get; set; }
    public bool Active { get; set; }
    public Month Month { get; set; }
    public Level EmployeeLevel { get; set; }
}