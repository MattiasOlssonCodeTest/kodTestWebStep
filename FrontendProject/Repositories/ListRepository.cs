using Dapper;
using System.Data;
using FrontendProject.Data;
using FrontendProject.Models;

namespace FrontendProject.Repositories
{
    public class ListRepository : IListRepository
    {
        private readonly ISqlQueryHandler queryHandler;
        private readonly ITaskRepository taskRepository;

        public ListRepository(ISqlQueryHandler context, ITaskRepository taskRepository)
        {
            this.queryHandler = context;
            this.taskRepository = taskRepository;
        }

        public async Task<IEnumerable<ListModel>> GetAllLists()
        {
            var query = "SELECT * FROM  Lists;";
            return await queryHandler.QueryListResult<ListModel>(query, new DynamicParameters());
        }

        public async Task<ListModel?> GetPopulatedList(int id)
        {
            var query = "SELECT * FROM Lists WHERE Id = @Id;";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.String);
            var foundTaskModel = await queryHandler.QuerySingle<ListModel>(query, parameters);
            if (foundTaskModel == null)
                return null;
            var tasks = await taskRepository.GetTasksByListId(foundTaskModel.Id);
            foundTaskModel.TaskModels = tasks;
            return foundTaskModel;
        }

        public async Task<ListModel> CreateList(string name)
        {
            var query = "INSERT INTO Lists (Name) VALUES (@Name);" +
                "SELECT * FROM Lists WHERE Name=@Name";
            var parameters = new DynamicParameters();
            parameters.Add("Name", name, DbType.String);
            return await queryHandler.QuerySingle<ListModel>(query, parameters);
        }

        public async Task RenameList(int id, string newName)
        {
            var query = "UPDATE Lists SET Name=@NewName WHERE Id = @Id;";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.String);
            parameters.Add("NewName", newName, DbType.String);
            await queryHandler.Execute(query, parameters);
        }
        public async Task DeleteList(int id)
        {
            var query = "DELETE Lists WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.String);
            await queryHandler.Execute(query, parameters);
        }
    }
}
