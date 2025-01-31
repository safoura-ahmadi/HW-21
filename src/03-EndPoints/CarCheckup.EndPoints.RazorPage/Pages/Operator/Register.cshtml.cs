using CarCheckup.Domain.Core.Contarcts.AppService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarCheckup.EndPoints.RazorPage.Pages.Operator
{
    public class RegisterModel(IOperatorAppService operatorAppService) : PageModel
    {
        [BindProperty]
        public Domain.Core.Entities.Operator Operator { get; set; } = null!;
        [BindProperty]
        public string Password { get; set; } = null!;
        public void OnGet()
        {
        }
        public async Task< IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var result = await operatorAppService.Register(Operator, Password, cancellationToken);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "ثبت نام با موفقیت انجام شد";
                    return RedirectToPage("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();

        }
    }
}
