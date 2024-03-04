using ToDoList.Application.Abstractions;
using ToDoList.Domain.Entities.Models;
using ToDoList.Infrastructure.BaseRepository;
using ToDoList.Infrastructure.Persistance;

namespace ToDoList.Infrastructure.BaseRepositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ToDoListDbContext context) : base(context)
        {
        }
    }

}
