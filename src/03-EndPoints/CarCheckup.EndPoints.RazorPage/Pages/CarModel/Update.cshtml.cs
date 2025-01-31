using CarCheckup.Domain.Core.Contarcts.AppService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarCheckup.EndPoints.RazorPage.Pages.CarModel
{
    [Authorize]
    public class UpdateModel(ICarModelAppService carModelAppService) : PageModel
    {
        private readonly ICarModelAppService _carModelAppService = carModelAppService;
        [BindProperty]
        public CarCheckup.Domain.Core.Entities.CarModel CarModel { get; set; } = null!;

        public async Task<IActionResult> OnGet(int id,CancellationToken cancellationToken)
        {
            if (HttpContext.Session.GetString("isLogin") == "True")
            {
                var item = await _carModelAppService.GetById(id, cancellationToken);
                if (item == null)
                {
                    return RedirectToPage("Index");
                }
                CarModel = item;
                return Page();
            }
            return RedirectToPage("/Operator/Index");
        }
        public async Task< IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (HttpContext.Session.GetString("isLogin") == "True")
            {
                var result = await _carModelAppService.UpdateName(CarModel.Id, CarModel.Name,cancellationToken);
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
