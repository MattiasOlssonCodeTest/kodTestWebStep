using Dapper;
using System.Data;
using FrontendProject.Data;
using FrontendProject.Models;

namespace FrontendProject.Repositories
{
    public class DapperRepository : ISqlQueryHandler
    {
        private readonly ApiContext context;

        public DapperRepository(ApiContext context)
        {
            this.context = context;
        }

        public async Task<List<T>> QueryListResult<T>(string query, DynamicParameters parameters)
        {
            using var connection = context.CreateConnection();
            var results = await connection.QueryAsync<T>(query, parameters);
            return results.ToList();
        }

        public async Task<T?> QuerySingle<T>(string query, DynamicParameters parameters)
        {
            using var connection = context.CreateConnection();
            T results;
            try
            {
                results = await connection.QuerySingleOrDefaultAsync<T>(query, parameters);
            }
            catch (InvalidOperationException)
            {
                return default;
            }
            return results;
            
        }

        public async Task Execute(string query, DynamicParameters parameters)
        {
            using var connection = context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }

    }
}
