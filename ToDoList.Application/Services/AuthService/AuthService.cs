﻿
using ToDoList.Application.Abstractions;
using ToDoList.Application.Abstractions.IService;
using ToDoList.Domain.Entities.DTOs;
using ToDoList.Domain.Entities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;


namespace ToDoList.Application.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepo;
        private readonly IUserService _userService;

        public AuthService(IConfiguration configuration, IUserRepository userRepo,IUserService userService)
        {
            _userService = userService;
            _userRepo = userRepo;
            _configuration = configuration;
        }

        public async Task<ResponseLogin> GenerateToken(RequestLogin request)
        {
            if (request == null)
            {
                return new ResponseLogin()
                {
                    Token = "User Not Found"
                };
            }
            var findUser = await FindUser(request);
            if (findUser != null)
            {

                var permission = new List<int>();

                if (findUser.Role != "Admin")
                {
                    permission = new List<int> {9,10,11,12,13,14 };
                }
                else
                {
                    permission = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14};


                }
                var jsonContent = JsonSerializer.Serialize(permission);
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Role, findUser.Role),
                    new Claim("Login", findUser.Login),
                    new Claim("UserID", findUser.Id.ToString()),
                    new Claim("CreatedDate", DateTime.UtcNow.ToString()),
                    new Claim("Permissions", jsonContent)
                };

                return await GenerateToken(claims);
            }

            return new ResponseLogin()
            {
                Token = "Un Authorize"
            };
        }

        public async Task<ResponseLogin> GenerateToken(IEnumerable<Claim> additionalClaims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var exDate = Convert.ToInt32(_configuration["JWT:ExpireDate"] ?? "10");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(DateTime.UtcNow).ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64)
            };

            if (additionalClaims?.Any() == true)
                claims.AddRange(additionalClaims);


            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(exDate),
                signingCredentials: credentials);

            return new ResponseLogin()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };


        }
        public async Task<User> FindUser(RequestLogin user)
        {
            var res = await _userService.GetToken(user.Login);
            if(user.Login == res.Login && user.Password == res.Password)
            {
                return res;
            }
            return null;
           
        }
    }
}
