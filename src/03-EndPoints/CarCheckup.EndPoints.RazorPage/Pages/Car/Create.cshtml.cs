using CarCheckup.Domain.Core.Contarcts.AppService;
using CarCheckup.Domain.Core.Enums.Car;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CarCheckup.EndPoints.RazorPage.Pages.Car
{
    public class CreateModel(ICarAppService carAppService, ICarModelAppService carModelAppService) : PageModel
    {
        private readonly ICarAppService _carAppService = carAppService;
        private readonly ICarModelAppService _carModelAppService = carModelAppService;
        [BindProperty]
        public Domain.Core.Dtos.Car.CarDto Car { get; set; } = null!;//?
        [BindProperty]
        public List<CarCheckup.Domain.Core.Entities.CarModel> Models { get; set; } = [];
        [BindProperty]
        public List<CarCompanyEnum> Companies { get; set; } = [];
        public async Task OnGet(CancellationToken cancellationToken)
        {
            Companies = Enum.GetValues<CarCompanyEnum>().Cast<CarCompanyEnum>().ToList();
            Models = await _carModelAppService.GetAll(cancellationToken);

        }
        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {

           await LoadPageData(cancellationToken);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                var id = await _carAppService.GetCarId(Car.Plate, cancellationToken);
                if (id != 0)
                {
                    return RedirectToPage("/CheckupRequest/Create", new { id });

                }

                id = await _carAppService.Create(Car, cancellationToken);
                return RedirectToPage("/CheckupRequest/Create", new { id });


            }

        }
        private async Task LoadPageData(CancellationToken cancellationToken)
        {
            Companies = Enum.GetValues<CarCompanyEnum>().Cast<CarCompanyEnum>().ToList();
            Models = await _carModelAppService.GetAll(cancellationToken);
        }
    }
}
