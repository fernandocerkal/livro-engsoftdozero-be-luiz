using System.IO;
using Botsuana.Covid19.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Botsuana.Covid19
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();            
            services.AddCors();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();
            var postgresqlString = configuration.GetConnectionString("PostgreConnectionString");
            services.AddDbContext<EngSoftDoZeroDBContext>(opt => opt.UseNpgsql(postgresqlString));

            using (var context = services.BuildServiceProvider().GetService<EngSoftDoZeroDBContext>())
            {
                context.Database.Migrate();                                       
            }
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())            
                app.UseDeveloperExceptionPage();            

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors(b => b                 
                 .WithOrigins(new string[] { "http://localhost:4200" })                 
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials()
            );
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
