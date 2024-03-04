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

        public async Task<string> DeleteNotePad(int id)
        {
            var result = await _notePadRepository.Delete(x => x.Id == id);
            if (result)
            {
                return "Deleted";
            }
            return "Failed";
        }

        public async Task<List<NotePadDTO>> GetAllNotePad()
        {
            var result = await _notePadRepository.GetAll();

            List<NotePadDTO> list = result.Select(x=> new NotePadDTO() { Note=x.Note}).ToList();
            return list;
        }

        public async Task<NotePad> GetNotePadById(int id)
        {
            var result = await _notePadRepository.GetByAny(x => x.Id == id);
            return result;
        }

        public async Task<DateTime> GetDateTimeDo(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public Task<DateTime> GetDateTimeTo(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UpdateNotePad(int id, NotePadDTO notePadDTO)
        {
           var res = await _notePadRepository.GetByAny(x => x.Id == id);

            if(res != null)
            {
                res.Note = notePadDTO.Note;
                res.Status = "true";

                var result = await _notePadRepository.Update(res);
                if(result != null)
                {
                    return "Updated";
                }
                return "Not Updated";
            }
            return "Failed";
        }
    }
}
