using WebApi.Entities.Enums;

namespace WebApi.Dtos;

public class EmployeeCreateRequest
{
    public required string Name { get; set; }
    public int EmployeeId { get; set; }

    public Level Level { get; set; }

    public decimal Salary { get; set; }


    public int CompanyId { get; set; }
}
