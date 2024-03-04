using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Attributes;
using ToDoList.Application.Abstractions.IService;
using ToDoList.Domain.Entities.DTOs;
using ToDoList.Domain.Entities.Enums;
using ToDoList.Domain.Entities.Models;

namespace ToDoList.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
   // [Authorize]
    public class NotePadController : ControllerBase
    {
        private readonly INotePadService _notePadService;

        public NotePadController(INotePadService notePadService)
        {
            _notePadService = notePadService;
        }
        [HttpPost]
        [IdentityFilterAttributes(Permission.AddNotePad)]

        public async Task<string> AddNotePad(NotePadDTO model)
        {
            var res = await _notePadService.Add(model);

            return res;
        }
        [HttpGet]
        [IdentityFilterAttributes(Permission.GetAllNotePad)]
       public async Task<List<NotePadDTO>> GetAllNotePad()
        {
            var res = await _notePadService.GetAllNotePad();
            return res;
        }
        [HttpGet]
        [IdentityFilterAttributes(Permission.GetNotePadById)]
        public async Task<NotePad> GetNotePadById(int id)
        {
            return await _notePadService.GetNotePadById(id);
        }
        [HttpPut]
        [IdentityFilterAttributes(Permission.UpdateNotePad)]
        public async Task<string> UpdateNotePad(int id, NotePadDTO model)
        {
            return await _notePadService.UpdateNotePad(id, model);  
        }
        [HttpDelete]
        [IdentityFilterAttributes(Permission.DeleteNotePad)]
        public async Task<string> DeleteNotePad(int id)
        {
            return await _notePadService.DeleteNotePad(id);
        }

    }
}

