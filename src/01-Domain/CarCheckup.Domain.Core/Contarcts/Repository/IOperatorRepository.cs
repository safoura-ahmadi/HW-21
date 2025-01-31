namespace CarCheckup.Domain.Core.Contarcts.Repository;

public interface IOperatorRepository
{
    Task<bool> Login(string username, string password, CancellationToken cancellationToken);
}
