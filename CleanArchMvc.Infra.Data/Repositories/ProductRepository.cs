using System;
using CleanArchMvc.Application.Context;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaes;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repositories;
// A classe ProductRepository implementa a interface IProductRepository, 
// fornecendo uma implementação específica para o gerenciamento de produtos.
public class ProductRepository : IProductRepository
{
    // Declaração do contexto do banco de dados que será usado para interagir com os produtos.
    ApplicationDbContext _productcontext;

    // Construtor que recebe um ApplicationDbContext, que é injetado na classe.
    // Isso permite que a classe acesse o banco de dados.
    public ProductRepository(ApplicationDbContext context)
    {
        _productcontext = context;
    }

    // Método assíncrono para criar um novo produto no banco de dados.
    // Adiciona o produto ao contexto e salva as alterações.
    public async Task<Product> Create(Product product)
    {
        // Adiciona o produto ao contexto.
        _productcontext.Add(product);
        // Salva as mudanças no banco de dados de forma assíncrona.
        await _productcontext.SaveChangesAsync();
        // Retorna o produto criado.
        return product;
    }

    // Método assíncrono para buscar um produto pelo seu ID.
    // Retorna um produto se encontrado, ou null se não existir.
    public async Task<Product> GetById(int? id)
    {
        // Usa o método FindAsync para localizar o produto com o ID fornecido.
        return await _productcontext.Products.FindAsync(id);
    }

    // Método assíncrono para buscar um produto específico, incluindo suas informações de categoria.
    public async Task<Product> GetProductCategoryAsync(int? id)
    {
        // Usa o método Include para carregar a entidade Category associada ao produto.
        // SingleOrDefaultAsync busca um único produto que corresponda ao ID fornecido.
        // Se nenhum produto for encontrado, retorna null.
        return await _productcontext.Products.Include(c => c.Category)
            .SingleOrDefaultAsync(p => p.Id == id);
    }


    // Método assíncrono para obter todos os produtos do banco de dados.
    // Retorna uma lista de produtos.
    public async Task<IEnumerable<Product>> GetProductAsync()
    {
        // Retorna todos os produtos como uma lista de forma assíncrona.
        return await _productcontext.Products.ToListAsync();
    }

    // Método assíncrono para remover um produto do banco de dados.
    public async Task<Product> Remove(Product product)
    {
        // Remove o produto do contexto.
        _productcontext.Remove(product);
        // Salva as mudanças no banco de dados de forma assíncrona.
        await _productcontext.SaveChangesAsync();
        // Retorna o produto removido.
        return product;
    }

    // Método assíncrono para atualizar um produto existente no banco de dados.
    public async Task<Product> Update(Product product)
    {
        // Atualiza o produto no contexto.
        _productcontext.Update(product);
        // Salva as mudanças no banco de dados de forma assíncrona.
        await _productcontext.SaveChangesAsync();
        // Retorna o produto atualizado.
        return product;
    }
}
