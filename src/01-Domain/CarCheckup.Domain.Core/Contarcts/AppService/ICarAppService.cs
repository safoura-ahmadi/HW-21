using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Enums.Car;

namespace CarCheckup.Domain.Core.Contarcts.AppService;

public interface ICarAppService
{
    Result Create(string plate, string ownerMeliCode, string ownerMobile, int generationYear, CarCompanyEnum company, int modelId);
    bool IsPlateValid(string plate);

}
