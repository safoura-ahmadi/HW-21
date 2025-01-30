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

        public async  Task<IActionResult> OnGet(CancellationToken cancellationToken)
        {

            var result = await _checkupRequestAppService.Create(Id, cancellationToken);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = result.Message;
                var checkupRequest = await _checkupRequestAppService.GetByCarId(Id, cancellationToken);
                if (checkupRequest != null)
                {
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
