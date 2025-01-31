using CarCheckup.Domain.Core.Contarcts.AppService;
using CarCheckup.Domain.Core.Contarcts.Service;
using CarCheckup.Domain.Core.Dtos.CheckupRequest;
using CarCheckup.Domain.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace CarCheckup.EndPoints.RazorPage.Pages.CheckupRequest
{
    [Authorize]
    public class IndexModel(ICheckupRequestAppService checkupRequestAppService) : PageModel
    {
        private readonly ICheckupRequestAppService _checkupRequestAppService = checkupRequestAppService;

        [BindProperty]
        public List<GetCheckupRequestDto> CheckupRequests { get; set; } = null!;
        [BindProperty(SupportsGet = true)]
        public string? SearchQuery { get; set; }
        public async Task< IActionResult> OnGet(CancellationToken cancellationToken)
        {
            if (HttpContext.Session.GetString("isLogin") == "True")
            {


                if (!string.IsNullOrWhiteSpace(SearchQuery))
                {
                    try
                    {
                        var pc = new PersianCalendar();
                        int year = int.Parse(SearchQuery.Substring(6));
                        int month = int.Parse((SearchQuery.Substring(3, 2)));
                        int day = int.Parse(SearchQuery.Substring(0, 2));
                        var shamiDate = pc.ToDateTime(year, month, day, 0, 0, 0, 0);
                        var date = DateOnly.FromDateTime(shamiDate);
                        CheckupRequests = await _checkupRequestAppService.GetByDate(DateOnly.FromDateTime(DateTime.Now),cancellationToken);
                        return Page();
                    }
                    catch
                    {
                        CheckupRequests = await _checkupRequestAppService.GettAll(cancellationToken);
                        return Page();
                    }
                }
                CheckupRequests = await _checkupRequestAppService.GettAll(cancellationToken);
                return Page();

            }
            return RedirectToPage("/Operator/Index");
        }
    }
}
