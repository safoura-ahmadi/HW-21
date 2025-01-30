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

    public Result Create(int carId)
    {

        if (carId < 0)
            return new Result(false, "خودرویی با این مشخصات وجود ندارد");
        var car = _carService.GetById(carId);
        if (car == null)
            return new Result(false, "خودرویی با این مشخصات وجود ندارد");
        var date = _checkupRequestService.GetLastCheckupDateByCar(carId).Year;
        if (date == DateTime.Now.Year)
            return new Result(false, "تعداد مجاز ثبت معاینه فنی یکبار در سال است");

        DateOnly generationDate = car.GenerationYear;
        if (generationDate.Year == 0)
            return new Result(false, "سال ساخت ماشین یافت نشد");
        if (generationDate.AddYears(5) <= DateOnly.FromDateTime(DateTime.Now))
        {
            _rejectedCheckupRequestService.Create(carId);
            return new Result(false, "ثبت معاینه فنی برای خودرو هایی با سال ساخت بیش از 5 سال مجاز نیست");

        }
        var timeToDone = DetermineTimetoDone(car.Company);
        if (timeToDone < DateOnly.FromDateTime(DateTime.Now))
            return new Result(false, "تاریخ مناسب برای معاینه فنی یافت نشد");
        var item = new CheckupRequest()
        {
            CarId = carId,
            Status = Core.Enums.checkupRequest.CheckUpRequestStatusEnum.Pending,
            TimeToDone = timeToDone

        };
        var result = _checkupRequestService.Create(item);
        if (result)
            return new Result(true, "درخواست شما با موفقیت ثبت شد");
        return new Result(false, "DataBase Error Raised");

    }
    public DateOnly DetermineTimetoDone(CarCompanyEnum carCompany)
    {

        var lastDate = _checkupRequestService.GetLastCheckupDate(carCompany);
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
        while (_checkupRequestService.GetDailyCount(date) >= dailyLimit)
        {
            // اگر محدودیت پر است، ۲ روز اضافه می‌کنیم تا زوج/فرد بودن حفظ شود
            date = DateOnly.FromDateTime(date.ToDateTime(TimeOnly.MinValue).AddDays(2));
        }

        return date;
    }


    public List<GetCheckupRequestDto> GeByCarModel(int modelId)
    {
        if (modelId <= 0)
            return [];
        return _checkupRequestService.GeByCarModel(modelId);
    }

    public List<GetCheckupRequestDto> GetByDate(DateOnly timeToDone)
    {
        if (timeToDone.Year <= 0)
            return [];
        return _checkupRequestService.GetByDate(timeToDone);
    }

    public List<GetCheckupRequestDto> GettAll()
    {
        return _checkupRequestService.GettAll();
    }

    public GetCheckupRequestDto? GetByCarId(int id)
    {
        if (id <= 0)
            return null;
        var checkupRequest = _checkupRequestService.GetByCarId(id);
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
    public Result MarkAsAccepted(int id)
    {
        if (id <= 0)
            return new Result(false, "ایتم انتخاب شده نامعتبر است");
        if (!_checkupRequestService.MarkAsAccepted(id))
            return new Result(false, "این ایدی در دیتا بیس موجود نیست");
        return new Result(true, "وضعیت درخواست معاینه فنی به حالت تایید شده تغییر یافت");

    }

    public Result MarkAsRejected(int id)
    {
        if (id <= 0)
            return new Result(false, "ایتم انتخاب شده نامعتبر است");
        if (!_checkupRequestService.MarkAsRejected(id))
            return new Result(false, "این ایدی در دیتا بیس موجود نیست");
        return new Result(true, "وضعیت درخواست معاینه فنی به حالت رد شده تغییر یافت");
    }

    public bool SetRequestsToIncompleted()
    {
        return _checkupRequestService.SetRequestsToIncompleted();
    }
}
