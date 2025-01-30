using CarCheckup.Domain.Core.Contarcts.AppService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CarCheckup.EndPoints.RazorPage.Pages.CarModel
{
    public class DeleteModel(ICarModelAppService carModelAppService) : PageModel
    {
        private readonly ICarModelAppService _carModelAppService = carModelAppService;
        public async Task< IActionResult> OnGet(int id,CancellationToken cancellationToken)
        {
            if (HttpContext.Session.GetString("isLogin") == "True")
            {
                var result = await _carModelAppService.Delete(id, cancellationToken);
                if (result.IsSuccess)
                {
                    TempData["SuccessMessage"] = result.Message;
                    return RedirectToPage("Index");
                }
                TempData["ErrorMessage"] = result.Message;
                return RedirectToPage("Index");


            }
            return RedirectToPage("/Operator/Index");
        }
    }
}
