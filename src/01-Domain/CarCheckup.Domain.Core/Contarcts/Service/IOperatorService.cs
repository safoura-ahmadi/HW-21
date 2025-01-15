namespace CarCheckup.Domain.Core.Contarcts.Service;

public interface IOperatorService
{
    bool Login(string username, string password);
}
