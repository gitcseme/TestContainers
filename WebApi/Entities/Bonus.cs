using WebApi.Entities.Enums;
namespace WebApi.Entities;

public class Bonus : EntityBase<int>
{
    public required string Title { get; set; }
    public decimal AmountPercentage { get; set; }
    public bool Active { get; set; }
    public Month Month { get; set; }
    public Level EmployeeLevel { get; set; }

    public Company Company { get; set; }
    public int CompanyId { get; set; }
}
