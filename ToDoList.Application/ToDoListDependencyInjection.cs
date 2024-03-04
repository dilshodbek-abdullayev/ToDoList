using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.Abstractions.IService;
using ToDoList.Application.Services.AuthService;
using ToDoList.Application.Services.NotePadService;
using ToDoList.Application.Services.UserService;

namespace ToDoList.Application
{
    public static class ToDoListDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<INotePadService, NotePadService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
