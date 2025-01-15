using CarCheckup.Domain.Core.Entities;

namespace CarCheckup.Domain.Core.Contarcts.Repository;

public interface ICarModelRepository
{
    List<CarModel> GetAll();
    
}
