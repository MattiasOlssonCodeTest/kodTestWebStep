using FrontendProject.Models;

namespace FrontendProject.Repositories
{
    public interface ITaskRepository
    {
        Task<List<TaskModel>> GetTasksByListId(int listId);
        Task<TaskModel?> GetTask(int id);
        Task CreateTask(TaskModel company);
        Task UpdateTask(TaskModel company);
        Task DeleteTask(int id);
        Task MoveTask(int taskId, int toList);
    }
}
