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
        public async Task< IActionResult> OnGet(CancellationToken cancellationToken)
        {
            if (HttpContext.Session.GetString("isLogin") == "True")
            {
                CarModels = await _carModelAppService.GetAll(cancellationToken);
                return Page();
            }
            return RedirectToPage("/Operator/Index");
        }
    }
}
