using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Entities.Models;
using ToDoList.Infrastructure.BaseRepositories;

namespace ToDoList.Application.Abstractions
{
    public interface IUserRepository : IBaseRepository<User>
    {
    }
}
