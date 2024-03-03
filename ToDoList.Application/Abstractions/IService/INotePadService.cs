using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Entities.DTOs;
using ToDoList.Domain.Entities.Models;

namespace ToDoList.Application.Abstractions.IService
{
    public interface INotePadService
    {
        public Task<string> Add(NotePadDTO notePadDTO);
        public Task<List<NotePad>> GetAll();
        public Task<NotePad> GetById(int id);
        public Task<DateTime> GetDateTimeTo(DateTime dateTime);
        public Task<DateTime> GetDateTimeDo(DateTime dateTime);

        public Task<string> Delete(int id);
        public Task<string> Update(int id, NotePadDTO notePadDTO);
    }
}
