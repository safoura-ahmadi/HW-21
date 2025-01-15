using CarCheckup.Domain.Core.Contarcts.Repository;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Infra.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace CarCheckup.Infra.EfCore.Repository;

public class CarModelRepository(CarCheckupDbContext carCheckupDbContext) : ICarModelRepository
{
    private readonly CarCheckupDbContext _context = carCheckupDbContext;
    public List<CarModel> GetAll()
    {
        return [.. _context.CarModels.AsNoTracking()];
    }
}
