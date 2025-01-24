using System.ComponentModel.DataAnnotations;

namespace CarCheckup.Domain.Core.Entities;

public class Operator
{
    public int Id { get; set; }
    [Required(ErrorMessage = "وارد کردن نام کاربری الزامی است")]
    public required string Username { get; set; }
    [Required(ErrorMessage = "وارد کردن نام کاربری الزامی است")]
    public required string Password { get; set; }
}
