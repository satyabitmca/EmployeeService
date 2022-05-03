using EmployeeService.Models;
using System.Collections.Generic;
namespace EmployeeService.Data 
{

    public interface IEmployeeRepo 
    {
        bool SaveChanges();
        IEnumerable<Employee> GetAllEmployee();
        Employee GetEmployeeById(int id);
        void CreateEmployee(Employee plat);
        void DeleteEmployee(Employee employee);
        Employee EditEmployees(Employee employee);
    }

}