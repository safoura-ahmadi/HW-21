using CarCheckup.Domain.Core.Contarcts.AppService;
using CarCheckup.Domain.Core.Contarcts.Service;
using CarCheckup.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace CarCheckup.Domain.Services.AppService;

public class OperatorAppService(UserManager<Operator> userManager, SignInManager<Operator> signInManager) : IOperatorAppService
{


    public async Task<IdentityResult> Login(string username, string password, CancellationToken cancellationToken)
    {
        var result = await signInManager.PasswordSignInAsync(username, password, true, false);
        return result.Succeeded ? IdentityResult.Success : IdentityResult.Failed();

    }
    public async Task<IdentityResult> Register(Operator model,string password, CancellationToken cancellationToken)
    {

        var result = await userManager.CreateAsync(model, password);

        return result;
    }

}
