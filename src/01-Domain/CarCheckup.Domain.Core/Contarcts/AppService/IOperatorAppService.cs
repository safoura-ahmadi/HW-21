namespace CarCheckup.Domain.Core.Contarcts.AppService;

public interface IOperatorAppService
{
    Task<bool> Login(string username, string password, CancellationToken cancellationToken);


}
