namespace CarCheckup.Domain.Core.Contarcts.Repository;

public interface IOperatorRepository
{
    bool Login(string username, string password);
}
