using CarCheckup.Domain.Core.Contarcts.Repository;
using CarCheckup.Infra.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace CarCheckup.Infra.EfCore.Repository;

public class OperatorRepository(CarCheckupDbContext carCheckupDbContext) : IOperatorRepository
{
    private readonly CarCheckupDbContext _context = carCheckupDbContext;
    public async Task<bool> Login(string username, string password, CancellationToken cancellationToken)
    {
        return await _context.Operators.AsNoTracking()
             .AnyAsync(o => o.Username == username && o.Password == password, cancellationToken);
    }
}
