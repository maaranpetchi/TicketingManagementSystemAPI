using System.Security.Claims;
using System.Threading.Tasks;
using TicketingManagementSystemAPI.Models;

namespace TicketingManagementSystemAPI.Interfaces

{
    public interface IAccountHandler
    {
        Task<ClaimsPrincipal> SetClaim(Signup signUp);
    }
}
