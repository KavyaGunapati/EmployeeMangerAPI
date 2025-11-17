using System.ComponentModel.DataAnnotations;

namespace EmployeeMangerAPI.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Project Title Required")]
        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
    }
}