using CarCheckup.Domain.Core.Contarcts.AppService;
using CarCheckup.Domain.Core.Contarcts.Service;

namespace CarCheckup.Domain.Services.AppService;

public class OperatorAppService(IOperatorService operatorService) : IOperatorAppService
{
    private readonly IOperatorService _operatorService = operatorService;

    public bool Login(string username, string password)
    {
        return _operatorService.Login(username, password);
    }
}
