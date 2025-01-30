using CarCheckup.Domain.Core.Dtos.Car;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Enums.Car;

namespace CarCheckup.Domain.Core.Contarcts.AppService;

public interface ICarAppService
{
    int Create(CarDto car);
    CarDto? Get(int id);
    int GetCarId(string plate);
}
