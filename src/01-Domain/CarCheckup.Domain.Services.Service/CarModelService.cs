using CarCheckup.Domain.Core.Contarcts.Repository;
using CarCheckup.Domain.Core.Contarcts.Service;
using CarCheckup.Domain.Core.Entities;

namespace CarCheckup.Domain.Services.Service;

public class CarModelService(ICarModelRepository carModelRepository) : ICarModelService
{
    private readonly ICarModelRepository _carModelRepository = carModelRepository;
    public List<CarModel> GetAll()
    {
        return _carModelRepository.GetAll();
    }
}
