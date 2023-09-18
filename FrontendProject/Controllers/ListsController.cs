using FrontendProject.Models;
using FrontendProject.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FrontendProject.Controllers
{
    public class ListsController : Controller
    {
        private readonly ILogger<ListsController> _logger;
        private readonly IListRepository listRepository;

        public ListsController(ILogger<ListsController> logger, IListRepository listRepository)
        {
            _logger = logger;
            this.listRepository = listRepository;
        }

        public async Task<IActionResult> Index()
        {
            var allLists = await listRepository.GetAllLists();
            return View(allLists);
        }

        [HttpPost]
        public async Task<IActionResult> NewList(string name)
        {
            await listRepository.CreateList(name);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveList(int Id)
        {
            await listRepository.DeleteList(Id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}   