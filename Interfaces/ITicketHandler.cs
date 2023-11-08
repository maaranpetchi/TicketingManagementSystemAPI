using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TicketingManagementSystemAPI.Interfaces

{
    public interface ITicketHandler
    {
        Task<string> FileOperation(int TicketId, IFormFile formFile, string Operation);
        Task EmailSender(string Username, string pasword,
            string hostName, int port, string RecieverName, int n, string AssignedPersonName
            , IFormFile formFile, string CreatedById, string CreatedByName, string TicketDetails, string BayNumber, string Department);
        Task<byte[]> GetFileAsync(string fileName);
    }
}
