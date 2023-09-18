using System.ComponentModel.DataAnnotations;

namespace FrontendProject.Models
{
    public class ListModel
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<TaskModel> TaskModels { get; set; } = new List<TaskModel>();
    }
}
