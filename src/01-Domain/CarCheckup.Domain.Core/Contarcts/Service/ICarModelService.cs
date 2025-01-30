using CarCheckup.Domain.Core.Entities;

namespace CarCheckup.Domain.Core.Contarcts.Service;

public interface ICarModelService
{
    Task<CarModel> GetById(int id, CancellationToken cancellationToken);
    Task<List<CarModel>> GetAll(CancellationToken cancellationToken);
    Task<bool> Create(string name, CancellationToken cancellationToken);
    Task<bool> UpdateName(int id, string name, CancellationToken cancellationToken);
    Task<bool> Delete(int id, CancellationToken cancellationToken);
}
