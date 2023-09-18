using FrontendProject.Models;
using FrontendProject.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace FrontendProject.Controllers
{
    public class TasksController : Controller
    {
        private readonly ILogger<ListsController> _logger;
        private readonly ITaskRepository taskRepository;
        private readonly IListRepository listRepository;

        public TasksController(ILogger<ListsController> logger, ITaskRepository taskRepository,
            IListRepository listRepository)
        {
            _logger = logger;
            this.taskRepository = taskRepository;
            this.listRepository = listRepository;
        }

        public async Task<IActionResult> Index(int listId)
        {
            var list = await listRepository.GetPopulatedList(listId);
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> NewTask(int listID, string name, string description)
        {
            var newTask = new TaskModel
            {
                Name = name,
                Description = description,
                ListId = listID
            };
            await taskRepository.CreateTask(newTask);
            return RedirectToAction("Index", new { listId = listID });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveTask(int Id, int ListId)
        {
            await taskRepository.DeleteTask(Id);
            return RedirectToAction("Index", new { listId = ListId });
        }

        [HttpPost]
        public async Task<IActionResult> MoveTask(int Id, string name, int listId)
        {
            var lists = await listRepository.GetAllLists();
            var listToMoveTo = lists.FirstOrDefault(l => name.Trim().Equals(l.Name));
            if (listToMoveTo != null)
                await taskRepository.MoveTask(Id, listToMoveTo.Id);
            return RedirectToAction("Index", new { listId });
        }

        [HttpPost]
        public async Task<IActionResult> EditTask(TaskModel taskModel)
        {
            await taskRepository.UpdateTask(taskModel);
            return RedirectToAction("Index", new { listId = taskModel.ListId });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
