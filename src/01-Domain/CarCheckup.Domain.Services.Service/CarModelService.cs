using CarCheckup.Domain.Core.Contarcts.Repository;
using CarCheckup.Domain.Core.Contarcts.Service;
using CarCheckup.Domain.Core.Entities;

namespace CarCheckup.Domain.Services.Service;

public class CarModelService(ICarModelRepository carModelRepository) : ICarModelService
{
    private readonly ICarModelRepository _carModelRepository = carModelRepository;

    public async Task<bool> Create(string name, CancellationToken cancellationToken)
    {
        return await _carModelRepository.Create(name, cancellationToken);
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        return await (_carModelRepository.Delete(id, cancellationToken));
    }

    public async Task<List<CarModel>> GetAll(CancellationToken cancellationToken)
    {
        return await _carModelRepository.GetAll(cancellationToken);
    }

    public async Task<CarModel> GetById(int id, CancellationToken cancellationToken)
    {
        return await _carModelRepository.GetById(id, cancellationToken);
    }

    public async Task<bool> UpdateName(int id, string name, CancellationToken cancellationToken)
    {
        return await _carModelRepository.UpdateName(id, name, cancellationToken);
    }
}
