using CarCheckup.Domain.Core.Contarcts.AppService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarCheckup.EndPoints.RazorPage.Pages.CarModel
{
    public class IndexModel(ICarModelAppService carModelAppService) : PageModel
    {
        private readonly ICarModelAppService _carModelAppService = carModelAppService;
        [BindProperty]
        public List<CarCheckup.Domain.Core.Entities.CarModel> CarModels { get; set; } = null!;
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("isLogin") == "True")
            {
                CarModels = _carModelAppService.GetAll();
                return Page();
            }
            return RedirectToPage("/Operator/Index");
        }
    }
}
