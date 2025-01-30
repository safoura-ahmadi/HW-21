using CarCheckup.Domain.Core.Dtos;
using CarCheckup.Domain.Core.Dtos.Car;
using CarCheckup.Domain.Core.Entities;

namespace CarCheckup.Domain.Core.Contarcts.Service;
public interface ICarService
{
    //Create
    Task<int> Create(Car car, CancellationToken cancellationToken);
    Task<CarDto?> Get(int id, CancellationToken cancellationToken);
    Task<int> GetCarId(string plate, CancellationToken cancellationToken);
    Task<GetCarDto?> GetById(int id, CancellationToken cancellationToken);

}
