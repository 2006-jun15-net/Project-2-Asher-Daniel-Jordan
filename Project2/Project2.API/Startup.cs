using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Project2.Data.Model;
using Project2.Data.Repository;
using Project2.Domain.Interface;
using Project2.Domain.Service;

namespace Project2.API
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "AllowLocalNgServe";
        private readonly ILoggerFactory loggerFactory = new LoggerFactory();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                              builder =>
                              {
                                  builder.WithOrigins("*")
                                                      .AllowAnyHeader()
                                                      .AllowAnyMethod();
                                                        //.AllowCredentials();
                              });
        });
            


            services.AddSwaggerGen();
            
            services.AddDbContext<Project2Context>(options =>
                options
                .UseLoggerFactory(loggerFactory)
                .UseSqlServer(Configuration.GetConnectionString("SqlServer")));

            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IIllnessRepository, IllnessRepository>();
            services.AddScoped<INurseRepository, NurseRepository>();
            services.AddScoped<IOpsRoomRepository, OpsRoomRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<ITreatmentDetailsRepository, TreatmentDetailsRepository>();
            services.AddScoped<ITreatmentRepository, TreatmentRepository>();
            services.AddScoped<IWorkingDetailsRepository, WorkingDetailsRepository>();
            services.AddScoped<IPatientRoomRepository, PatientRoomRepository>();
            services.AddScoped<PatientService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/Hospital-{Date}.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project 2 API V1");
            });

            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                        
            });
        }
    }
}
