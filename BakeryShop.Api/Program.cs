using BakeryShop.Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            using var context = scope.ServiceProvider.GetService<AppDbContext>();
            context.Database.EnsureCreated();
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices((context, services) =>
                {
                    var teste = context.Configuration.GetConnectionString("DefaultConnection");

                    // Configuração do banco de dados
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

                    // Injeção de dependências
                    services.AddScoped<ICountryRepository, CountryRepository>();
                    services.AddScoped<IJobCategoryRepository, JobCategoryRepository>();
                    services.AddScoped<IEmployeeRepository, EmployeeRepository>();

                    // Configuração de CORS
                    services.AddCors(options =>
                    {
                        options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
                    });

                    // Adicionar controllers
                    services.AddControllers();

                    // Configuração do Swagger
                    services.AddEndpointsApiExplorer();
                    services.AddSwaggerGen();
                });

                webBuilder.Configure((context, app) =>
                {
                    var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

                    if (env.IsDevelopment())
                    {
                        app.UseDeveloperExceptionPage();

                        // Habilita o Swagger no ambiente de desenvolvimento
                        app.UseSwagger();
                        app.UseSwaggerUI(c =>
                        {
                            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                        });
                    }

                    app.UseHttpsRedirection();
                    app.UseRouting();
                    app.UseAuthorization();

                    app.UseCors("Open");

                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                });
            });
}
