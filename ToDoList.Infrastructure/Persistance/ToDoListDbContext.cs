using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Entities.Models;

namespace ToDoList.Infrastructure.Persistance
{
    public class ToDoListDbContext : DbContext
    {
        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : base(options) 
        {
            Database.Migrate();
        }

        public DbSet<User> users { get; set; } 
        public DbSet<NotePad> notes { get; set; }

    }
}
