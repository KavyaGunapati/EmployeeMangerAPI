using EmployeeMangerAPI.DTOs;
using EmployeeMangerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMangerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees(string? department,
            string? order, string? sortBy, int page = 1, int pageSize = 5)
        {
            var employees = await _employeeService.GetALLEmployees(department, order, sortBy, page, pageSize);
            return Ok(employees);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee == null) return NotFound($"Employee with ID {id} not found.");
            return Ok(employee);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] CreateEmployeeDTO employeeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var employee = await _employeeService.AddEmployee(employeeDto);
            return CreatedAtAction(nameof(AddEmployee), new { id = employee.Id }, employee);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] CreateEmployeeDTO updatedDto)
        {
            var updatedEmployee = await _employeeService.UpdateEmployee(id, updatedDto);
            if (updatedEmployee == null) return NotFound($"Employee with ID {id} not found.");
            return Ok(updatedEmployee);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeService.DeleteEmployee(id);
            if (!result) return NotFound($"Employee with ID {id} not found.");
            return NoContent();
        }
        [HttpGet("{employeeId}/assign-project/{projectId}")]

        public async Task<IActionResult> AssignProjectToEmployee(int employeeId, int projectId)
        {
            var result = await _employeeService.AssignProjectToEmployee(employeeId, projectId);
            if (!result) return BadRequest("Employee or Project not found or already assigned.");
            return Ok($"Employee {employeeId} assigned to Project {projectId}");
        }
        [HttpGet("{employeeId}/projects")]
        public async Task<IActionResult> GetProjectsForEmployee(int employeeId)
        {

            var projects = await _employeeService.GetProjectsForEmployee(employeeId);
            if (!projects.Any()) return NotFound("No projects found for this employee.");
            return Ok(projects);
    }
        [HttpGet("search")]

        public async Task<IActionResult> SearchEmployeesBySkills([FromQuery] string skills)
        {
            var skillList = skills.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
            var employees = await _employeeService.SearchEmployeesBySkills(skillList);
            return Ok(employees);
        }
        }
    }
