using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AutoMapper;
using Course.Core.Dto;
using Course.Persistence;
using Course.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Course
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddTransient<IValidator<CourseDto>, CourseValidator>();
           
            services.Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses()
                .AsMatchingInterface());
            
            services.AddControllers();
            var x = Configuration.GetConnectionString("Default");
            services.AddDbContext<CourseDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
         
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mapping.MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Course API",
                    Version = "v1",
                    Description = "Description for the API goes here.",

                });
               
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }});
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();

            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


            app.UseSwagger();


            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "course API"); });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
        }


    }
}