using CarCheckup.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace CarCheckup.Domain.Core.Contarcts.AppService;

public interface IOperatorAppService
{
    Task<IdentityResult> Login(string username, string password, CancellationToken cancellationToken);
    Task<IdentityResult> Register(Operator model,string password, CancellationToken cancellationToken);

}
