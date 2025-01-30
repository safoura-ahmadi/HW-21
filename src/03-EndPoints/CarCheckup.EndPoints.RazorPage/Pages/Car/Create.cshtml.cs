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
        public void OnGet()
        {
            Companies = Enum.GetValues<CarCompanyEnum>().Cast<CarCompanyEnum>().ToList();
            Models = _carModelAppService.GetAll();

        }
        public IActionResult OnPost()
        {

            LoadPageData();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                var id = _carAppService.GetCarId(Car.Plate);
                if (id != 0)
                {
                    return RedirectToPage("/CheckupRequest/Create", new { id });

                }

                id = _carAppService.Create(Car);
                return RedirectToPage("/CheckupRequest/Create", new { id });


            }

        }
        private void LoadPageData()
        {
            Companies = Enum.GetValues<CarCompanyEnum>().Cast<CarCompanyEnum>().ToList();
            Models = _carModelAppService.GetAll();
        }
    }
}
