using Dapper;
using System.Data;
using FrontendProject.Data;
using FrontendProject.Models;
using System.Reflection.Metadata.Ecma335;

namespace FrontendProject.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ISqlQueryHandler queryHandler;

        public TaskRepository(ISqlQueryHandler context)
        {
            this.queryHandler = context;
        }

        public async Task<List<TaskModel>> GetTasksByListId(int TableId)
        {
            var query = "SELECT * FROM Tasks WHERE ListId = @ListId;";
            var parameters = new DynamicParameters();
            parameters.Add("ListId", TableId, DbType.Int32);
            return await queryHandler.QueryListResult<TaskModel>(query, parameters);
        }

        public async Task<TaskModel?> GetTask(int id)
        {
            var query = "SELECT * FROM Tasks WHERE Id = @Id;";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.String);
            return await queryHandler.QuerySingle<TaskModel>(query, parameters);
        }

        public async Task CreateTask(TaskModel TaskModel)
        {
            var query = "INSERT INTO Tasks (Name, Description, ListId) VALUES (@Name, @Description, @ListId);";
            var parameters = new DynamicParameters();
            parameters.Add("Name", TaskModel.Name, DbType.String);
            parameters.Add("Description", TaskModel.Description, DbType.String);
            parameters.Add("ListId", TaskModel.ListId, DbType.String);
            await queryHandler.Execute(query, parameters);
        }

        public async Task UpdateTask(TaskModel TaskModel)
        {
            var query = "UPDATE Tasks SET Name=@Name, Description=@Description WHERE Id = @Id;";
            var parameters = new DynamicParameters();
            parameters.Add("Name", TaskModel.Name, DbType.String);
            parameters.Add("Description", TaskModel.Description, DbType.String);
            parameters.Add("Id", TaskModel.Id, DbType.Int32);
            await queryHandler.Execute(query, parameters);
        }

        public async Task DeleteTask(int id)
        {
            var query = "DELETE Tasks WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.String);
            await queryHandler.Execute(query, parameters);
        }

        public async Task MoveTask(int id, int toList)
        {
            var query = " ALTER TABLE Tasks NOCHECK CONSTRAINT FK_Tasks_ToLists; " +
                " UPDATE Tasks SET ListId = @toList WHERE Id = @id; " +
                " ALTER TABLE Tasks CHECK CONSTRAINT FK_Tasks_ToLists; ";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32);
            parameters.Add("toList", toList, DbType.Int32);
            await queryHandler.Execute(query, parameters);
        }

    }
}
