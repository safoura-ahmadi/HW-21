﻿using CarCheckup.Domain.Core.Contarcts.AppService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarCheckup.EndPoints.RazorPage.Pages.Operator
{
    public class IndexModel(IOperatorAppService operatorAppService) : PageModel
    {
        private readonly IOperatorAppService _operatoAppService = operatorAppService;
        [BindProperty]
        public string UserName { get; set; } = null!;
        [BindProperty]
        public string Password { get; set; } = null!;

        public void OnGet()
        {

            if (HttpContext.Session.GetString("isLogin") == "True")
            {
                HttpContext.Session.Remove("isLogin");
            }
        }
        public IActionResult OnPost()
        {
            bool isLogin = _operatoAppService.Login(UserName, Password);
            if (isLogin)
            {
                HttpContext.Session.SetString("isLogin", "True");
                return RedirectToPage("Menue");
            }
            TempData["ErrorMessage"] = "نام کاربری یا رمز عبور اشتباه است";
            return Page();


        }
    }
}
