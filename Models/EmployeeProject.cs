using System.ComponentModel.DataAnnotations;

namespace EmployeeMangerAPI.Models
{
    public class EmployeeProject
    {
        [Required(ErrorMessage = "Enter employee ID")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Required(ErrorMessage = "Enter project ID")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}