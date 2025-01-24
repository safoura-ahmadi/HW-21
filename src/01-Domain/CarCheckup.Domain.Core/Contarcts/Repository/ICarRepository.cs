using CarCheckup.Domain.Core.Dtos;
using CarCheckup.Domain.Core.Dtos.Car;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Enums.Car;

namespace CarCheckup.Domain.Core.Contarcts.Repository;
public interface ICarRepository
{
    //Create
    int Create(Car car);
     GetCarDto? GetById(int id);
    int GetCarId(string plate);

}
