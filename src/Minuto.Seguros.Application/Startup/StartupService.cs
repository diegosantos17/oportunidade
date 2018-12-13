using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Minuto.Seguros.Infra.CrossCutting.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Swashbuckle.AspNetCore.Swagger;
using System.Threading.Tasks;
using Minuto.Seguros.Application.Filters.swagger;
using Autofac.Extensions.DependencyInjection;
using Minuto.Seguros.Infra.Data.Connection;
using Minuto.Seguros.Service;
using Autofac;
using Minuto.Seguros.Infra.Data;
using Minuto.Seguros.Application.Extensions.swagger;
using AutoMapper;
using System.IO;

namespace Minuto.Seguros.Application
{
    /// <summary>
    /// Classe para unificar configura~ções da startup.cs
    /// </summary>
    public static class StartupService
    {
        /// <summary>
        /// Adicona configuração CORS no projeto
        /// </summary>
        /// <param name="services"></param>
        public static void AddCorsService(this IServiceCollection services)
        {
            services.AddCors(options => {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin());
            });
        }

        /// <summary>
        /// Adiciona configuração do MVC no projeto
        /// </summary>
        /// <param name="services"></param>
        public static void AddMvcService(this IServiceCollection services)
        {
            services.AddMvc(config => {
                AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
                                    .RequireAuthenticatedUser()
                                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        /// <summary>
        /// Adiciona configuração de versionamento de api ao projeto
        /// </summary>
        /// <param name="services"></param>
        public static void AddApiVersioningService(this IServiceCollection services)
        {
            services.AddApiVersioning(o => {
                o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0); // Versão padrão
                o.AssumeDefaultVersionWhenUnspecified = true;                        // Assume a versão padrão quando não informado
                o.ApiVersionReader = new MediaTypeApiVersionReader();                // Ler a versão via accept header
            });
        }

        /// <summary>
        /// Adiciona configuração JWT no projeto
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddJwtService(this IServiceCollection services, IConfigurationRoot configuration)
        {
            JwtConfig jwtConfig = configuration.GetSection("JwtConfig").Get<JwtConfig>();
            byte[] key = Encoding.ASCII.GetBytes(jwtConfig.Secret);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret))
                };
            });
        }

        /// <summary>
        /// Adiciona Swagger na aplicação
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", 
                    new Info {
                        Version = "v1"
                        , Title = "Minuto Seguro API"
                    });                

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                    { "Bearer", Enumerable.Empty<string>() },
                });

                c.DocInclusionPredicate((docName, apiDesc) => {
                    ApiVersionModel actionApiVersionModel = apiDesc.ActionDescriptor?.GetApiVersion();
                    
                    if (actionApiVersionModel == null)
                    {
                        return true;
                    }
                    if (actionApiVersionModel.DeclaredApiVersions.Any())
                    {
                        return actionApiVersionModel.DeclaredApiVersions.Any(v => $"v{v.ToString()}" == docName);
                    }
                    return actionApiVersionModel.ImplementedApiVersions.Any(v => $"v{v.ToString()}" == docName);
                });
                c.DocumentFilter<VersionFilter>();
                c.OperationFilter<ApiVersionOperationFilter>();                
                c.IncludeXmlComments(GetXmlCommentsPath());

            });
        }

        public static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\Minuto.Seguro.Application.xml", System.AppDomain.CurrentDomain.BaseDirectory);
        }

        /// <summary>
        /// Adiciona suporte a configuração na aplicação
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        public static void AddSettingsService(this IServiceCollection services, IConfigurationRoot Configuration)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<ConfigEmail>(Configuration.GetSection("ConfigEmail"));            
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            ConnectionStrings connectionStrings = Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();            
        }

        private static ConnectionStrings AddConnectionStringsService(this IServiceCollection services, IConfigurationRoot Configuration)
        {
            return Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static AutofacServiceProvider RegisterServices(this IServiceCollection services, IConfigurationRoot Configuration)
        {
            services.AddScoped<IConnect, Infra.Data.Context.MongoContext>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped<ServiceModule>();

            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new DataModule(AddConnectionStringsService(services, Configuration).MinutoSeguroConn, "minutosegurodb"));
            containerBuilder.RegisterModule<ServiceModule>();

            containerBuilder.RegisterType<Infra.Client.Corporate.Rss.RssClient>().As<Infra.Client.Corporate.Rss.Contracts.IRssClient>();

            containerBuilder.Populate(services);

            return new AutofacServiceProvider(containerBuilder.Build());
        }
    }
}
