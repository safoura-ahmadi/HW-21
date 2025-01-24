using CarCheckup.Domain.Core.Contarcts.AppService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CarCheckup.EndPoints.RazorPage.Pages.CarModel
{
    public class DeleteModel(ICarModelAppService carModelAppService) : PageModel
    {
        private readonly ICarModelAppService _carModelAppService = carModelAppService;
        public IActionResult OnGet(int id)
        {
            if (HttpContext.Session.GetString("isLogin") == "True")
            {
                var result = _carModelAppService.Delete(id);
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
