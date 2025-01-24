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
    public bool Create(CheckupRequest checkupRequest)
    {
        try
        {
            _context.CheckupRequests.Add(checkupRequest);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public List<GetCheckupRequestDto> GeByCarModel(int modelId)
    {
        //return [.. _context.CheckupRequests.AsNoTracking()
        //         .Where(cr => cr.TimeToDone == timeToDone)
        //        .Include(cr => cr.Car)
        //        .Include(c => c.Car.Model)


        //return [.. _context.CheckupRequests.AsNoTracking()
        //     .Select(cr => new GetCheckupRequestDto
        //    {
        //        Id = cr.Id,
        //        Company = cr.Car.Company,
        //        OwnerMeliCode = cr.Car.OwnerMeliCode,
        //        ModelName = cr.Car.Model.Name,
        //        Status = cr.Status,
        //        TimeToDone = cr.TimeToDone,


        //    }).Where(??)];
        return [.. _context.CheckupRequests.AsNoTracking()
            .Include(cr => cr.Car)
            .ThenInclude(c => c.Model)
            .Where(cr => cr.Car.ModelId == modelId)
            .Select(cr => new GetCheckupRequestDto
            {
                Id = cr.Id,
                Company = cr.Car.Company,
                OwnerMeliCode = cr.Car.OwnerMeliCode,
                ModelName = cr.Car.Model.Name,
                Status = cr.Status,
                TimeToDone = cr.TimeToDone,


            })];
    }
    public List<GetCheckupRequestDto> GetByDate(DateOnly timeToDone)
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
        return [.. _context.CheckupRequests.AsNoTracking()
                 .Where(cr => cr.TimeToDone == timeToDone)
                 .Select(cr => new GetCheckupRequestDto
                 {
                        Id = cr.Id,
                        Company = cr.Car.Company,
                        OwnerMeliCode = cr.Car.OwnerMeliCode,
                        ModelName = cr.Car.Model.Name,
                        Status = cr.Status,
                        TimeToDone = cr.TimeToDone,


                  })];
    }
    public bool MarkAsAccepted(int id)
    {
        try
        {
            var item = _context.CheckupRequests.Find(id);
            if (item == null)
                return false;
            item.Status = CheckUpRequestStatusEnum.Accepted;
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }

    }
    public DateOnly? GetLastCheckupDate(CarCompanyEnum carCompany)
    {
        try
        {
            //return _context.CheckupRequests
            //    .OrderByDescending(cr => cr.TimeToDone)
            //    .Select(cr => cr.TimeToDone)
            //    .FirstOrDefault();
            var c = _context.CheckupRequests.AsNoTracking()
                .OrderByDescending(cr => cr.TimeToDone)
                .FirstOrDefault(cr => cr.Car.Company == carCompany);
            var t = c.TimeToDone;
            return t;
        }
        catch
        {
            return null;
        }
    }
    public int GetDailyCount(DateOnly date)
    {
        try
        {
            //return _context.CheckupRequests
            //    .AsNoTracking()
            //    .Count(cr => cr.TimeToDone == date);
            return _context.CheckupRequests.AsNoTracking().Where(cr => cr.TimeToDone == date).Count();
        }
        catch
        {
            return 0;
        }
    }
    public DateOnly GetLastCheckupDateByCar(int carId)
    {
        try
        {
            //return _context.CheckupRequests.AsNoTracking()
            //  .Where(cr => cr.CarId == carId)
            //  .OrderByDescending(cr => cr.TimeToDone)
            //  .Select(cr => cr.TimeToDone)
            //  .FirstOrDefault();
            return _context.CheckupRequests.AsNoTracking()
                
                .Where(cr => cr.CarId == carId).OrderByDescending(cr => cr.TimeToDone).First()
                .TimeToDone;

        }
        catch
        {
            return new(1, 1, 1);
        }
    }
    public bool SetRequestsToIncompleted()
    {
        try
        {
            _context.CheckupRequests
                .Where(cr => cr.TimeToDone < DateOnly.FromDateTime(DateTime.Now))
                .ExecuteUpdate(update => update.SetProperty(cr => cr.Status, CheckUpRequestStatusEnum.Rejected));
            return true;

            // نیازی به SaveChanges نیست، ExecuteUpdate تغییرات را مستقیم در دیتابیس ذخیره می‌کند.
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public List<GetCheckupRequestDto> GettAll()
    {
        return [.. _context.CheckupRequests.AsNoTracking()
            .Select(cr => new GetCheckupRequestDto
            {
                Id = cr.Id,
                ModelName = cr.Car.Model.Name,
                OwnerMeliCode = cr.Car.OwnerMeliCode,
                Company = cr.Car.Company,
                Status = cr.Status,
                TimeToDone = cr.TimeToDone
            }

            )];
    }

    public bool MarkAsRejected(int id)
    {
        try
        {
            //var entry = _context.Entry(new CheckupRequest { Id = id }); 
            //entry.Property(e => e.Status).CurrentValue = CheckUpRequestStatusEnum.InCompleted;
            //_context.SaveChanges(); 
            //return true;
            var item = _context.CheckupRequests.Find(id);
            item.Status = CheckUpRequestStatusEnum.Rejected;
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public GetCheckupRequestDto? GetByCarId(int id)
    {
        return _context.CheckupRequests.AsNoTracking()
            .Where(cr => cr.Car.Id == id).OrderByDescending(cr => cr.TimeToDone)
            .Select(cr => new GetCheckupRequestDto
            {

                Id = cr.Id,
                ModelName = cr.Car.Model.Name,
                 Plate = cr.Car.Plate,
                OwnerMeliCode = cr.Car.OwnerMeliCode,
                Company = cr.Car.Company,
                Status = cr.Status,
                TimeToDone = cr.TimeToDone
            }

            ).FirstOrDefault();
    }
}
