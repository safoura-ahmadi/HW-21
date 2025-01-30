using CarCheckup.Domain.Core.Dtos.Car;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Enums.Car;

namespace CarCheckup.Domain.Core.Contarcts.AppService;

public interface ICarAppService
{
    Task<int> Create(CarDto car,CancellationToken cancellationToken);
    Task<CarDto?> Get(int id,CancellationToken cancellationToken);
    Task<int> GetCarId(string plate, CancellationToken cancellationToken);
}
