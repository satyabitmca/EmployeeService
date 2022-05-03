using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using EmployeeService.Data;
using EmployeeService.Dtos;
using EmployeeService.Models;

namespace EmployeeService.Controllers 
{
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepo _repo;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
      
        [HttpGet]
        public ActionResult<IEnumerable<EmployeeReadDto>> GetEmployees()
        {
            Console.WriteLine("...Getting employees...");
            var employees = _repo.GetAllEmployee();

            return Ok(_mapper.Map<IEnumerable<EmployeeReadDto>>(employees));

        }

        [HttpGet("{id}", Name="getemployeebyid")]
        public ActionResult<IEnumerable<EmployeeReadDto>> GetEmployeeById(int id) 
        {  
               var employee = _repo.GetEmployeeById(id);
               if(employee !=null) 
               {
                   return Ok(_mapper.Map<EmployeeReadDto>(employee));

               }
               else
               {
                   return NotFound();
               }
        }

        [HttpPost]
        public  ActionResult<EmployeeReadDto> CreateEmployee( [FromBody] EmployeeCreateDto employeeCreateDto ) 
        {
             var employeeModel = _mapper.Map<Employee>(employeeCreateDto);
             _repo.CreateEmployee(employeeModel);
             _repo.SaveChanges();

             var platformReadDto = _mapper.Map<EmployeeReadDto>(employeeModel);
             return CreatedAtRoute(nameof(GetEmployeeById), new {Id = platformReadDto.Id},platformReadDto);

        }

        [HttpDelete]
        [Route("{id}", Name="deleteemployee")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _repo.GetEmployeeById(id);
            if (employee != null)
            {
                _repo.DeleteEmployee(employee);
                return Ok($"Employee with the id: {id} deleted successfully");
            }
            return NotFound($"Employee with the id: {id} was not found");

        }

        [HttpPatch]
        public IActionResult EditEmployee( [FromBody]Employee employee)
        {

             Console.WriteLine("...EditEmployee...");
            var existingEmployee = _repo.GetEmployeeById(employee.Id);
            if (existingEmployee != null)
            {
                 employee.Id = existingEmployee.Id;
                _repo.EditEmployees(employee);
                return Ok(employee);
            }
         
            return NotFound($"Employee with the id: {employee.Id} was not found");

        }

    }
}