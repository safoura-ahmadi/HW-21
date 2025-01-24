using CarCheckup.Domain.Core.Contarcts.Repository;
using CarCheckup.Domain.Core.Contarcts.Service;
using CarCheckup.Domain.Core.Entities;

namespace CarCheckup.Domain.Services.Service;

public class CarModelService(ICarModelRepository carModelRepository) : ICarModelService
{
    private readonly ICarModelRepository _carModelRepository = carModelRepository;

    public bool Create(string name)
    {
        return _carModelRepository.Create(name);
    }

    public bool Delete(int id)
    {
        return (_carModelRepository.Delete(id));
    }

    public List<CarModel> GetAll()
    {
        return _carModelRepository.GetAll();
    }

    public CarModel GetById(int id)
    {
        return _carModelRepository.GetById(id);
    }

    public bool UpdateName(int id, string name)
    {
        return _carModelRepository.UpdateName(id, name);
    }
}
