using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minuto.Seguros.Application.Extensions;
using Minuto.Seguros.Domain.Entities;

namespace Minuto.Seguros.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        public IServiceProvider serviceProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

        }

        /// <summary>
        /// Configuração dos serviços utilizados pela Application
        /// </summary>
        /// <param name="services">Coleção de serviços</param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCorsService();
         
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<Minuto.Seguros.Infra.Client.Corporate.Rss.Dto.FeedDto, Minuto.Seguros.Service.Dto.FeedDto>();                
                mc.CreateMap<Minuto.Seguros.Service.Dto.FeedDto, Minuto.Seguros.Infra.Client.Corporate.Rss.Dto.FeedDto>();
                mc.CreateMap<Minuto.Seguros.Infra.Client.Corporate.Rss.Dto.FeedDto, Feed>();
                mc.CreateMap<Feed, Minuto.Seguros.Infra.Client.Corporate.Rss.Dto.FeedDto>();
            });            

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvcService();
            services.AddApiVersioningService();
            services.AddJwtService(Configuration);
            services.AddSwaggerService();
            services.AddHttpContextAccessor();
            services.AddSettingsService(Configuration);

            serviceProvider = services.RegisterServices(Configuration);

            return serviceProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Use(next => context => { context.Request.EnableRewind(); return next(context); });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {                
                c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Minuto.Seguros.Application.Resources.Swagger_Minuto_Seguros_index.html");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minuto Seguro API v1.0");                
            });

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthentication();

            app.UseStaticFiles();
            app.UseMvc();
        }        
    }
}
