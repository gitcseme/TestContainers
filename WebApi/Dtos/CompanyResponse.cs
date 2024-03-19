namespace WebApi.Dtos;

public class CompanyResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Address { get; set; }

    public IEnumerable<EmployeeResponse>? Employees { get; set; }
    public IEnumerable<BonusResponse>? BonusList { get; set; }

}
