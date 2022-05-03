using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Models 
{
    public class Employee
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        public string MiddleName { get; set; } 
       
       [Required]
        public int Age { get; set; }
        
        [Required]
        public string Gender { get; set; } 

    }
}