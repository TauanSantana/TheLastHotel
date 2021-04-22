using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using TheLastHotel.API.DependencyInjection;
using TheLastHotel.Repository.Database;
using TheLastHotel.Repository.Database.Interfaces;
using AutoMapper;
using TheLastHotel.Repository;

namespace TheLastHotel.API
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
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddAutoMapper(typeof(Startup));

            services.Configure<MongoDbSettings>(configuration =>
            {
                configuration.ConnectionString = Environment.GetEnvironmentVariable("ConnectionString");
                configuration.DatabaseName = Environment.GetEnvironmentVariable("DatabaseName");
            });
            
            services.AddSingleton<IMongoDbSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            MongoDbPersistence<ConfigurationDbMap>.Configure();
            DIContainer.RegisterRepository(services);
            DIContainer.RegisterServices(services);

            services.AddHealthChecks();
            services.AddControllers()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            services.AddSwaggerGen();
            services.AddCustomSwaggerConfiguration();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHealthChecks("/health");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "The Last Hotel - V1");
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
