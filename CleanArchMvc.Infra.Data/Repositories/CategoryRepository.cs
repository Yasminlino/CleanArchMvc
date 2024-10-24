using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CleanArchMvc.Infra.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly IMongoCollection<Category> _categoryCollection;

    public CategoryRepository(IMongoClient mongoClient, IOptions<MongoDbSettings> settings)
    {
        var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        _categoryCollection = database.GetCollection<Category>("Categories");
    }

    public async Task<Category> Create(Category category)
    {
        await _categoryCollection.InsertOneAsync(category);
        return category;
    }

    public async Task<Category> GetById(int? id)
    {
        return await _categoryCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
        return await _categoryCollection.Find(c => true).ToListAsync();
    }

    public async Task<Category> Remove(Category category)
    {
        await _categoryCollection.DeleteOneAsync(c => c.Id == category.Id);
        return category;
    }

    public async Task<Category> Update(Category category)
    {
        await _categoryCollection.ReplaceOneAsync(c => c.Id == category.Id, category);
        return category;
    }
}
