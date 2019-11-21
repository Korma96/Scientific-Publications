using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScientificPublications.Common.Settings;
using ScientificPublications.Service;
using Swashbuckle.AspNetCore.Swagger;
using ScientificPublications.Application;
using AutoMapper;
using System.Reflection;
using ScientificPublications.DataAccess;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace ScientificPublications
{
    public class Startup
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public Startup(IConfiguration configuration, ILogger<ApiExceptionFilter> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new AuthorizeFilter());
                options.Filters.Add(new ApiExceptionFilter(_logger));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
              .AddXmlSerializerFormatters()
              .AddXmlDataContractSerializerFormatters();

            var appSettings = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettings);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("Bearer",
                            new ApiKeyScheme
                            {
                                In = "header",
                                Description = "Please enter into field the word 'Bearer' following by space and JWT",
                                Name = "Authorization",
                                Type = "apiKey"
                            });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> { { "Bearer", Enumerable.Empty<string>() } });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearerAuth(appSettings);

            services.RegisterSingletons<ISingletonService>();
            services.RegisterSingletons<IDataAccess>();

            // add automapper for api and service projects
            services.AddAutoMapper(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(Service.MappingProfile)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
