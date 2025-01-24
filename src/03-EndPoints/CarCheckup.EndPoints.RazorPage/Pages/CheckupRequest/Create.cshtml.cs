using CarCheckup.Domain.Core.Contarcts.AppService;
using CarCheckup.Domain.Core.Dtos.CheckupRequest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace CarCheckup.EndPoints.RazorPage.Pages.CheckupRequest
{
    public class CreateModel(ICheckupRequestAppService checkupRequestAppService) : PageModel
    {
        private readonly ICheckupRequestAppService _checkupRequestAppService = checkupRequestAppService;
        [BindProperty]
        public GetCheckupRequestDto CheckupRequest { get; set; } = null!;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        [BindProperty]
        public string TimeToDone { get; set; } = null!;
        public IActionResult OnGet()
        {

            var result = _checkupRequestAppService.Create(Id);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
                var checkupRequest = _checkupRequestAppService.GetByCarId(Id);
                if (checkupRequest != null)
                {
                    PersianCalendar pc = new PersianCalendar();

                    // استخراج سال، ماه و روز شمسی
                    var miladiDate = checkupRequest.TimeToDone.ToDateTime(TimeOnly.MinValue);
                    int shamsiYear = pc.GetYear(miladiDate);
                    int shamsiMonth = pc.GetMonth(miladiDate);
                    int shamsiDay = pc.GetDayOfMonth(miladiDate);

                    TimeToDone = $"{shamsiYear}/{shamsiMonth}/{shamsiDay} - {Calendar.DaysOfWeek.GetNameOfDay(checkupRequest.TimeToDone)}";
                   


                    CheckupRequest = checkupRequest;


                    return Page();
                }
                else
                {
                    TempData["ErrorMessage"] = "معاینه فنی با این مشخصات یافت نشد";
                    return RedirectToPage("/Car/Create");
                }

            }
            else
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToPage("/Car/Create");
            }

        }


    }
}
