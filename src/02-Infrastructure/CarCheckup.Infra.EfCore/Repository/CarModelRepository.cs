using CarCheckup.Domain.Core.Contarcts.Repository;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Infra.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace CarCheckup.Infra.EfCore.Repository;

public class CarModelRepository(CarCheckupDbContext carCheckupDbContext) : ICarModelRepository
{
    private readonly CarCheckupDbContext _context = carCheckupDbContext;

    public bool Create(string name)
    {
        try
        {
            var item = new CarModel() { Name = name };
            _context.CarModels.Add(item);
            _context.SaveChanges();
            return true;

        }
        catch
        {
            return false;
        }
    }

    public bool Delete(int id)
    {
        try
        {
            var item = _context.CarModels.Find(id);
            if (item != null)
                _context.CarModels.Remove(item);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public List<CarModel> GetAll()
    {
        return [.. _context.CarModels.AsNoTracking()];
    }

    public CarModel GetById(int id)
    {
        return _context.CarModels.AsNoTracking()
            .First(x => x.Id == id);
    }

    public bool UpdateName(int id, string name)
    {
        try
        {
            var item = _context.CarModels.Find(id);
            if (item != null)
                item.Name = name;
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
