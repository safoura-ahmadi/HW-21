using CarCheckup.Domain.Core.Contarcts.Repository;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Infra.EfCore.Common;

namespace CarCheckup.Infra.EfCore.Repository;

public class RejectedCheckupRequestRepository(CarCheckupDbContext carCheckupDbContext) : IRejectedCheckupRequestRepository
{
    private readonly CarCheckupDbContext _context = carCheckupDbContext;
    public async Task Create(int carId, CancellationToken cancellationToken)
    {
        var item = new RejectedCheckupRequest()
        { CarId = carId };
        await _context.AddAsync(item, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
