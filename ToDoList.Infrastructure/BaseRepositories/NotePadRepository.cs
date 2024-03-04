using ToDoList.Application.Abstractions;
using ToDoList.Domain.Entities.Models;
using ToDoList.Infrastructure.BaseRepository;
using ToDoList.Infrastructure.Persistance;

namespace ToDoList.Infrastructure.BaseRepositories
{
    public class NotePadRepository : BaseRepository<NotePad>, INotePadRepository
    {
        public NotePadRepository(ToDoListDbContext context) : base(context) { }
    }
}
