using Capgemini.Domain.Handlers;
using Capgemini.Domain.Repositories;
using Capgemini.Infra.DataContetx;
using Capgemini.Infra.Repositories;
using Capgemini.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace Capgemini.Api
{
    public class Startup
    {
        public static IConfiguration Configuration { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            services.AddMvc();

            services.AddResponseCompression();

            services.AddScoped<DataContext, DataContext>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<PersonHandler, PersonHandler>();

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "Capgemini", Version = "v1" });
            });

            Settings.ConnectionString = $"{Configuration["connectionString"]}";
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();
            app.UseResponseCompression();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Capgemini - V1");
            });
        }
    }
}
