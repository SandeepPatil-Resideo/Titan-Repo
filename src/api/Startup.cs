using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using Titan.Common.Configuration.ContainerSecrets;
using Titan.Common.Diagnostics.State;
using Titan.UFC.Common.ExceptionMiddleWare;
using Titan.UFC.Addresses.API.Configuration;
using Titan.UFC.Addresses.API.Diagnostics;
using Titan.UFC.Addresses.API.Entities;
using Titan.UFC.Addresses.API.HealthChecks;
using Titan.UFC.Addresses.API.Mapper;
using Titan.UFC.Addresses.API.Models;
using Titan.UFC.Addresses.API.Repository;
using Titan.UFC.Addresses.API.Service;
using Titan.UFC.Addresses.API.Resources;
using Honeywell.Security.FrameWork.ReadingConfigurationFile;

namespace Titan.UFC.Addr.API
{
    public class Startup
    {
        
        public Settings Settings { get; set; }
        public IStateObserver StateObserver { get; set; }

        public Startup(IHostingEnvironment env, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            //Pull config settings from both appsettings.json and environment variables
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddJsonFile($"secrets/appsettings.{Assembly.GetEntryAssembly().GetName().Name}.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
               .AddContainerSecrets() //Load env variables from secrets file
               .AddEnvironmentVariables();
            Configuration = builder.Build();

            Configuration = configuration;
        }
        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; private set; }
        public void ConfigureServices(IServiceCollection services)
        {
            var reader = new ReadingConfiguration();
            Configuration = reader.ApplyConfigurationFromOrchestrator();

            services.AddOptions();
            services.Configure<Settings>(Configuration);
            Settings = Configuration.Get<Settings>();
            services.InjectServiceDependency();
            services.AddApiVersioning();
            //All requests will be logged
            services.AddDefaultRequestLoggingEventSource();
            services.AddMvcCore(c => c.AddGlobalLoggingRequestFilters())
                .AddApiExplorer()
                .AddJsonFormatters();

            services.Configure<RequestLocalizationOptions>(
               opts =>
               {
                   var supportedCultures = new List<CultureInfo>
                   {
                        new CultureInfo("en-GB"),
                        new CultureInfo("en-US"),
                        new CultureInfo("en"),
                        new CultureInfo("fr-FR"),
                        new CultureInfo("fr"),
                   };

                   opts.DefaultRequestCulture = new RequestCulture("en-US");
                    // Formatting numbers, dates, etc.
                    opts.SupportedCultures = supportedCultures;
                    // UI strings that we have localized.
                    opts.SupportedUICultures = supportedCultures;
               });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
           

            services.AddDbContextPool<AddressContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
        .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning)));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info() { Title = "titan-address-api", Version = "v1" });
                c.DescribeAllEnumsAsStrings();

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
            services.AddMvc(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
            }
           ).SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddFluentValidation(fvc =>
            {
                fvc.RegisterValidatorsFromAssemblyContaining<Startup>();
                fvc.RegisterValidatorsFromAssemblyContaining<Address>();
            });

            //Set up an observer that can be used to reflect the health of the application
            StateObserver = new CountStateObserver("Test observer", Settings.ObserverFailures, Settings.ObserverSuccesses);
            services.AddSingleton(StateObserver);
            SetupStateObserverLogging();

            //Provides health checks that can be used to query the health of the application
            services.AddHealthChecks()
                .AddCheck<LivenessHealthCheck>("Liveness")
                .AddCheck<ReadinessHealthCheck>("Readiness");

            //Enables the use of auditing headers
            services.AddTitanActionAudit().AddTitanEventSourceAuditProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseTitanRequestLogging();
            app.UseMiddleware(typeof(CustomExceptionMiddleware));
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "titan-address-api API V1");
            });
            app.UseHealthChecks("/health/live", new HealthCheckOptions()
            {
                Predicate = check => check.Name == "Liveness"
            });
            app.UseHealthChecks("/health/ready", new HealthCheckOptions()
            {
                Predicate = check => check.Name == "Readiness"
            });
            app.UseRequestLocalization();
        }

        public void SetupStateObserverLogging()
        {
            StateObserver.OnStateChanged += (sender, args) => titanaddressapiEventSource.Log.StateChanged(args.CurrentState.ToString());
        }

    }
    /// <summary>
    /// Service dependency classes
    /// </summary>
    public static class ServiceDependency
    {
        /// <summary>
        /// Implement dependency 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection InjectServiceDependency(this IServiceCollection services)
        {
            services.AddSingleton<ISharedResource, SharedResource>();
            services.AddScoped<IAddressValidator, AddressValidator>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IAddressRepository, AddressRepository>();            
            return services;
        }
    }
}


