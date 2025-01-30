

using CarCheckup.Domain.Core.Contarcts.Repository;
using CarCheckup.Domain.Core.Dtos.Car;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Infra.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace CarCheckup.Infra.EfCore.Repository;

public class CarRepository(CarCheckupDbContext carCheckupDbContext) : ICarRepository
{
    private readonly CarCheckupDbContext _context = carCheckupDbContext;

    public async Task<int> Create(Car car, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Cars.AddAsync(car, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch { }


        return car.Id;

    }

    public async Task< GetCarDto?> GetById(int id,CancellationToken cancellationToken)
    {
        return await _context.Cars.AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => new GetCarDto
            {
                Company = c.Company,
                GenerationYear = c.GenerationYear
            }).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task< CarDto?> Get(int id, CancellationToken cancellationToken)
    {
        var item = await _context.Cars.AsNoTracking()
              .Where(c => c.Id == id)
              .Select(c => new CarDto
              {

                  Company = c.Company,
                  OwnerMeliCode = c.OwnerMeliCode,
                  OwnerMobile = c.OwnerMobile,
                  Plate = c.Plate,
                  ModelId = c.ModelId,
                  GenerationYear = c.GenerationYear.Year

              }).FirstOrDefaultAsync(cancellationToken);
        return item;

    }

    public async Task< int> GetCarId(string plate,CancellationToken cancellationToken)
    {
        try
        {
            var car = await _context.Cars.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Plate == plate, cancellationToken);
            return car is null ? 0 :car.Id;

        }
        catch
        {
            return 0;
        }
    }
}
