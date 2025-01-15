namespace CarCheckup.Domain.Core.Contarcts.Service;

public interface IRejectedCheckupRequestService
{
    void Create(int carId);
}
