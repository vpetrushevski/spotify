using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Spotify.Application.Session;
using Spotify.DTO.Response;
using System.Reflection;

namespace Spotify.API.Extensions
{
    public static class DiExtensions
    {
        #region CORS

        /// <summary>
        /// Configure Cross-Origin Resource Sharing (CORS)
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed((host) => true)
                        .AllowCredentials();
                    });
            });
        }

        #endregion

        #region Configure Controllers

        /// <summary>
        /// Configure controllers and add 400 and 401 response to all endpoints
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers(o =>
            {
                // Add [ProducesResponseType(typeof(ApiResponse<string>), 400)] to all endpoints
                // This response is returned on validation error, server error, missing token or manually for failed response
                o.Filters.Add(new ProducesResponseTypeAttribute(typeof(ApiResponse<string>), 400));

                // Add [ProducesResponseType(typeof(ApiResponse<string>), 401)] to all endpoints
                // This response is returned when liftime of the JWT sent in the authtoken header is expired
                o.Filters.Add(new ProducesResponseTypeAttribute(typeof(ApiResponse<string>), 401));
            }).AddNewtonsoftJson();
        }

        #endregion

        #region Validation Response

        /// <summary>
        /// Service responsible for returning validation errors using Data annotation validation approach
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureValidationResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(o =>
            {
                o.InvalidModelStateResponseFactory = actionContext =>
                    ControllerExtensions.ValidationErrrorResponse(actionContext.ModelState);
            });
        }

        #endregion

        /// <summary>
        /// Add Swagger service
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, true);
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {
                        "Bearer", new string[] { }
                    },
                };

                //Enable Swagger authentication
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter your token to authorize!",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                // Create Authorization header and insert the token in it
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },

                    new List<string>()
                    }
                });
            });
        }

        public static void ConfigureSession(this IApplicationBuilder app)
        {
            app.UseSession();
            SessionContext.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
        }
    }
}
