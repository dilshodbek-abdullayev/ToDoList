using ToDoList.Application.Abstractions;
using ToDoList.Application.Abstractions.IService;
using ToDoList.Domain.Entities.DTOs;
using ToDoList.Domain.Entities.Models;
using ToDoList.Domain.Entities.ViewModels;

namespace ToDoList.Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string> CreateUser(UserDTO userDTO)
        {
            var res = await _userRepository.GetAll();
            var email = res.Any(x => x.Email == userDTO.Email);
            var login = res.Any(x => x.Login == userDTO.Login);

            if (!login)
            {
                if (!login)
                {

                    var newUser = new User()
                    {
                        Name = userDTO.Name,

                        Email = userDTO.Email,
                        Password = userDTO.Password,
                        Login = userDTO.Login,
                        Role = userDTO.Role
                    };

                    await _userRepository.Create(newUser);
                    return "Created";
                }
                return "Login already exists";
            }
            return "Email already exists";
        }

        public async Task<string> DeleteUser(int id)
        {
            var res = await _userRepository.Delete(x => x.Id == id);

            if (res)
            {
                return "Deleted";
            }
            return "Failed";

        }

        public async Task<List<UserViewModel>> GetAll()
        {
            var get = await _userRepository.GetAll();

            var res = get.Select(x => new UserViewModel()
            {
                Name = x.Name,
                Email = x.Email,
                Role = x.Role
            }).ToList();    
            return res;

        }

        public async Task<UserViewModel> GetByEmail(string email)
        {
           var get = await _userRepository.GetByAny(x  => x.Email == email);

            var res = new UserViewModel()
            {

                Name = get.Name,
                Email = get.Email,
                Role = get.Role
            };
            return res;
        }

        public async Task<UserViewModel> GetById(int id)
        {
           var get = await _userRepository.GetByAny(x => x.Id == id);
            var res = new UserViewModel()
            {
                Name = get.Name,
                Email = get.Email,
                Role = get.Role
            };
            return res;

        }

        public async Task<List<UserViewModel>> GetByName(string name)
        {
            var get = await _userRepository.GetAll();
            var find = get.Where(x => x.Name == name);
            var res = find.Select(x => new UserViewModel()
            {
                Name = x.Name,
                Email = x.Email,
                Role = x.Role
            }).ToList();
            return res;
        }

        public async Task<List<UserViewModel>> GetByRole(string role)
        {
            var get = await _userRepository.GetAll();
            var find = get.Where(x => x.Role == role);

            var res = find.Select(x => new UserViewModel { Name = x.Name,Email = x.Email,Role = x.Role }).ToList();

            return res;
        }

        public Task<string> GetPdfPath()
        {
            throw new NotImplementedException();
        }

        public async Task<string> UpdateUser(int id, UserDTO userDTO)
        {
            var res = await _userRepository.GetAll();
            var email = res.Any(x => x.Email == userDTO.Email);
            var login = res.Any(x => x.Login ==  userDTO.Login);

            if (!email)
            {
                if (!login)
                {
                    var old = await _userRepository.GetByAny(x => x.Id == id);

                    if (old == null) return "Failed";
                    old.Name = userDTO.Name;

                    old.Login = userDTO.Login;
                    old.Role = userDTO.Role;
                    old.Password = userDTO.Password;
                    old.Email = userDTO.Email;

                    await _userRepository.Update(old);
                    return "Updated";

                }
                return "Login already exists";
            }
            return "Email already exists";

        }
    }
}
