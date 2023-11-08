using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketingManagementSystemAPI.Interfaces;
using TicketingManagementSystemAPI.Models;

namespace  TicketingManagementSystemAPI.Services
{
    public class AccountHandler:IAccountHandler
    {
        private readonly ApplicationDbContext db;
       
        public async Task<ClaimsPrincipal> SetClaim(Signup signUp)
        {
            IEnumerable<Claim> tenantClaims = new Claim[]
     {
                new Claim(ClaimTypes.Name,signUp.EmployeeId) ,
              
                new Claim("Username",signUp.Firstname),
                new Claim("Userid",signUp.Id.ToString()),
                new Claim("Department",signUp.Departments.DepartmentName),
                new Claim("DepartmentID",signUp.DepartmentsId.ToString()),
                new Claim("IsSuperAdmin",signUp.isSuperAdmin.ToString()),

     };
            ClaimsIdentity identity = new ClaimsIdentity(tenantClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
            return claimsPrincipal;

        }
    }
}
