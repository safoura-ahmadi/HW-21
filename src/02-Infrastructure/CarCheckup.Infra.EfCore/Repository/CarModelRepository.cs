using CarCheckup.Domain.Core.Contarcts.Repository;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Infra.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace CarCheckup.Infra.EfCore.Repository;

public class CarModelRepository(CarCheckupDbContext carCheckupDbContext) : ICarModelRepository
{
    private readonly CarCheckupDbContext _context = carCheckupDbContext;

    public async Task<bool> Create(string name, CancellationToken cancellationToken)
    {
        try
        {
            var item = new CarModel() { Name = name };
            await _context.CarModels.AddAsync(item, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return true;

        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _context.CarModels.FirstAsync(cm => cm.Id == id, cancellationToken);
            _context.CarModels.Remove(item);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<CarModel>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.CarModels.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<CarModel> GetById(int id, CancellationToken cancellationToken)
    {
        return await _context.CarModels.AsNoTracking()
            .FirstAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> UpdateName(int id, string name, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _context.CarModels.FirstAsync(cm => cm.Id == id, cancellationToken);
            item.Name = name;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
