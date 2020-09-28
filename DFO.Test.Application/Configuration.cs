using System;
using AutoMapper;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DFO.Test.Application.Context;
using DFO.Test.Application.Services.Interfaces;
using DFO.Test.Application.Validators;
using DFO.Test.Application.Contracts.User;
using DFO.Test.Application.Model.Entities;
using DFO.Test.Application.Services.User;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Builder;

namespace DFO.Test.Application
{
    public static class Configuration
    {
        public static void ConfigureApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("DFO_TEST"));

            //Logging
            services.AddLogging(x =>
            {
                x.AddFile($"{AppDomain.CurrentDomain.BaseDirectory}\\logs\\app.log");
                x.AddConsole();
                x.AddDebug();
            });

            services.AddSingleton<UserRequestValidator>();
            services.AddScoped<IUserService, UserService>();
            services.AddHttpContextAccessor();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRequest, User>();
                cfg.CreateMap<User, UserResponse>();
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            //config swashbuckle
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1.0", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "DFO API", Version = "v1.0" });

                opt.DocInclusionPredicate((version, apiDescription) =>
                {
                    var values = apiDescription.RelativePath
                        .Split('/')
                        .Select(v => v.Replace("v{version}", version));

                    apiDescription.RelativePath = string.Join("/", values);

                    var versionParameter = apiDescription.ParameterDescriptions
                        .SingleOrDefault(p => p.Name == "version");

                    if (versionParameter != null)
                        apiDescription.ParameterDescriptions.Remove(versionParameter);

                    return true;
                });
            });

            //compress json
            services.Configure<GzipCompressionProviderOptions>(opt => opt.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression(opt => opt.Providers.Add<GzipCompressionProvider>());

        }

    }
}
