

using CarCheckup.Domain.Core.Contarcts.Repository;
using CarCheckup.Domain.Core.Dtos.Car;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Infra.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace CarCheckup.Infra.EfCore.Repository;

public class CarRepository(CarCheckupDbContext carCheckupDbContext) : ICarRepository
{
    private readonly CarCheckupDbContext _context = carCheckupDbContext;

    public int Create(Car car)
    {
        _context.Cars.Add(car);
        _context.SaveChanges();
        return car.Id;
    }

    public GetCarDto? GetById(int id)
    {
        return _context.Cars.AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => new GetCarDto
            {
                Company = c.Company,
                GenerationYear = c.GenerationYear
            }).FirstOrDefault();
    }

    public CarDto? Get(int id)
    {
        var item = _context.Cars.AsNoTracking()
              .Where(c => c.Id == id)
              .Select(c => new CarDto
              {

                  Company = c.Company,
                  OwnerMeliCode = c.OwnerMeliCode,
                  OwnerMobile = c.OwnerMobile,
                  Plate = c.Plate,
                  ShamsiYear = c.GenerationYear.Year

              }).FirstOrDefault();
        return item;

    }

    public int GetCarId(string plate)
    {
        try
        {
            return _context.Cars.AsNoTracking()
                .First(c => c.Plate == plate).Id;
        }
        catch
        {
            return 0;
        }
    }
}
