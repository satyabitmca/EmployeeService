using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Dtos 
{
       public class EmployeeCreateDto 
       {
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