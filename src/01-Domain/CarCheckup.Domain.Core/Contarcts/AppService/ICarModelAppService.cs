using CarCheckup.Domain.Core.Entities;

namespace CarCheckup.Domain.Core.Contarcts.AppService;

public interface ICarModelAppService
{
    CarModel? GetById(int id);
    List<CarModel> GetAll();
    Result Create(string name);
   Result UpdateName(int id, string name);
   Result Delete(int id);
}
