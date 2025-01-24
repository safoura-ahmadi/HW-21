using CarCheckup.Domain.Core.Entities;

namespace CarCheckup.Domain.Core.Contarcts.Repository;

public interface ICarModelRepository
{
    List<CarModel> GetAll();
    CarModel GetById(int id);
    bool Create(string name);
    bool UpdateName(int id,string name);
    bool Delete(int id);
    
}
