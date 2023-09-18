using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FrontendProject.Models;
using FrontendProject.Data;

namespace FrontendProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ListsApiController : ControllerBase
    {
        private readonly ApiContext _context;

        public ListsApiController(ApiContext context)
        {
            _context = context;
        }
        /*
        [HttpPost]
        public JsonResult CreateEdit(ListModel listModel)
        {
            if (listModel.Id == 0)
            {
                _context.Lists.Add(listModel);
            }
            else
            {
                var listModelInDb = _context.Lists.Find(listModel.Id);
                if (listModelInDb == null)
                    return new JsonResult(NotFound());

                listModelInDb = listModel;
            }

            _context.SaveChanges();

            return new JsonResult(Ok(listModel));
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.Lists.Find(id);
            if(result == null)
                return new JsonResult(NotFound());
            return new JsonResult(result);
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Lists.Find(id);
            if (result == null)
                return new JsonResult(NotFound());
            _context.Lists.Remove(result);
            _context.SaveChanges();
            return new JsonResult(NoContent());
        }

        [HttpGet]
        public JsonResult GetAll() 
        {
            var result = _context.Lists.ToList();
            return new JsonResult(Ok(result));
        }
        */
    }
}
