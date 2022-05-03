using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using EmployeeService.Data;

namespace EmployeeService
{
    public class Startup 
    {
        public IConfiguration Configuration {get;}

        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var origins = Configuration.GetSection("AllowOrigin").GetSection("OriginList").Value;
            services.AddControllers();
            if(_env.IsProduction()) 
            {
                Console.WriteLine("--> Using SqlServer Db");
                services.AddDbContext<AppDbContext>( opt => opt.UseSqlServer(Configuration.GetConnectionString("Conn")));

            }
            else
            {
                Console.WriteLine("--> Using InMeb Db");
                services.AddDbContext<AppDbContext>( opt => opt.UseInMemoryDatabase("InMem"));

            }
            
            services.AddScoped<IEmployeeRepo, EmployeeRepo>();        
            
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                builder => {
                    builder.WithOrigins(origins).AllowAnyOrigin().AllowAnyHeader().WithMethods("GET","POST", "DELETE","PUT","PATCH");
                });

            });
            
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen ( c=>
                c.SwaggerDoc("V1",new OpenApiInfo{Title ="EmployeeService", Version="V1"})
            );

            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
        {
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI( c=> c.SwaggerEndpoint("/swagger/v1/swagger.json","EmployeeService v1"));
            }
            
            app.UseRouting();
            app.UseCors("AllowSpecificOrigin");
            app.UseAuthorization();
            app.UseEndpoints (endpoints => endpoints.MapControllers());
            PrepDb.PrepPopulation(app, env.IsProduction());

        }
    }
}