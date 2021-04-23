using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace TheLastHotel.API
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddCustomSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "The Last Hotel - V1",
                    Description = "This is the management api of The Last Hotel - Cancun",
                    TermsOfService = new Uri("https://TheLastHotel.ca/en/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "The Last Hotel - IT",
                        Email = string.Empty,
                        Url = new Uri("https://TheLastHotel.ca/en/support"),
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            return services;
        }
    }
}
