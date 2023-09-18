using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FrontendProject.Models
{
    public class ListModel
    {
        [Key]
        [HiddenInput]
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<TaskModel> TaskModels { get; set; } = new List<TaskModel>();
    }
}
