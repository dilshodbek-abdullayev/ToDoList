using ToDoList.Domain.Entities.Enums;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace ToDoList.API.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Enum)]
    public class IdentityFilterAttributes : Attribute, IAuthorizationFilter
    {
        private readonly int _permissionId;

        public IdentityFilterAttributes(Permission permissionId)
        {
            _permissionId = (int)permissionId;
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            var identity = context.HttpContext.User.Identity as ClaimsIdentity;
            var permissionIds = identity.FindFirst("Permissions")?.Value;

            var res = JsonSerializer.Deserialize<List<int>>(permissionIds).Any(x => _permissionId ==x);
            if (!res)
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
