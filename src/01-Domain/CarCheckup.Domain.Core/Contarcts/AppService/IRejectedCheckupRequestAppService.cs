namespace CarCheckup.Domain.Core.Contarcts.AppService;

public interface IRejectedCheckupRequestAppService
{
    void Create(int carId);
}
