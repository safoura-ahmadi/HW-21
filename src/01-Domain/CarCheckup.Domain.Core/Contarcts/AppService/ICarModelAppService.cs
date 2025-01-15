using CarCheckup.Domain.Core.Entities;

namespace CarCheckup.Domain.Core.Contarcts.AppService;

public interface ICarModelAppService
{
    List<CarModel> GetAll();
}
