using CarCheckup.Domain.Core.Contarcts.AppService;
using CarCheckup.Domain.Core.Contarcts.Service;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Enums.Car;

namespace CarCheckup.Domain.Services.AppService;

public class CarAppService(ICarService carService) : ICarAppService
{
    private readonly ICarService _carService = carService;

    public Result Create(Car car)
    {
        var id = _carService.Create(car);
        if (id > 0)
        {
            car.Id = id;
            return new Result(true);
        }
       
        return new Result(false, "DataBase Erro Raised");
    }

    public int GetCarId(string plate)
    {
        return _carService.GetCarId(plate);
    }
}
