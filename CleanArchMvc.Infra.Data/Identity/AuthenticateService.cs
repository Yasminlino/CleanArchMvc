using CleanArchMvc.Domain.Account;
using CleanArchMvc.Infra.Data.Identity;
using Microsoft.AspNetCore.Identity;

public class AuthenticateService : IAuthenticate
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthenticateService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<bool> Authenticate(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return false; // Usuário não encontrado
        }

        var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
        return result.Succeeded;  // Retorna sucesso ou falha da autenticação
    }

    public async Task<bool> RegisterUser(string email, string password)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email
        };

        var result = await _userManager.CreateAsync(user, password);
        return result.Succeeded; // Retorna se o registro foi bem-sucedido
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }
}
