using CarCheckup.Domain.Core.Contarcts.AppService;
using CarCheckup.Domain.Core.Contarcts.Service;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Enums.Car;

namespace CarCheckup.Domain.Services.AppService;

public class CarModelAppService(ICarModelService carModelService) : ICarModelAppService
{
    private readonly ICarModelService _carModelService = carModelService;

    public List<CarModel> GetAll()
    {
        return _carModelService.GetAll();
    }
}
