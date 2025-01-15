namespace CarCheckup.Domain.Core.Contarcts.Repository;

public interface IRejectedCheckupRequestRepository
{
    void Create(int carId);
}
