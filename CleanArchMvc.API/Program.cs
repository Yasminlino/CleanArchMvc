// Program.cs

using CleanArchMvc.Application.Mappings;
using CleanArchMvc.Infra.Data.Identity;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Configurações e serviços
builder.Services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

// Adiciona a camada de infraestrutura e dependências
builder.Services.AddInfrastructureApi(builder.Configuration);

// Adiciona Controllers com suporte para API
builder.Services.AddControllers(); // Sem Views, apenas para API

// Configuração do MongoDB e Identity...
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddUserStore<MongoUserStore>()
    .AddRoleStore<MongoRoleStore>()
    .AddDefaultTokenProviders();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura o pipeline de requisições
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); // Configura as rotas da WebAPI

app.Run();
