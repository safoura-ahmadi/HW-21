using CarCheckup.Domain.Core.Contarcts.AppService;
using CarCheckup.Domain.Core.Contarcts.Service;
using CarCheckup.Domain.Core.Dtos.Car;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Enums.Car;
using System.Globalization;

namespace CarCheckup.Domain.Services.AppService;

public class CarAppService(ICarService carService) : ICarAppService
{
    private readonly ICarService _carService = carService;

    public int Create(CarDto car)
    {
        var pc = new PersianCalendar();
        var shamiDate = pc.ToDateTime(car.GenerationYear, 1, 1, 0, 0, 0, 0);
        var miladiDate = shamiDate.ToUniversalTime();
         
        var item = new Car()
        {
            OwnerMeliCode = car.OwnerMeliCode,
            OwnerMobile = car.OwnerMobile,
            Plate = car.Plate,
            ModelId = car.ModelId,
            Company = car.Company,
            GenerationYear = DateOnly.FromDateTime(miladiDate)
        };
        return _carService.Create(item);

    }

    public CarDto? Get(int id)
    {
        return _carService.Get(id);
    }

    public int GetCarId(string plate)
    {
        return _carService.GetCarId(plate);
    }
    
}
