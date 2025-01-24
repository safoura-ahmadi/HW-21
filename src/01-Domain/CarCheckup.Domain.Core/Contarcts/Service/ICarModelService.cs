using CarCheckup.Domain.Core.Entities;

namespace CarCheckup.Domain.Core.Contarcts.Service;

public interface ICarModelService
{
    CarModel GetById(int id);
    List<CarModel> GetAll();
    bool Create(string name);
    bool UpdateName(int id, string name);
    bool Delete(int id);
}
