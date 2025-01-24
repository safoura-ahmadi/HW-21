using CarCheckup.Domain.Core.Contarcts.AppService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarCheckup.EndPoints.RazorPage.Pages.CarModel
{
    public class CreateModel(ICarModelAppService carModelAppService) : PageModel
    {
        private readonly ICarModelAppService _carModelAppService = carModelAppService;
        [BindProperty]
        public string Name { get; set; } = null!;
        public IActionResult OnGet(int id)
        {
            if (HttpContext.Session.GetString("isLogin") == "True")
            {

                return Page();
            }
            return RedirectToPage("/Operator/Index");
        }
        public IActionResult OnPost()
        {
            if (HttpContext.Session.GetString("isLogin") == "True")
            {
                var result = _carModelAppService.Create(Name);
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
