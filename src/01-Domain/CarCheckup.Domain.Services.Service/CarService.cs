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
    public async Task<int> Create(Car car, CancellationToken cancellationToken)
    {
        return await _carRepository.Create(car, cancellationToken);
    }

    public async Task<GetCarDto?> GetById(int id, CancellationToken cancellationToken)
    {
        return await _carRepository.GetById(id, cancellationToken);
    }

    public async Task<CarDto?> Get(int id, CancellationToken cancellationToken)
    {
        return await _carRepository.Get(id, cancellationToken);
    }

    public async Task<int> GetCarId(string plate, CancellationToken cancellationToken)
    {
        return await _carRepository.GetCarId(plate, cancellationToken);
    }
}
