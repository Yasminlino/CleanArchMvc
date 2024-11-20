using CleanArchMvc.Domain.Account;
using CleanArchMvc.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticate _authenticate;  // Corrigido de _authentication para _authenticate

        public AccountController(IAuthenticate authenticate)
        {
            _authenticate = authenticate;
        }

        // Ação para exibir o formulário de registro
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Ação para registrar o usuário
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModels model)
        {
            var result = await _authenticate.RegisterUser(model.Email, model.Password);  // Corrigido para usar _authenticate
            if (result)
                return Redirect("/");
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid registration attempt (password must be strong).");
                return View(model);
            }
        }

        // Ação para exibir o formulário de login
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModels()
            {
                ReturnUrl = returnUrl
            });
        }

        // Ação para autenticar o login do usuário
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModels model)
        {
            var result = await _authenticate.Authenticate(model.Email, model.Password);  // Corrigido para usar _authenticate

            if (result)
            {
                if (string.IsNullOrEmpty(model.ReturnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(model.ReturnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt (password must be strong).");
                return View(model);
            }
        }

        // Ação para fazer logout
        public async Task<IActionResult> Logout()
        {
            await _authenticate.Logout();  // Corrigido para usar _authenticate
            return Redirect("/Account/Login");
        }
    }
}
