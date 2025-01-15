namespace CarCheckup.Domain.Core.Entities;

public class Operator
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}
