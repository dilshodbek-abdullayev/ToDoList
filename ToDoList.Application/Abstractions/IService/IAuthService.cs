using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Entities.DTOs;

namespace ToDoList.Application.Abstractions.IService
{
    public interface IAuthService
    {
        public Task<ResponseLogin> GenerateToken(RequestLogin request);
    }
}
