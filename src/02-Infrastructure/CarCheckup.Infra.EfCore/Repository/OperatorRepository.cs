using CarCheckup.Domain.Core.Contarcts.Repository;
using CarCheckup.Infra.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace CarCheckup.Infra.EfCore.Repository;

public class OperatorRepository(CarCheckupDbContext carCheckupDbContext) : IOperatorRepository
{
    private readonly CarCheckupDbContext _context = carCheckupDbContext;
    public bool Login(string username, string password)
    {
        return _context.Operators.AsNoTracking()
             .Any(o => o.Username == username && o.Password == password);
    }
}
