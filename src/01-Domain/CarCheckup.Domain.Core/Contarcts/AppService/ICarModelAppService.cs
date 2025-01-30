using CarCheckup.Domain.Core.Entities;

namespace CarCheckup.Domain.Core.Contarcts.AppService;

public interface ICarModelAppService
{
    Task<CarModel?> GetById(int id, CancellationToken cancellationToken);
    Task<List<CarModel>> GetAll(CancellationToken cancellationToken);
    Task<Result> Create(string name, CancellationToken cancellationToken);
    Task<Result> UpdateName(int id, string name, CancellationToken cancellationToken);
    Task<Result> Delete(int id, CancellationToken cancellationToken);
}
