using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.Abstractions;
using ToDoList.Domain.Entities.Models;
using ToDoList.Infrastructure.BaseRepositories;
using ToDoList.Infrastructure.BaseRepository;
using ToDoList.Infrastructure.Persistance;

namespace ToDoList.Infrastructure
{
    public static class ToDoListDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ToDoListDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("ToDoListConnectionString"));
            });
            
            services.AddScoped<IBaseRepository<User>,BaseRepository<User>>();
            services.AddScoped<INotePadRepository,NotePadRepository>();
            services.AddScoped<IUserRepository,UserRepository>();

            return services;
        }
    }
}
