using CarCheckup.Domain.Core.Contarcts.Repository;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Infra.EfCore.Common;

namespace CarCheckup.Infra.EfCore.Repository;

public class RejectedCheckupRequestRepository(CarCheckupDbContext carCheckupDbContext) : IRejectedCheckupRequestRepository
{
    private readonly CarCheckupDbContext _context = carCheckupDbContext;
    public void Create(int carId)
    {
        var item = new RejectedCheckupRequest()
        { CarId = carId };
        _context.Add(item);
        _context.SaveChanges();
    }
}
