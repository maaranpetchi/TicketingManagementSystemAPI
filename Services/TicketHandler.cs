using Microsoft.AspNetCore.Http;

using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using MailKit.Net.Smtp;
using MailKit.Security;
using TicketingManagementSystemAPI.Interfaces;
using MimeKit;

namespace TicketingManagementSystemAPI.Services
{
    public class TicketHandler:ITicketHandler
    {
        public async Task<string> FileOperation(int TicketId,IFormFile formFile,string Operation)
        {
            string currentDirectory = System.IO.Directory.GetCurrentDirectory();
            currentDirectory = currentDirectory + @"\FilePath\" + $"{TicketId}\\{Operation}\\";
            if (!System.IO.File.Exists(currentDirectory))
            {
                System.IO.Directory.CreateDirectory(currentDirectory);
                var memoryStream = new MemoryStream();
                await formFile.CopyToAsync(memoryStream);
                FileStream file = new FileStream(Path.Combine(currentDirectory, $"{formFile.FileName}"), FileMode.Create, FileAccess.Write,FileShare.ReadWrite);
                await file.WriteAsync(memoryStream.ToArray(), 0, memoryStream.ToArray().Length);
                return currentDirectory;
            }
            else
            {
                var memoryStream = new MemoryStream();
                await formFile.CopyToAsync(memoryStream);
                FileStream file = new FileStream(Path.Combine(currentDirectory, $"{formFile.FileName}"), FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
               await file.WriteAsync(memoryStream.ToArray(), 0, memoryStream.ToArray().Length);
                return currentDirectory;
            }
        }
        public async Task EmailSender(string Username, string pasword, 
            string hostName, int port, string RecieverName, int n, string AssignedPersonName
            ,IFormFile formFile,string CreatedById, string CreatedByName, string TicketDetails,string BayNumber,string Department)
        {
            var email = new MimeMessage();
            string Content = EmailContent(n, AssignedPersonName, CreatedById, CreatedByName, TicketDetails,BayNumber,Department);
            email.From.Add(MailboxAddress.Parse(Username));
            email.To.Add(MailboxAddress.Parse(RecieverName));
            var builder = new BodyBuilder()
            {
            HtmlBody = Content  
            };
         if(formFile != null) {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await formFile.CopyToAsync(memoryStream);
                    builder.Attachments.Add(formFile.FileName, memoryStream.ToArray());
                }
            }
                 
            email.Body = builder.ToMessageBody();
            email.Subject = "About Ticket";
           // email.Body = new TextPart(TextFormat.Plain) { Text = Content };
            
            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(hostName, port, SecureSocketOptions.StartTls);

            smtp.Authenticate(Username, pasword);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
        private string EmailContent(int n, string AssignedPersonName,string CreatedById,string CreatedByName, string TicketDetails, string BayNumber, string Department)
        {
            string Content = "";
            if (n == 1)
            {
                Content = @$"<div>
                           <h3>Hi Team,</h3>
                                <h4> New Ticket has been raised into your Department</h4>
                          <table>
                               
                                    
                                   <tr>
                                        <th><span>Created By </span></th>
                                        <td>:</td>
                                        <td>{CreatedByName} - {CreatedById}</td>
                                    </tr>

                                          <tr>
                                        <th>Ticket Details </th>
                                            <td>:</td>
                                        <td>{TicketDetails}</td>
                                    </tr>
                                       
                                     <tr>
                                        <th> Bay Number </th>
                                          <td>:</td>
                                        <td>{BayNumber} </td>
                                    </tr>
                                     
                                      <tr>
                                        <th>Department </th>
                                           <td>:</td>
                                        <td>{Department}</td>
                                    </tr>
                                 
                                

                          </table>
<h3>Regards,</h3>
<h4>VLead-Ticketing System.</h4></div> "
;
            }
            else if (n == 2)
            {
                Content = @$"Hi {AssignedPersonName},
                                 Ticket has been assigned to you.
                                                                              
                                       
                          <table>
                               
                                    
                                   <tr>
                                        <th><span>Created By</span></th>
                                        <td>:</td>
                                        <td>{CreatedByName} - {CreatedById}</td>
                                    </tr>

                                          <tr>
                                        <th>Ticket Details </th>
                                            <td>:</td>
                                        <td>{TicketDetails}</td>
                                    </tr>
                                       
                                     <tr>
                                        <th> Bay Number </th>
                                          <td>:</td>
                                        <td>{BayNumber} </td>
                                    </tr>
                                     
                                      <tr>
                                        <th>Department </th>
                                           <td>:</td>
                                        <td>{Department}</td>
                                    </tr>
                                 
                                

                          </table>     
<h3>Regards,</h3>
<h4>VLead-Ticketing System.</h4>
";
            }
            else if (n == 3)
            {
                Content = $@"Hi {AssignedPersonName},
                                 Ticket has been resolved to you.

                                 
";
            }
            else if (n == 4)
            {
                Content = @$"Hi {CreatedByName},
                                 Ticket has been closed.
                                                                              
                                       
                          <table>
                               
                                    
                                   <tr>
                                        <th><span>Created By</span></th>
                                        <td>:</td>
                                        <td>{CreatedByName} - {CreatedById}</td>
                                    </tr>

                                          <tr>
                                        <th>Ticket Details </th>
                                            <td>:</td>
                                        <td>{TicketDetails}</td>
                                    </tr>
                                       
                                     <tr>
                                        <th> Bay Number </th>
                                          <td>:</td>
                                        <td>{BayNumber} </td>
                                    </tr>
                                     
                                      <tr>
                                        <th>Department </th>
                                           <td>:</td>
                                        <td>{Department}</td>
                                    </tr>
                                 
                                

                          </table>     
<h3>Regards,</h3>
<h4>VLead-Ticketing System.</h4>
";
            }
            return Content;
        }
        public async Task<byte[]> GetFileAsync(string fileName)
        {
            string path = fileName;

            //Read the File data into Byte Array.
            byte[] bytes = await System.IO.File.ReadAllBytesAsync(path);
           // string file = "Issue" + Guid.NewGuid().GetType();
            //Send the File to Download.
            return bytes;
        }
       
    }
}
