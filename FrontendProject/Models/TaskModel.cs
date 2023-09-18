using System.ComponentModel.DataAnnotations;
using FrontendProject.Repositories;

namespace FrontendProject.Models
{
    public class TaskModel
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int ListId { get; set; }
    }
}
