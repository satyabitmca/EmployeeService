using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeService.Models;

namespace EmployeeService.Data 
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly AppDbContext _context;

        public EmployeeRepo(AppDbContext context)
         {
             _context = context;
         }

     
        public void CreateEmployee(Employee plat)
        {

          if(plat == null) {
              throw new ArgumentNullException(nameof(plat));
          }
          _context.Employees.Add(plat);

        }

        public void DeleteEmployee(Employee employee)
        {
             _context.Employees.Remove(employee);
             _context.SaveChanges();        }

        public Employee EditEmployees(Employee employee)
        {
            var existingEmployee = _context.Employees.Find(employee.Id);
            if(existingEmployee !=null)
            {
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.MiddleName = employee.MiddleName;
                existingEmployee.Age = employee.Age;
                existingEmployee.Gender = employee.Gender;

                _context.Employees.Update(existingEmployee);             
                _context.SaveChanges();
            }
            return employee;  
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _context.Employees.ToList();
        }

        public Employee GetEmployeeById(int id)
        {
           return _context.Employees.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges() {

           return (_context.SaveChanges() >= 0);
        }
    
    }

}