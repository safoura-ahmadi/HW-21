using CarCheckup.Domain.Core.Contarcts.Repository;
using CarCheckup.Domain.Core.Contarcts.Service;
using CarCheckup.Domain.Core.Dtos.Car;
using CarCheckup.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCheckup.Domain.Services.Service;

public class CarService(ICarRepository carRepository) : ICarService
{
    private readonly ICarRepository _carRepository = carRepository;
    public int Create(Car car)
    {
        return _carRepository.Create(car);
    }

    public GetCarDto? GetById(int id)
    {
        return _carRepository.GetById(id);
    }

    public int GetCarId(string plate)
    {
        return _carRepository.GetCarId(plate);
    }
}
