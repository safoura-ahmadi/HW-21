namespace CarCheckup.Domain.Core.Contarcts.Service;

public interface IRejectedCheckupRequestService
{
    Task Create(int carId, CancellationToken cancellationToken);
}
