using AutoMapper;
using EmployeeMangerAPI.Data;
using EmployeeMangerAPI.DTOs;
using EmployeeMangerAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMangerAPI.Services
{
    public class EmployeeService:IEmployeeService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public EmployeeService(AppDbContext context,IMapper mapper) {
              _context = context;
            _mapper = mapper;
        }

        public async Task<EmployeeDTO> AddEmployee(CreateEmployeeDTO employeeDTO)
        {
            var employee=_mapper.Map<Employee>(employeeDTO);
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return _mapper.Map<EmployeeDTO>(employee);
        }

        public Task<bool> AssignProjectToEmployee(int employeeId, int projectId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return false;
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetALLEmployees(string? department,string? order, string? sortBy, int page = 1, int pageSize = 5)
        {
            var query = _context.Employees.AsQueryable();
            //filtering
            if (!string.IsNullOrWhiteSpace(department))
            {
                query=query.Where(e=>e.Department.Contains(department));
            }
            
            //sorting
            var isDesc = order?.ToLower() == "desc";
            query = sortBy switch
            {
                "name" => isDesc ? query.OrderByDescending(e => e.Name):query.OrderBy(e=>e.Name),
                "department"=>isDesc?query.OrderByDescending(e=>e.Department):query.OrderBy(e=>e.Department),
                _=>isDesc?query.OrderByDescending(e=>e.Id):query.OrderBy(e=>e.Id)
            };
            //paging
            query=query.Skip((page-1)*pageSize).Take(pageSize);
            var employees =await query.ToListAsync();
            return _mapper.Map<IEnumerable<EmployeeDTO>>(employees);
        }

        public async Task<EmployeeDTO?> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if(employee == null) return null;
            return _mapper.Map<EmployeeDTO>(employee);
        }

        public Task<IEnumerable<ProjectDTO>> GetProjectsForEmployee(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmployeeDTO>> SearchEmployeesBySkills(List<string> skills)
        {
            throw new NotImplementedException();
        }

        public async Task<EmployeeDTO?> UpdateEmployee(int id, CreateEmployeeDTO updatedDTO)
        {
            var employee = await _context.Employees.FindAsync(id);
            if(employee == null) return null;
            _mapper.Map(updatedDTO, employee);
            await _context.SaveChangesAsync();
            return _mapper.Map<EmployeeDTO>(employee);

        }
    }
}
