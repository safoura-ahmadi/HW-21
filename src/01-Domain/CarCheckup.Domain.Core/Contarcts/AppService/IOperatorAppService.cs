using Microsoft.AspNetCore.Identity;

namespace CarCheckup.Domain.Core.Contarcts.AppService;

public interface IOperatorAppService
{
    Task<IdentityResult> Login(string username, string password, CancellationToken cancellationToken);


}
