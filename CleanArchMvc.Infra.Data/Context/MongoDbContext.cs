using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Infra.Data.Configurations;
using CleanArchMvc.Infra.Data.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CleanArchMvc.Infra.Data.Context;
public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    // Agora injetamos o IMongoDatabase diretamente
    public MongoDbContext(IMongoDatabase database)
    {
        _database = database ?? throw new ArgumentNullException(nameof(database), "MongoDb database cannot be null.");
    }

    public IMongoDatabase Database => _database;
}
