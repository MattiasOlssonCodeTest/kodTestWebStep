using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using FrontendProject.Models;

namespace FrontendProject.Data
{
    public class ApiContext
    {
        private readonly IConfiguration _configuration;
        private readonly string? _connectionString;
        public ApiContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
