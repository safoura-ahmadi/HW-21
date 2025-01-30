using CarCheckup.Domain.Core.Contarcts.Repository;
using CarCheckup.Domain.Core.Contarcts.Service;

namespace CarCheckup.Domain.Services.Service;

public class OperatorService(IOperatorRepository operatorRepository) : IOperatorService
{
    private readonly IOperatorRepository _operatorRepository = operatorRepository;
    public async Task<bool> Login(string username, string password, CancellationToken cancellationToken)
    {
        return await _operatorRepository.Login(username, password, cancellationToken);
    }
}
