using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Attributes;
using ToDoList.Application.Abstractions.IService;
using ToDoList.Domain.Entities.DTOs;
using ToDoList.Domain.Entities.Enums;
using ToDoList.Domain.Entities.ViewModels;

namespace ToDoList.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [IdentityFilterAttributes(Permission.CreateUser)]
        public async Task<string> CreateUser([FromForm] UserDTO userDTO)
        {
            return await _userService.CreateUser(userDTO);
        }
        [HttpGet]
        [IdentityFilterAttributes(Permission.GetAll)]
        public async Task<List<UserViewModel>> GetAll()
        {
            return await _userService.GetAll();
        }
        [HttpGet]
        [IdentityFilterAttributes(Permission.GetByUserId)]
        public async Task<UserViewModel> GetByUserId(int id)
        {
            return await _userService.GetById(id);
        }
        [HttpGet]
        [IdentityFilterAttributes(Permission.GetByUserEmail)]
        public async Task<UserViewModel> GetByUserEmail(string email)
        {
            return await _userService.GetByEmail(email);
        }
        [HttpGet]
        [IdentityFilterAttributes(Permission.GetByUserName)]
        public async Task<List<UserViewModel>> GetByUserName(string name)
        {
            return await _userService.GetByName(name);
        }
        [HttpGet]
        [IdentityFilterAttributes(Permission.GetByRole)]
        public async Task<List<UserViewModel>> GetByRole(string role)
        {
            return await _userService.GetByRole(role);
        }
        [HttpPut]
        [IdentityFilterAttributes(Permission.UpdateUser)]
        public async Task<string> UpdateUser(int id,UserDTO userDTO)
        {
            return await _userService.UpdateUser(id,userDTO);
        }
        [HttpDelete]
        [IdentityFilterAttributes(Permission.DeleteUser)]
        public async Task<string> DeleteUser(int id)
        {
            return await _userService.DeleteUser(id);
        }

    }
}
