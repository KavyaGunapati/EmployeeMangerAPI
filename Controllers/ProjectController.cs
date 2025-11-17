using EmployeeMangerAPI.DTOs;
using EmployeeMangerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMangerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProjects( string? title, string? sortby,  string? order,
             int page = 1,  int pageNumber = 5)
        {
            var projects = await _projectService.GetAllProjects(title, sortby, order, page, pageNumber);
            return Ok(projects);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPeojectById(int id)
        {
            var project = await _projectService.GetProjectById(id);
            if (project == null) return NotFound($"Project with ID {id} not found.");
            return Ok(project);
        }
        [HttpPost]
        public async Task<IActionResult> AddProject([FromBody] CreateProjectDTO projectDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var project = await _projectService.AddProject(projectDTO);
            return CreatedAtAction(nameof(AddProject), new { id = project.Id }, project);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] CreateProjectDTO updateProjectDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updatedProject = await _projectService.UpdateProject(id, updateProjectDTO);
            if (updatedProject == null) return NotFound($"Project with ID {id} not found.");
            return Ok(updatedProject);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var result = await _projectService.DeleteProject(id);
            if (!result) return NotFound($"Project with ID {id} not found.");
            return NoContent();
        }
    }
}
