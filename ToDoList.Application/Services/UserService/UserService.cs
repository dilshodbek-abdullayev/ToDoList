using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.ComponentModel;
using System.Reflection.Metadata;
using ToDoList.Application.Abstractions;
using ToDoList.Application.Abstractions.IService;
using ToDoList.Domain.Entities.DTOs;
using ToDoList.Domain.Entities.Models;
using ToDoList.Domain.Entities.ViewModels;
using Document = QuestPDF.Fluent.Document;

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

        public async Task<string> GetPdfPath()
        {
            var text = "";

            var getAll = await _userRepository.GetAll();
            foreach(var user in getAll.Where(x => x.Role != "Admin"))
            {
                text = text + $"{user.Name}|{user.Email}\n";
            }

            DirectoryInfo directoryInfo = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent;

            var file = Guid.NewGuid().ToString();

            string pdfFolder = Directory.CreateDirectory(
                Path.Combine(directoryInfo.FullName, "pdfs")).FullName;

            QuestPDF.Settings.License = LicenseType.Community;

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header()
                    .Text("To Do List Users")
                    .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                    page.Content()
                    .Padding(1, Unit.Centimetre)
                    .Column(x =>
                    {
                        x.Spacing(20);

                        x.Item().Text(text);
                    });

                    page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                    });
                });
            })
                .GeneratePdf(Path.Combine(pdfFolder, $"{file}.pdf"));
            return Path.Combine(pdfFolder, $"{file}.pdf");
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
