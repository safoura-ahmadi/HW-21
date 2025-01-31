using CarCheckup.Domain.Core.Contarcts.AppService;
using CarCheckup.Domain.Core.Contarcts.Service;
using CarCheckup.Domain.Core.Dtos.Car;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Enums.Car;
using Microsoft.VisualBasic;
using System.Globalization;

namespace CarCheckup.Domain.Services.AppService;

public class CarAppService(ICarService carService) : ICarAppService
{
    private readonly ICarService _carService = carService;

    public async Task<int> Create(CarDto car, CancellationToken cancellationToken)
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
         return await _carService.Create(item, cancellationToken);

    }

    public async Task<CarDto?> Get(int id, CancellationToken cancellationToken)
    {
        return await _carService.Get(id, cancellationToken);
    }

    public async Task<int> GetCarId(string plate, CancellationToken cancellationToken)
    {
        return await _carService.GetCarId(plate, cancellationToken);
    }

}
