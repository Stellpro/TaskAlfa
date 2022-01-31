
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TaskAlfa.Data.Models;

namespace TaskAlfa.Data.SharedServices
{
    public class SecurityService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public SecurityService(IHttpContextAccessor _httpContextAccessor)
        {
            httpContextAccessor = _httpContextAccessor;

        }

       

      
    }
}
