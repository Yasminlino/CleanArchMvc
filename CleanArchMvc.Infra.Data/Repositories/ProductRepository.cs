using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Configurations;
using Microsoft.Extensions.Options; // Não esqueça de importar este namespace
using MongoDB.Driver;

namespace CleanArchMvc.Infra.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _productCollection;

    public ProductRepository(IMongoClient mongoClient, IOptions<MongoDbSettings> settings)
    {
        var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        _productCollection = database.GetCollection<Product>("Products");
    }


    public async Task<Product> Create(Product product)
    {
        await _productCollection.InsertOneAsync(product);
        return product;
    }

    public async Task<Product> GetById(int? id)
    {
        return await _productCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductAsync()
    {
        return await _productCollection.Find(p => true).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductCategoryAsync(int? id)
    {
        return await _productCollection.Find(p => p.CategoryId == id).ToListAsync();
    }

    public async Task<Product> Remove(Product product)
    {
        await _productCollection.DeleteOneAsync(p => p.Id == product.Id);
        return product;
    }

    public async Task<Product> Update(Product product)
    {
        await _productCollection.ReplaceOneAsync(p => p.Id == product.Id, product);
        return product;
    }
}
