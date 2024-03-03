using ToDoList.Application.Abstractions;
using ToDoList.Application.Abstractions.IService;
using ToDoList.Domain.Entities.DTOs;
using ToDoList.Domain.Entities.Models;

namespace ToDoList.Application.Services.NotePadService
{
    public class NotePadService : INotePadService
    {
        private readonly INotePadRepository _notePadRepository;
        public NotePadService(INotePadRepository notePadRepository)
        {
            _notePadRepository = notePadRepository;
        }
        public async Task<string> Add(NotePadDTO notePadDTO)
        {
            var notePad = new NotePad()
            {
                Note = notePadDTO.Note,
            };
            if (notePad != null)
            {
                await _notePadRepository.Create(notePad);
                return "Added";
            }
            return "Failed";

        }

        public async Task<string> Delete(int id)
        {
            var result = await _notePadRepository.Delete(x => x.Id == id);
            if (result)
            {
                return "Deleted";
            }
            return "Failed";
        }

        public async Task<List<NotePad>> GetAll()
        {
            var result = await _notePadRepository.GetAll();
            return result.ToList();
        }

        public async Task<NotePad> GetById(int id)
        {
            var result = await _notePadRepository.GetByAny(x => x.Id == id)
            return result;
        }

        public Task<DateTime> GetDateTimeDo(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public Task<DateTime> GetDateTimeTo(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public Task<string> Update(int id, NotePadDTO notePadDTO)
        {
            throw new NotImplementedException();
        }
    }
}
