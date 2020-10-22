using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

namespace APIGateWay
{
public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo{ Version = "v1", Title = "Task2 Api" });
        });
        
        services.AddControllers();
        services.AddOcelot().AddConsul();

    }

    public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {


        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger3/v1/swagger.json", "Course API");
            c.SwaggerEndpoint("/swagger2/v1/swagger.json", "User API ");
        });
        
        app.UseRouting();
       
        await app.UseOcelot();

    }
}
}

