using CarCheckup.Domain.Core.Dtos;
using CarCheckup.Domain.Core.Dtos.Car;
using CarCheckup.Domain.Core.Entities;

namespace CarCheckup.Domain.Core.Contarcts.Service;
public interface ICarService
{
    //Create
    int Create(Car car);

    int GetCarId(string plate);
    GetCarDto? GetById(int id);
   
}
