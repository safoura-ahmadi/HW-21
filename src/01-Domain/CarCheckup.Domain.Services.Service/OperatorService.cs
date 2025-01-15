using CarCheckup.Domain.Core.Contarcts.Repository;
using CarCheckup.Domain.Core.Contarcts.Service;

namespace CarCheckup.Domain.Services.Service;

public class OperatorService(IOperatorRepository operatorRepository) : IOperatorService
{
    private readonly IOperatorRepository _operatorRepository = operatorRepository;
    public bool Login(string username, string password)
    {
        return _operatorRepository.Login(username, password);
    }
}
