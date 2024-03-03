using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.Abstractions.IService;
using ToDoList.Domain.Entities.DTOs;

namespace ToDoList.Application.Services.AuthService
{
    public class AuthService : IAuthService
    {
        public Task<ResponseLogin> GenerateToken(RequestLogin request)
        {
            throw new NotImplementedException();
        }
    }
}
