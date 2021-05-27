using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FMCW.WebCommon
{
    public static class WebExtensions
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services, 
            IConfiguration configuration, 
            string groupName)
        {
            var openApiInfo = new OpenApiInfo();
            configuration.GetSection("Application").Bind(openApiInfo);
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(groupName, openApiInfo);
            });
            return services;
        }

        public static IServiceCollection ConfigureControllersWithFilters(
            this IServiceCollection services, 
            IConfiguration configuration, 
            params IFilterMetadata[] filters)
        {
            services.AddControllers(options =>
            {
                foreach (var filter in filters)
                {
                    options.Filters.Add(filter);
                }
            });
            return services;
        }

        public static IServiceCollection AddConfig<T>(
                    this IServiceCollection services,
                    IConfiguration configuration, string sectionName = "") where T : class, new()
        {
            var config = string.IsNullOrEmpty(sectionName) ? configuration : configuration.GetSection(sectionName);
            var t = new T();
            config.Bind(t);
            services.AddSingleton(t);
            return services;
        }
    }
}
