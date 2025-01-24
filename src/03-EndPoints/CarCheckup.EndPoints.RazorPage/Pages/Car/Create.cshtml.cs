using CarCheckup.Domain.Core.Contarcts.AppService;
using CarCheckup.Domain.Core.Enums.Car;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CarCheckup.EndPoints.RazorPage.Pages.Car
{
    public class CreateModel(ICarAppService carAppService,ICarModelAppService carModelAppService) : PageModel
    {
        private readonly ICarAppService _carAppService = carAppService;
        private readonly ICarModelAppService _carModelAppService = carModelAppService;
        [BindProperty]
        public Domain.Core.Entities.Car Car { get; set; } = null!;//?
        [BindProperty]
        public List<CarCheckup.Domain.Core.Entities.CarModel> Models { get; set; } = [];
        [BindProperty]
        public List<CarCompanyEnum> Companies { get; set; } = [];
        [BindProperty]
        [Required(ErrorMessage = "سال شمسی نمی‌تواند خالی باشد.")]
        [Range(1340, 1403, ErrorMessage = "سال شمسی باید بین 1340 تا 1403 باشد.")]
        public int ShamsiYear { get; set; }
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
                var pc = new PersianCalendar();
                var shamiDate = pc.ToDateTime(ShamsiYear, 1, 1, 0, 0, 0, 0);
                var miladiDate = shamiDate.ToUniversalTime();
                Car.GenerationYear = DateOnly.FromDateTime(miladiDate);
                var result = _carAppService.Create(Car);
                if(!result.IsSuccess)
                {
                    TempData["ErrorMessage"] = result.Message;
                   
                }
                else
                {

                    return RedirectToPage("/CheckupRequest/Create", new { id = Car.Id });

                }
            }
            return Page();
           

        }
        private void LoadPageData()
        {
            Companies = Enum.GetValues<CarCompanyEnum>().Cast<CarCompanyEnum>().ToList();
            Models = _carModelAppService.GetAll();
        }
    }
}
