using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using User.Core.Entities;
using User.Persistence.Classes;
using User.Persistence.Context;
using User.Persistence.Interfaces;

namespace User
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
    services.AddConsulConfig(Configuration);
            /*services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);*/
            services.AddControllers();
           // services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
           
            services.AddAutoMapper(typeof(Startup));
      
            
            var x = Configuration.GetConnectionString("Default");
            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = Boolean.Parse(Configuration.GetSection("IdentityConfiguration")
                    .GetSection("RequireDigit").Value);
                options.Password.RequireLowercase = Boolean.Parse(Configuration.GetSection("IdentityConfiguration")
                    .GetSection("RequireLowercase").Value);
                options.Password.RequireUppercase = Boolean.Parse(Configuration.GetSection("IdentityConfiguration")
                    .GetSection("RequireUppercase").Value);
                options.Password.RequireNonAlphanumeric = Boolean.Parse(Configuration
                    .GetSection("IdentityConfiguration").GetSection("RequireNonAlphanumeric").Value);
                options.Password.RequiredLength = int.Parse(Configuration.GetSection("IdentityConfiguration")
                    .GetSection("RequiredLength").Value);
            })
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders()
                .AddRoles<IdentityRole>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.ClaimsIssuer = Configuration["Jwt:Issuer"];
                options.Audience = Configuration["Jwt:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecurityKey"])),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,//Type = SecuritySchemeType.ApiKey,
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

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddTransient<IUserService, UserService>();

            services.Scan(scan => scan
            .FromCallingAssembly()
            .AddClasses()
            .AsMatchingInterface());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseConsul(Configuration);
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
           
            app.UseEndpoints(endpoints => { endpoints.MapControllers();});
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "User API");
            });
        }
        /*public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddTransient<IValidator<CreateUserDto>, UserValidator>();
            services.Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses()
                .AsMatchingInterface());

         
            
            
            var x = Configuration.GetConnectionString("Default");
            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));


            var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new Mapping.MappingProfile()); });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "User API",
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

           
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


            app.UseSwagger();


            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "User API"); });

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
            app.UseAuthentication();
            app.UseRouting();
        }*/
    }
}