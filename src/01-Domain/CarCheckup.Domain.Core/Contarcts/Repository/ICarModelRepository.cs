using CarCheckup.Domain.Core.Entities;

namespace CarCheckup.Domain.Core.Contarcts.Repository;

public interface ICarModelRepository
{
    Task<List<CarModel>> GetAll(CancellationToken cancellationToken);
    Task<CarModel> GetById(int id, CancellationToken cancellationToken);
    Task<bool> Create(string name, CancellationToken cancellationToken);
    Task<bool> UpdateName(int id, string name, CancellationToken cancellationToken);
    Task<bool> Delete(int id, CancellationToken cancellationToken);

}
