﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Infrastructure.Persistance;

namespace ToDoList.Infrastructure
{
    public static class DependenyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ToDoListDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("ToDoListConnectionString"));
            });

            return services;
        }
    }
}
