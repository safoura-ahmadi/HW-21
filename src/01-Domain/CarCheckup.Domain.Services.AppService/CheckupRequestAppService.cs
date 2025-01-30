using Calendar;
using CarCheckup.Domain.Core.Contarcts.AppService;
using CarCheckup.Domain.Core.Contarcts.Service;
using CarCheckup.Domain.Core.Dtos.CheckupRequest;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Entities.Configs;
using CarCheckup.Domain.Core.Enums.Car;
using System.ComponentModel.Design;
using System.Globalization;
using System.Security.AccessControl;

namespace CarCheckup.Domain.Services.AppService;

public class CheckupRequestAppService(ICheckupRequestService checkupRequestService, IRejectedCheckupRequestService rejectedCheckupRequestService, ICarService carService, SiteSettings siteSettings) : ICheckupRequestAppService
{
    private readonly ICheckupRequestService _checkupRequestService = checkupRequestService;
    private readonly IRejectedCheckupRequestService _rejectedCheckupRequestService = rejectedCheckupRequestService;
    private readonly ICarService _carService = carService;
    private readonly SiteSettings _siteSettings = siteSettings;

    public async Task<Result> Create(int carId, CancellationToken cancellationToken)
    {

        if (carId < 0)
            return new Result(false, "خودرویی با این مشخصات وجود ندارد");
        var car = await _carService.GetById(carId, cancellationToken);
        if (car == null)
            return new Result(false, "خودرویی با این مشخصات وجود ندارد");
        var date = await _checkupRequestService.GetLastCheckupDateByCar(carId, cancellationToken);
        if (date.Year == DateTime.Now.Year)
            return new Result(false, "تعداد مجاز ثبت معاینه فنی یکبار در سال است");

        DateOnly generationDate = car.GenerationYear;
        if (generationDate.Year == 0)
            return new Result(false, "سال ساخت ماشین یافت نشد");
        if (generationDate.AddYears(5) <= DateOnly.FromDateTime(DateTime.Now))
        {
            await _rejectedCheckupRequestService.Create(carId, cancellationToken);
            return new Result(false, "ثبت معاینه فنی برای خودرو هایی با سال ساخت بیش از 5 سال مجاز نیست");

        }
        var timeToDone = await DetermineTimetoDone(car.Company, cancellationToken);
        if (timeToDone < DateOnly.FromDateTime(DateTime.Now))
            return new Result(false, "تاریخ مناسب برای معاینه فنی یافت نشد");
        var item = new CheckupRequest()
        {
            CarId = carId,
            Status = Core.Enums.checkupRequest.CheckUpRequestStatusEnum.Pending,
            TimeToDone = timeToDone

        };
        var result = await _checkupRequestService.Create(item, cancellationToken);
        if (result)
            return new Result(true, "درخواست شما با موفقیت ثبت شد");
        return new Result(false, "DataBase Error Raised");

    }
    public async Task<DateOnly> DetermineTimetoDone(CarCompanyEnum carCompany, CancellationToken cancellationToken)
    {

        var lastDate = await _checkupRequestService.GetLastCheckupDate(carCompany, cancellationToken);
        var currentDate = DateOnly.FromDateTime(DateTime.Now);

        // اگر تاریخ قبلی وجود نداشته باشد، از تاریخ امروز استفاده می‌شود
        var date = lastDate ?? currentDate;

        // تعیین تنظیمات شرکت
        bool isIranKhodro = carCompany == CarCompanyEnum.IranKhodro;
        bool isEvenDayRequired = isIranKhodro; // ایران‌خودرو فقط روزهای زوج
        bool isOddDayRequired = !isIranKhodro; // سایپا فقط روزهای فرد

        // بررسی زوج یا فرد بودن روز
        bool isCurrentDayEven = DaysOfWeek.GetDayType(currentDate) == "Even";

        if (isEvenDayRequired && !isCurrentDayEven || (date < DateOnly.FromDateTime(DateTime.Now)))
        {

            date = DaysOfWeek.FindNextEvenDay(date);
        }
        else if (isOddDayRequired && (isCurrentDayEven || date.DayOfWeek == DayOfWeek.Friday || date < DateOnly.FromDateTime(DateTime.Now)))
        {
            date = DaysOfWeek.FindNextOddDay(date);
        }


        // دریافت محدودیت روزانه برای شرکت
        int dailyLimit = isIranKhodro
            ? _siteSettings.Limitation.IranKhodro
            : _siteSettings.Limitation.Saipa;

        // بررسی محدودیت‌ها و تنظیم تاریخ
        while (await _checkupRequestService.GetDailyCount(date, cancellationToken) >= dailyLimit)
        {
            // اگر محدودیت پر است، ۲ روز اضافه می‌کنیم تا زوج/فرد بودن حفظ شود
            date = DateOnly.FromDateTime(date.ToDateTime(TimeOnly.MinValue).AddDays(2));
        }

        return date;
    }


    public async Task<List<GetCheckupRequestDto>> GeByCarModel(int modelId, CancellationToken cancellationToken)
    {
        if (modelId <= 0)
            return [];
        return await _checkupRequestService.GeByCarModel(modelId, cancellationToken);
    }

    public async Task<List<GetCheckupRequestDto>> GetByDate(DateOnly timeToDone, CancellationToken cancellationToken)
    {
        if (timeToDone.Year <= 0)
            return [];
        return await _checkupRequestService.GetByDate(timeToDone, cancellationToken);
    }

    public async Task<List<GetCheckupRequestDto>> GettAll(CancellationToken cancellationToken)
    {
        return await _checkupRequestService.GettAll(cancellationToken);
    }

    public async Task<GetCheckupRequestDto?> GetByCarId(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return null;
        var checkupRequest = await _checkupRequestService.GetByCarId(id, cancellationToken);
        if (checkupRequest != null)
        {

            var timeToDone = checkupRequest.TimeToDone;
            var pertionTimeToDone = ConvertDateToPersion(timeToDone);
            checkupRequest.PersionTimeToDone = pertionTimeToDone;

        }
        return checkupRequest;
    }
    public string ConvertDateToPersion(DateOnly date)
    {
        PersianCalendar pc = new();

        var miladiDate = date.ToDateTime(TimeOnly.MinValue);
        int shamsiYear = pc.GetYear(miladiDate);
        int shamsiMonth = pc.GetMonth(miladiDate);
        int shamsiDay = pc.GetDayOfMonth(miladiDate);
        var result = $"{shamsiYear}/{shamsiMonth}/{shamsiDay} - {Calendar.DaysOfWeek.GetNameOfDay(date)}";
        return result;
    }
    public async Task<Result> MarkAsAccepted(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return new Result(false, "ایتم انتخاب شده نامعتبر است");
        if (!await _checkupRequestService.MarkAsAccepted(id, cancellationToken))
            return new Result(false, "این ایدی در دیتا بیس موجود نیست");
        return new Result(true, "وضعیت درخواست معاینه فنی به حالت تایید شده تغییر یافت");

    }

    public async Task<Result> MarkAsRejected(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return new Result(false, "ایتم انتخاب شده نامعتبر است");
        if (!await _checkupRequestService.MarkAsRejected(id, cancellationToken))
            return new Result(false, "این ایدی در دیتا بیس موجود نیست");
        return new Result(true, "وضعیت درخواست معاینه فنی به حالت رد شده تغییر یافت");
    }

    public async Task<bool> SetRequestsToIncompleted(CancellationToken cancellationToken)
    {
        return await _checkupRequestService.SetRequestsToIncompleted(cancellationToken);
    }
}
