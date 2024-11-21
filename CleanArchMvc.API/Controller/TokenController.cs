using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CleanArchMvc.API.DTO;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IAuthenticate _authenticate;
    private readonly IConfiguration _configuration;

    public TokenController(IAuthenticate authenticate, IConfiguration configuration)
    {
        _authenticate = authenticate;
        _configuration = configuration;
    }

    [HttpPost("CreateUser")]
    public async Task<ActionResult> CreateUser([FromBody] LoginDTO userInfo)
    {
        var result = await _authenticate.RegisterUser(userInfo.Email, userInfo.Password);
        if (result)
        {
            return Ok($"User {userInfo.Email} was created successfully");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Registration failed");
            return BadRequest(ModelState);
        }
    }

    [HttpPost("LoginUser")]
    public async Task<ActionResult<UserToken>> Login([FromBody] LoginDTO userInfo)
    {
        var result = await _authenticate.Authenticate(userInfo.Email, userInfo.Password);
        if (result)
        {
            return GenerateToken(userInfo.Email);  // Passando o email para o método GenerateToken
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return BadRequest(ModelState);
        }
    }

    private ActionResult<UserToken> GenerateToken(string email)
    {
        // Declarações do usuário
        var claims = new[]
        {
            new Claim("email", email),  // Usando o email do usuário autenticado
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Gerar chave privada para assinar o token
        var privateKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

        // Gerar assinatura digital 
        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

        // Definir o tempo de expiração
        var expiration = DateTime.UtcNow.AddMinutes(10);

        // Gerar o token
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],  // Ajustando o nome da chave "Jwt:Issuer"
            audience: _configuration["Jwt:Audience"],  // Ajustando o nome da chave "Jwt:Audience"
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

        return new UserToken()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
    }
}
