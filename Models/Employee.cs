using System.ComponentModel.DataAnnotations;

namespace EmployeeMangerAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Department is Required")]
        public string Department { get; set; }

        [MinLength(1, ErrorMessage = "Enter at least one skill")]
        public List<string> Skills { get; set; } = new List<string>();

        public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
    }
}