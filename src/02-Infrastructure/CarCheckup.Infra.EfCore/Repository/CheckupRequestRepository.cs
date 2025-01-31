using CarCheckup.Domain.Core.Contarcts.Repository;
using CarCheckup.Domain.Core.Dtos.CheckupRequest;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Enums.Car;
using CarCheckup.Domain.Core.Enums.checkupRequest;
using CarCheckup.Infra.EfCore.Common;
using Microsoft.EntityFrameworkCore;

namespace CarCheckup.Infra.EfCore.Repository;

public class CheckupRequestRepository(CarCheckupDbContext carCheckupDbContext) : ICheckupRequestRepository
{
    private readonly CarCheckupDbContext _context = carCheckupDbContext;
    public async Task<bool> Create(CheckupRequest checkupRequest, CancellationToken cancellationToken)
    {
        try
        {
            await _context.CheckupRequests.AddAsync(checkupRequest, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public async Task<List<GetCheckupRequestDto>> GeByCarModel(int modelId, CancellationToken cancellationToken)
    {

        return await _context.CheckupRequests
        .AsNoTracking()
        .Include(cr => cr.Car)
        .ThenInclude(c => c!.Model)
        .Where(cr => cr.Car!.ModelId == modelId)
        .Select(cr => new GetCheckupRequestDto
        {
            Id = cr.Id,
            Company = cr.Car!.Company,
            OwnerMeliCode = cr.Car.OwnerMeliCode,
            ModelName = cr.Car.Model!.Name,
            Status = cr.Status,
            TimeToDone = cr.TimeToDone,
        })
        .ToListAsync(cancellationToken);
    }
    public async Task<List<GetCheckupRequestDto>> GetByDate(DateOnly timeToDone, CancellationToken cancellationToken)
    {

        //return [.. _context.CheckupRequests.AsNoTracking()
        //         .Where(cr => cr.TimeToDone == timeToDone)
        //        .Include(cr => cr.Car)
        //        .ThenInclude(c => c.Model)
        //        .Select(cr => new GetCheckupRequestDto
        //        {
        //            Id = cr.Id,
        //            Company = cr.Car.Company,
        //            OwnerMeliCode = cr.Car.OwnerMeliCode,
        //            ModelName = cr.Car.Model.Name,
        //            Status = cr.Status,
        //            TimeToDone = cr.TimeToDone,


        //        })];
        return await _context.CheckupRequests
        .AsNoTracking()
        .Where(cr => cr.TimeToDone == timeToDone)
        .Include(cr => cr.Car)
        .ThenInclude(c => c!.Model)
        .Select(cr => new GetCheckupRequestDto
        {
            Id = cr.Id,
            Company = cr.Car!.Company,
            OwnerMeliCode = cr.Car.OwnerMeliCode,
            ModelName = cr.Car.Model!.Name,
            Status = cr.Status,
            TimeToDone = cr.TimeToDone,
        })
        .ToListAsync(cancellationToken);
    }
    public async Task<bool> MarkAsAccepted(int id, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _context.CheckupRequests.FindAsync(id, cancellationToken);
            if (item == null)
                return false;
            item.Status = CheckUpRequestStatusEnum.Accepted;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }

    }
    public async Task<DateOnly?> GetLastCheckupDate(CarCompanyEnum carCompany, CancellationToken cancellationToken)
    {
        try
        {
            //return _context.CheckupRequests
            //    .OrderByDescending(cr => cr.TimeToDone)
            //    .Select(cr => cr.TimeToDone)
            //    .FirstOrDefault();
            var c = await _context.CheckupRequests.AsNoTracking()
                .OrderByDescending(cr => cr.TimeToDone)
                .FirstOrDefaultAsync(cr => cr.Car!.Company == carCompany, cancellationToken);
            var t = c!.TimeToDone;
            return t;
        }
        catch
        {
            return null;
        }
    }
    public async Task<int> GetDailyCount(DateOnly date, CancellationToken cancellationToken)
    {
        try
        {

            return await _context.CheckupRequests.AsNoTracking().Where(cr => cr.TimeToDone == date).CountAsync(cancellationToken);
        }
        catch
        {
            return 0;
        }
    }
    public async Task<DateOnly> GetLastCheckupDateByCar(int carId, CancellationToken cancellationToken)
    {
        try
        {
            var lastCheckup = await _context.CheckupRequests
            .AsNoTracking()
            .Where(cr => cr.CarId == carId)
            .OrderByDescending(cr => cr.TimeToDone)
            .Select(cr => cr.TimeToDone)
            .FirstOrDefaultAsync(cancellationToken);

            return lastCheckup;

        }
        catch
        {
            return new(1, 1, 1);
        }
    }
    public async Task<bool> SetRequestsToIncompleted(CancellationToken cancellationToken)
    {
        try
        {
            await _context.CheckupRequests
                 .Where(cr => cr.TimeToDone < DateOnly.FromDateTime(DateTime.Now))
                 .ExecuteUpdateAsync(update => update.SetProperty(cr => cr.Status, CheckUpRequestStatusEnum.Rejected), cancellationToken);
            return true;

            // نیازی به SaveChanges نیست، ExecuteUpdate تغییرات را مستقیم در دیتابیس ذخیره می‌کند.
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<GetCheckupRequestDto>> GettAll(CancellationToken cancellationToken)
    {
        return await _context.CheckupRequests
       .AsNoTracking()
       .Select(cr => new GetCheckupRequestDto
       {
           Id = cr.Id,
           ModelName = cr.Car!.Model!.Name,
           OwnerMeliCode = cr.Car.OwnerMeliCode,
           Company = cr.Car.Company,
           Status = cr.Status,
           TimeToDone = cr.TimeToDone
       })
       .ToListAsync(cancellationToken);
    }

    public async Task<bool> MarkAsRejected(int id, CancellationToken cancellationToken)
    {
        try
        {
            //var entry = _context.Entry(new CheckupRequest { Id = id }); 
            //entry.Property(e => e.Status).CurrentValue = CheckUpRequestStatusEnum.InCompleted;
            //_context.SaveChanges(); 
            //return true;
            var item = await _context.CheckupRequests.FindAsync(id, cancellationToken);
            if (item is not null)
                item.Status = CheckUpRequestStatusEnum.Rejected;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<GetCheckupRequestDto?> GetByCarId(int id, CancellationToken cancellationToken)
    {
        return await _context.CheckupRequests.AsNoTracking()
            .Where(cr => cr.Car!.Id == id).OrderByDescending(cr => cr.TimeToDone)
            .Select(cr => new GetCheckupRequestDto
            {

                Id = cr.Id,
                ModelName = cr.Car!.Model!.Name,
                Plate = cr.Car.Plate,
                OwnerMeliCode = cr.Car.OwnerMeliCode,
                Company = cr.Car.Company,
                Status = cr.Status,
                TimeToDone = cr.TimeToDone
            }

            ).FirstOrDefaultAsync(cancellationToken);
    }
}
