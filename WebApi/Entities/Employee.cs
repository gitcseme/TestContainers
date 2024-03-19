using WebApi.Entities.Enums;
namespace WebApi.Entities;

public class Employee : EntityBase<int>
{
    public required string Name { get; set; }
    public int EmployeeId { get; set; }

    public Level Level { get; set; }

    public decimal Salary { get; set; }


    public int CompanyId { get; set; }
    public Company Company { get; set; }
}
