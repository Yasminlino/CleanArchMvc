using MongoDB.Driver;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.Identity
{
    public class MongoRoleStore : IRoleStore<IdentityRole>
    {
        private readonly IMongoCollection<IdentityRole> _rolesCollection;

        // Construtor
        public MongoRoleStore(IMongoDatabase database)
        {
            _rolesCollection = database.GetCollection<IdentityRole>("Roles"); // A coleção de roles no MongoDB
        }

        // Criação de um novo role
        public async Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            try
            {
                await _rolesCollection.InsertOneAsync(role, cancellationToken: cancellationToken);
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }

        // Deletar um role
        public async Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _rolesCollection.DeleteOneAsync(r => r.Id == role.Id, cancellationToken);
                return result.DeletedCount > 0 ? IdentityResult.Success : IdentityResult.Failed(new IdentityError { Description = "Role not found." });
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }

        // Disposição de recursos
        public void Dispose()
        {
            // O MongoDB não requer um Dispose explícito, mas se você precisar liberar recursos, pode implementar aqui.
        }

        // Encontrar role por ID
        public async Task<IdentityRole?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return await _rolesCollection.Find(r => r.Id == roleId).FirstOrDefaultAsync(cancellationToken);
        }

        // Encontrar role por nome
        public async Task<IdentityRole?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return await _rolesCollection.Find(r => r.Name == normalizedRoleName).FirstOrDefaultAsync(cancellationToken);
        }

        // Obter nome normalizado do role
        public Task<string?> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        // Obter ID do role
        public Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id);
        }

        // Obter nome do role
        public Task<string?> GetRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        // Definir nome normalizado do role
        public Task SetNormalizedRoleNameAsync(IdentityRole role, string? normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            return Task.CompletedTask;
        }

        // Definir nome do role
        public Task SetRoleNameAsync(IdentityRole role, string? roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            return Task.CompletedTask;
        }

        // Atualizar um role existente
        public async Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _rolesCollection.ReplaceOneAsync(r => r.Id == role.Id, role, cancellationToken: cancellationToken);
                return result.MatchedCount > 0 ? IdentityResult.Success : IdentityResult.Failed(new IdentityError { Description = "Role not found." });
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }
    }
}
