using CarCheckup.Domain.Core.Contarcts.AppService;
using CarCheckup.Domain.Core.Contarcts.Service;
using CarCheckup.Domain.Core.Dtos;
using CarCheckup.Domain.Core.Entities;
using CarCheckup.Domain.Core.Entities.Configs;
using CarCheckup.Domain.Core.Enums.Car;
using System.Globalization;
using System.Security.AccessControl;

namespace CarCheckup.Domain.Services.AppService;

public class CheckupRequestAppService(ICheckupRequestService checkupRequestService, IRejectedCheckupRequestService rejectedCheckupRequestService, ICarService carService, SiteSettings siteSettings) : ICheckupRequestAppService
{
    private readonly ICheckupRequestService _checkupRequestService = checkupRequestService;
    private readonly IRejectedCheckupRequestService? _rejectedCheckupRequestService = rejectedCheckupRequestService;
    private readonly ICarService _carService = carService;
    private readonly SiteSettings _siteSettings = siteSettings;

    public Result Create(int carId, DateOnly timeToDone)
    {
        if (carId < 0 || _carService.IsCarIdValid(carId))
            return new Result(false, "ماشین نامعتبر است");

        if (_checkupRequestService.GetLastCheckupDateByCar(carId).Year == DateTime.Now.Year)
            return new Result(false, "تعداد مجاز ثبت معاینه فنی یکبار در سال است");

        int persionYear = _carService.GetGenerationYearById(carId);
        int gregorianDate = DateOnly.FromDateTime(DateTime.Now).Year;
        PersianCalendar persianCalendar = new();
        DateTime gregorianFromPersian = persianCalendar.ToDateTime(persionYear, 1, 1, 0, 0, 0, 0);
        int convertedGregorianYear = gregorianFromPersian.Year;
        int difference = gregorianDate - convertedGregorianYear;
        if(difference > 5)
        {
            _rejectedCheckupRequestService.Create(carId); //?
            return new Result(false, "ثبت معاینه فنی برای خودرو هایی با سال ساخت بیش از 5 سال مجاز نیست");

        }
        if (timeToDone.Year <= 0)
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
        bool isCurrentDayEven = date.Day % 2 == 0;

        if ((isEvenDayRequired && !isCurrentDayEven) || (isOddDayRequired && isCurrentDayEven))
        {
            // تنظیم تاریخ به روز مناسب (روز زوج برای ایران‌خودرو و فرد برای سایپا)
            date = DateOnly.FromDateTime(date.ToDateTime(TimeOnly.MinValue).AddDays(1));
        }

        // دریافت محدودیت روزانه برای شرکت
        int dailyLimit = isIranKhodro
            ? _siteSettings.Limitation.IranKhodro
            : _siteSettings.Limitation.Saipa;

        // بررسی محدودیت‌ها و تنظیم تاریخ
        while (_checkupRequestService.GetDailyCount(date) >= dailyLimit)
        {
            // اگر محدودیت پر است، ۲ روز اضافه می‌کنیم تا زوج/فرد بودن حفظ شود
            date = DateOnly.FromDateTime( date.ToDateTime(TimeOnly.MinValue).AddDays(2));
        }

        return date;
    }


    public List<GetCheckupRequestDto> GeByCarModel(int modelId)
    {
        return _checkupRequestService.GeByCarModel(modelId);
    }

    public List<GetCheckupRequestDto> GetByDate(DateOnly timeToDone)
    {
        return _checkupRequestService.GetByDate(timeToDone);
    }

    public string MarkAsCompleted(int id)
    {
        if (!_checkupRequestService.MarkAsCompleted(id))
            return "DataBase Error Raised";
        return "وضعیت درخواست معاینه فنی به حالت اتمام تغییر یافت";

    }

    public bool SetRequestsToIncompleted()
    {
        return _checkupRequestService.SetRequestsToIncompleted();
    }
}
