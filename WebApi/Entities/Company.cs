using WebApi.Entities.Enums;
namespace WebApi.Entities;

public class Company : EntityBase<int>
{
    public required string Name { get; set; }
    public string? Address { get; set; }

    public ICollection<Employee>? Employees { get; set; } = [];
    public ICollection<Bonus>? BonusList { get; set; } = [];
}
