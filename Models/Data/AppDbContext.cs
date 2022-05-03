using Microsoft.EntityFrameworkCore;
using EmployeeService.Models;

namespace EmployeeService.Data 
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base (opt)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }  

    }
}