using CarCheckup.Domain.Core.Entities;

namespace CarCheckup.Domain.Core.Contarcts.Service;

public interface ICarModelService
{
    List<CarModel> GetAll();
}
