using Microsoft.AspNetCore.Identity;


namespace CarCheckup.Domain.Core.Entities;

public class Operator : IdentityUser<int>
{
    public int Id { get; set; }
    public  int Age { get; set; }
    //[Required(ErrorMessage = "Username is required")]
    //public override string UserName { get; set; } = null!;

    //[Required(ErrorMessage = "Password is required")]
    //public override string PasswordHash { get; set; } = null!;

}
