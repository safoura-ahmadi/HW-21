using CarCheckup.Domain.Core.Dtos.Car;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Enums.Car;

namespace CarCheckup.Domain.Core.Contarcts.AppService;

public interface ICarAppService
{
    int Create(CreateCarDto car);

    int GetCarId(string plate);
}
