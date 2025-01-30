using CarCheckup.Domain.Core.Contarcts.AppService;
using CarCheckup.Domain.Core.Contarcts.Service;

namespace CarCheckup.Domain.Services.AppService;

public class OperatorAppService(IOperatorService operatorService) : IOperatorAppService
{
    private readonly IOperatorService _operatorService = operatorService;

    public async Task< bool> Login(string username, string password,CancellationToken cancellationToken)
    {
        return await _operatorService.Login(username, password, cancellationToken);
    }
}
