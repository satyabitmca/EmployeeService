using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using EmployeeService.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Data 
{

 public static class PrepDb 
 {
     public static void PrepPopulation(IApplicationBuilder app, bool isProd)
     {
      
      using(var serviceScope = app.ApplicationServices.CreateAsyncScope()) 
      {

         SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
      }

     }

     private static void SeedData(AppDbContext context, bool isProd ) 
     {
         if(isProd)
         {
             Console.WriteLine("--> Attemting to apply migrations...");

             try
             {

                 context.Database.Migrate();

             }
             catch(Exception ex) 
             {
                Console.WriteLine($"--> Could not run migrations {ex.Message}");
             }

         }
        
         if(!context.Employees.Any())
         {
             Console.WriteLine("Seeding data...");
             
             context.Employees.AddRange(
                 new Employee() { FirstName="Romin", LastName="Irani", MiddleName="", Age = 10, Gender="Male" },
                 new Employee() { FirstName="Neil", LastName="Irani", MiddleName="", Age = 20, Gender="Male" },
                 new Employee() { FirstName="Tom", LastName="Hanks", MiddleName="", Age = 10, Gender="Male" },
                 new Employee() { FirstName="George", LastName="Waker", MiddleName="", Age = 10, Gender="Male" },
                 new Employee() { FirstName="Romin", LastName="Irani", MiddleName="", Age = 40, Gender="Male" },
                 new Employee() { FirstName="Maya", LastName="Govil", MiddleName="", Age = 15, Gender="Female" }
             );
             context.SaveChanges();
         }

         else
         {
             Console.WriteLine("We already have data");
         }
     }
 }

}
