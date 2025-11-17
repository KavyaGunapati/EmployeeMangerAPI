using AutoMapper;
using EmployeeMangerAPI.DTOs;
using EmployeeMangerAPI.Models;

namespace EmployeeMangerAPI.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<CreateEmployeeDTO, Employee>();
            CreateMap<Project, ProjectDTO>();
            CreateMap<CreateProjectDTO, Project>();
        }
    }
}
