using CarCheckup.Domain.Core.Contarcts.AppService;
using CarCheckup.Domain.Core.Contarcts.Service;
using CarCheckup.Domain.Core.Dtos.CheckupRequest;
using CarCheckup.Domain.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarCheckup.EndPoints.RazorPage.Pages.CheckupRequest
{
    public class IndexModel(ICheckupRequestAppService checkupRequestAppService) : PageModel
    {
        private readonly ICheckupRequestAppService _checkupRequestAppService = checkupRequestAppService;

        [BindProperty]
        public List<GetCheckupRequestDto> CheckupRequests { get; set; } = null!;
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("isLogin") == "True")
            {
                CheckupRequests = _checkupRequestAppService.GettAll();
                return Page();
            }
            return RedirectToPage("/Operator/Index");
        }
    }
}
