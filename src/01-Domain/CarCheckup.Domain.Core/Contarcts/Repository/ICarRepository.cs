using CarCheckup.Domain.Core.Dtos;
using CarCheckup.Domain.Core.Dtos.Car;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Enums.Car;

namespace CarCheckup.Domain.Core.Contarcts.Repository;
public interface ICarRepository
{
    //Create
    Task<int> Create(Car car, CancellationToken cancellationToken);
    Task<GetCarDto?> GetById(int id, CancellationToken cancellationToken);
    Task<CarDto?> Get(int id, CancellationToken cancellationToken);
    Task<int> GetCarId(string plate, CancellationToken cancellationToken);

}
