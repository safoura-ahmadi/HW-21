using CarCheckup.Domain.Core.Contarcts.AppService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarCheckup.EndPoints.RazorPage.Pages.CheckupRequest
{
    public class MarkAsAcceptedModel(ICheckupRequestAppService checkupRequestAppService) : PageModel
    {
        private readonly ICheckupRequestAppService _checkupRequestAppService = checkupRequestAppService;
        public async Task< IActionResult> OnGet(int id,CancellationToken cancellationToken )
        {
            if (HttpContext.Session.GetString("isLogin") == "True")
            {
                var result =await _checkupRequestAppService.MarkAsAccepted(id,cancellationToken);
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
