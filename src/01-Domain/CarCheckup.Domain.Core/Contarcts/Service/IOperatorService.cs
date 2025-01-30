namespace CarCheckup.Domain.Core.Contarcts.Service;

public interface IOperatorService
{
    Task<bool> Login(string username, string password, CancellationToken cancellationToken);
}
