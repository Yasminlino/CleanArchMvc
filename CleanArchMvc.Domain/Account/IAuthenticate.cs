using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CleanArchMvc.Domain.Account;
public interface IAuthenticate
{
    Task<bool> Authenticate(string email, string password);  // Retorna um bool indicando sucesso
    Task<bool> RegisterUser(string email, string password); // Retorna um bool indicando sucesso
    Task Logout();
}
