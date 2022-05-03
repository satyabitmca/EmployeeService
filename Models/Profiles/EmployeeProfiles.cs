using AutoMapper;
using EmployeeService.Models;
using EmployeeService.Dtos;

namespace EmployeeService.Profiles 
{
    public class EmployeeProfile : Profile 
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeReadDto>();
            CreateMap<EmployeeCreateDto, Employee>();
        }

    }
}