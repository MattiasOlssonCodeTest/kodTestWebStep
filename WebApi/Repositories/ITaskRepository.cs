using FrontendProject.Models;

namespace FrontendProject.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskModel>> GetTasksByListId(int listId);
        Task<TaskModel> GetTask(int id);
        Task CreateTask(TaskModel company);
        Task UpdateTask(int id, TaskModel company);
        Task DeleteTask(int id);
        Task MoveTask(int taskId, int toList);
    }
}
