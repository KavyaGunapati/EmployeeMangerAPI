using EmployeeMangerAPI.DTOs;

namespace EmployeeMangerAPI.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDTO>> GetAllProjects(string? title, string? sortBy, string? order, int pageNumber = 1, int pageSize = 5);
        Task<ProjectDTO?> GetProjectById(int id);
        Task<ProjectDTO> AddProject(CreateProjectDTO projectDTO);
        Task<ProjectDTO?> UpdateProject(int id,CreateProjectDTO updatedprojectDTO);
        Task<bool> DeleteProject(int id);
        Task<IEnumerable<EmployeeDTO>> GetEmployeesForProject(int projectId);
    }
}
