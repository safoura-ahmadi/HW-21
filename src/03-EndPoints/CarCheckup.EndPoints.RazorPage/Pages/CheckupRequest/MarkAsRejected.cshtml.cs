using CarCheckup.Domain.Core.Contarcts.AppService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarCheckup.EndPoints.RazorPage.Pages.CheckupRequest
{
    public class MarkAsRejectedModel(ICheckupRequestAppService checkupRequestAppService) : PageModel
    {
        private readonly ICheckupRequestAppService _checkupRequestAppService = checkupRequestAppService;
        public IActionResult OnGet(int id)
        {
            if (HttpContext.Session.GetString("isLogin") == "True")
            {
                var result = _checkupRequestAppService.MarkAsRejected(id);
                if (result.IsSuccess)
                {
                    TempData["SuccessMessage"] = result.Message;
                }
                else
                    TempData["ErrorMessage"] = result.Message;
                return RedirectToPage("Index");
            }
            return RedirectToPage("/Operator/Index");
        }
    }
}
