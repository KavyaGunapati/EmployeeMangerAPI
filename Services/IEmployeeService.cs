using EmployeeMangerAPI.DTOs;
using EmployeeMangerAPI.Models;

namespace EmployeeMangerAPI.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetALLEmployees(string? department,  string? order,string? sortBy ,int page=1, int pageSize=5);
        Task<EmployeeDTO?> GetEmployeeById(int id);
        Task<EmployeeDTO> AddEmployee(CreateEmployeeDTO employeeDTO);
        Task<EmployeeDTO?> UpdateEmployee(int id, CreateEmployeeDTO updatedDTO);
        Task<bool> DeleteEmployee(int id);
        Task<bool> AssignProjectToEmployee(int employeeId, int projectId);
        Task<IEnumerable<ProjectDTO>> GetProjectsForEmployee(int employeeId);
        Task<IEnumerable<EmployeeDTO>> SearchEmployeesBySkills(List<string> skills);

    }
}
