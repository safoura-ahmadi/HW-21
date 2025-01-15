using CarCheckup.Domain.Core.Contarcts.AppService;
using CarCheckup.Domain.Core.Contarcts.Service;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Enums.Car;

namespace CarCheckup.Domain.Services.AppService;

public class CarAppService(ICarService carService) : ICarAppService
{
    private readonly ICarService _carService = carService;

    public Result Create(string plate, string ownerMeliCode, string ownerMobile, int generationYear, CarCompanyEnum company, int modelId)
    {
        if (!IsPlateValid(plate))
        {
            return new Result(false, "شماره پلاک تکراری است");
        }
        var item = new Car()
        {
            OwnerMeliCode = ownerMeliCode,
            OwnerMobile = ownerMobile,
            Company = company,
            ModelId = modelId,
            GenerationYear = generationYear,
            Plate = plate,

        };
        var id = _carService.Create(item);
        if (id > 0)
            return new Result (true);
        return new Result(false,"DataBase Erro Raised");
    }
    public bool IsPlateValid(string plate)
    {
        return _carService.IsPlateValid(plate);
    }
}
