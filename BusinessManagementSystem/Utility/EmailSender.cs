using BusinessManagementSystem.Controllers;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Services;
using BusinessManagementSystem.Utility;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessManagementSystem.Utility
{
    public class EmailSender : IEmailSender
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ILogger<EmailSender> _logger;
        private static string emailAlias = "";
        private static string emailAddress = "";
        private static string password = "";
        private static string hostName = "";
        private static int port=0;
        public EmailSender(IUnitOfWork unitOfWork, ILogger<EmailSender> logger)
        {
            _unitOfWork= unitOfWork;
            _logger= logger;
             GetEmailDetail();

        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            new Task(() =>
            {
                var emailToSend = new MimeMessage();
                emailToSend.From.Add(new MailboxAddress(emailAlias, emailAddress));
                emailToSend.To.Add(MailboxAddress.Parse(email));
                emailToSend.Subject = subject;
                emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };
                

                //send email
                using (var emailClient = new SmtpClient())
                {
                    emailClient.Connect(hostName, port, MailKit.Security.SecureSocketOptions.Auto);
                    emailClient.Authenticate(emailAddress, password);
                    emailClient.Send(emailToSend);
                    emailClient.Disconnect(true);
                }
            }).Start();
            _logger.LogInformation($"## {this.GetType().Name} Email Send to {email} Message: {htmlMessage}");
            return Task.CompletedTask;
        }
        public string PrepareEmail(UserDto userDto, string message)
        {
            StringBuilder sb = new StringBuilder(message);
            sb.Replace("{{fullname}}", userDto.FullName);
            sb.Replace("{{username}}", userDto.UserName);
            sb.Replace("{{email}}", userDto.Email);
            sb.Replace("{{mobilenumber}}", userDto.PhoneNumber);
            sb.Replace("{{password}}", userDto.Password);
            sb.Replace("{{dateofbirth}}", userDto.DateOfBirth.ToString());
            sb.Replace("{{occupation}}", userDto.Occupation);
            return sb.ToString();
        }
        public string PrepareEmailAppointmentArtist(AppointmentDto appointmentDto, string message)
        {
            StringBuilder sb = new StringBuilder(message);
            sb.Replace("{{artistname}}", appointmentDto.ArtistAssigned);
            sb.Replace("{{clientname}}", appointmentDto.ClientName);
            sb.Replace("{{clientphonenumber}}", appointmentDto.ClientPhoneNumber);
            sb.Replace("{{appointmentdate}}", appointmentDto.AppointmentDate.ToString());
            sb.Replace("{{outletname}}", appointmentDto.Outlet);
            
            return sb.ToString();
        }
        public string PrepareEmailAppointmentClient(AppointmentDto appointmentDto, string message)
        {
            StringBuilder sb = new StringBuilder(message);
            sb.Replace("{{artistname}}", appointmentDto.ArtistAssigned);
            sb.Replace("{{clientname}}", appointmentDto.ClientName);

            sb.Replace("{{clientphonenumber}}", appointmentDto.ClientPhoneNumber);
            sb.Replace("{{appointmentdate}}", appointmentDto.AppointmentDate.ToString());
            sb.Replace("{{outletname}}", appointmentDto.Outlet);
            sb.Replace("{{artistphonenumber}}", _unitOfWork.Users.GetById(appointmentDto.UserId).Data.PhoneNumber);


            return sb.ToString();
        }

        private void GetEmailDetail()
        {
            var basicInfo = _unitOfWork.BasicConfiguration.GetSingleOrDefault().Data;
            emailAlias = basicInfo.EmailAlias;
            emailAddress = basicInfo.Email;
            password = basicInfo.Password;
            hostName = basicInfo.HostName;
            port = basicInfo.Port;

        }
    }
}
