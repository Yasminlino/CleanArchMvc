using System;
using System.Reflection.Metadata;

namespace CleanArchMvc.Domain.Account;

public interface IAuthentication
{
    Task<BlobBuilder> Authenticate(string email, string password);
    Task<BlobBuilder> RegisterUser(string email, string password);
}
