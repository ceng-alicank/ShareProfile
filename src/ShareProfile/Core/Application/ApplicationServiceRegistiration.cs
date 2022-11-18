using Application.Behaviors.Authorization;
using Application.Behaviors.Logging;
using Application.Behaviors.Validation;
using Core.Security.JWT;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection; 
namespace Application
{
    public static class ApplicationServiceRegistiration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<ITokenHelper, JwtHelper>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        }

        //public static class ServiceTool
        //{
        //    public static IServiceProvider ServiceProvider { get; set; }

        //   public static IServiceCollection Create(IServiceCollection services)
        //    {
        //        ServiceProvider = services.BuildServiceProvider();
        //        return services;
        //    }
        //}
    }
}
