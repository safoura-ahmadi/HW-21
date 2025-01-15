

using CarCheckup.Domain.Core.Contarcts.Repository;
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

    public int GetGenerationYearById(int id)
    {
        try
        {
           // return _context.Cars.Where(c => c.Id == id).Select(c => c.GenerationYear).FirstOrDefault();
            return _context.Cars.AsNoTracking().First(c => c.Id == id).GenerationYear;
        }
        catch
        {
            return 0;
        }
    }

    public bool IsPlateValid(string plate)
    {
        if (_context.Cars.AsNoTracking().Any(c => c.Plate == plate))
            return false;
        return true;
    }
    public bool IsCarIdValid(int id)
    {
        return _context.Cars.AsNoTracking().Any(c => c.Id == id);
    }
}
