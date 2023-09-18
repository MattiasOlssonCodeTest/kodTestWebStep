using Dapper;

namespace FrontendProject.Repositories
{
    public interface ISqlQueryHandler
    {
        public Task<List<T>> QueryListResult<T>(string query, DynamicParameters parameters);
        public Task<T?> QuerySingle<T>(string query, DynamicParameters parameters);
        public Task Execute(string query, DynamicParameters parameters);
    }
}
