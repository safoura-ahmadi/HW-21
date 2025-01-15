using CarCheckup.Domain.Core.Contarcts.Repository;
using CarCheckup.Domain.Core.Contarcts.Service;
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

    public int GetGenerationYearById(int id)
    {
        return _carRepository.GetGenerationYearById(id);
    }

    public bool IsCarIdValid(int id)
    {
       return _carRepository.IsCarIdValid(id);
    }

    public bool IsPlateValid(string plate)
    {
        return _carRepository.IsPlateValid(plate);
    }
}
