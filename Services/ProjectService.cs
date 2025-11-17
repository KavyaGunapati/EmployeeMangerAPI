using AutoMapper;
using EmployeeMangerAPI.Data;
using EmployeeMangerAPI.DTOs;
using EmployeeMangerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMangerAPI.Services
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ProjectService(AppDbContext context, IMapper mapper) {
             _context = context;
            _mapper = mapper;
        }
        public async Task<ProjectDTO> AddProject(CreateProjectDTO projectDTO)
        {
            var project= _mapper.Map<Project>(projectDTO);
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProjectDTO>(project);
        }

        public async Task<bool> DeleteProject(int id)
        {
            var project=await _context.Projects.FindAsync(id);
            if(project == null) return false;
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProjectDTO>> GetAllProjects(string? title, string? sortBy, string? order, int pageNumber = 1, int pageSize = 5)
        {
            var query = _context.Projects.AsQueryable();
            if (!string.IsNullOrEmpty(title))
            {
                query=query.Where(p=>p.Title.Contains( title)); 
            }
            var isDesc=order?.ToLower()=="desc";
            //sorting
            query = sortBy switch
            {
                "title" => isDesc ? query.OrderByDescending(p => p.Title) : query.OrderBy(p => p.Title),
                _ => isDesc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id)
            };
            //paging
            query=query.Skip((pageNumber-1) * pageSize).Take(pageSize);
            var projects = await query.ToListAsync();
            return _mapper.Map<IEnumerable<ProjectDTO>>(projects);
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesForProject(int projectId)
        {
            var employees = await _context.EmployeesProjects.Where(ep => ep.ProjectId == projectId).Select(ep => ep.Employee).ToListAsync();
            return _mapper.Map<IEnumerable<EmployeeDTO>>(employees);
        }

        public async Task<ProjectDTO?> GetProjectById(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return null;
            return _mapper.Map<ProjectDTO>(project);

        }

        public async Task<ProjectDTO?> UpdateProject(int id, CreateProjectDTO updatedprojectDTO)
        {
            var project=await _context.Projects.FindAsync(id);
            if (project == null) return null;
            _mapper.Map(updatedprojectDTO, project);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProjectDTO>(project);
        }
    }
}
