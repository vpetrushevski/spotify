using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Spotify.Application.Services.Implementations;
using Spotify.Application.Services.Interface;
using Spotify.DTO.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Application.Extensions
{
    public static class DiExtensions
    {
        #region AutoMapper
        /// <summary>
        /// Configure AutoMapper package
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.RegisterMappers();
        }

        #endregion

        #region Mappers
        public static void RegisterMappers(this IServiceCollection services)
        {
            // Register custom mappers with parameters
            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                //cfg.AddProfile(new LookupMapper());
                //cfg.AddProfile(new LeadMapper(provider.GetService<IEncryptor>()));
            }).CreateMapper());
        }

        #endregion

        #region Services

        /// <summary>
        /// Register services
        /// </summary>
        /// <param name="services"></param>
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register your services for DI here
            services.AddAppSettingsInstances(configuration);

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddSingleton<IEncryptor>(new Encryptor(configuration.GetStringValue("EncryptionKey")));
            services.AddScoped<ISpotifyService, SpotifyService>();
            services.AddScoped<IAuthService, AuthService>();
        }

        #endregion

        public static void AddFluentValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
        }

        public static void AddAppSettingsInstances(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration.GetInstance<SpotifyConfig>());
        }
    }
}
