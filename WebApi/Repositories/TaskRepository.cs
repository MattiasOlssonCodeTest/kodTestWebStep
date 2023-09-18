using Dapper;
using System.Data;
using FrontendProject.Data;
using FrontendProject.Models;

namespace FrontendProject.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApiContext context;

        public TaskRepository(ApiContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<TaskModel>> GetTasksByListId(int TableId)
        {
            var query = "SELECT * Tasks WHERE TableId = @ListId;";

            var parameters = new DynamicParameters();
            parameters.Add("ListId", TableId, DbType.Int32);


            using (var connection = context.CreateConnection())
            {
                var companies = await connection.QueryAsync<TaskModel>(query, parameters);
                return companies.ToList();
            }
        }

        public async Task<TaskModel> GetTask(int id)
        {
            var query = "SELECT * Tasks WHERE Id = @Id;";

            using (var connection = context.CreateConnection())
            {
                var TaskModel = await connection.QuerySingleOrDefaultAsync<TaskModel>(query, new { id });
                return TaskModel;
            }
        }

        public async Task CreateTask(TaskModel TaskModel)
        {
            var query = "INSERT INTO Tasks (Name, Description) VALUES (@Name, @Description);";

            var parameters = new DynamicParameters();
            parameters.Add("Name", TaskModel.Name, DbType.String);
            parameters.Add("Description", TaskModel.Description, DbType.String);

            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task UpdateTask(int id, TaskModel TaskModel)
        {
            var query = "UPDATE Tasks SET Name=@Name, Description=@Description WHERE Id = @Id;";
            var parameters = new DynamicParameters();
            parameters.Add("Name", TaskModel.Name, DbType.String);
            parameters.Add("Description", TaskModel.Description, DbType.String);
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task DeleteTask(int id)
        {
            var query = "DELETE Tasks WHERE Id = @Id";
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task MoveTask(int id, int toList)
        {
            var query = "SET foreign_key_checks = 0; " +
                "UPDATE Tasks SET ListId=@toList WHERE Id = @Id;" +
                "SET foreign_key_checks = 1;";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.String);
            parameters.Add("toList", toList, DbType.String);
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
