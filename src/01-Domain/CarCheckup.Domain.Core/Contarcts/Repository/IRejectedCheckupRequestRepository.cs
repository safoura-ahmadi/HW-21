namespace CarCheckup.Domain.Core.Contarcts.Repository;

public interface IRejectedCheckupRequestRepository
{
    Task Create(int carId, CancellationToken cancellationToken);
}
