using BusinessManagementSystem.Controllers;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
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
            if(htmlMessage.Contains("completed"))
            {
                subject = "Regarding Appointment Completion";
            }

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
        //for New User Creation
        public string PrepareEmail(UserDto userDto, string message)
        {
            StringBuilder sb = new StringBuilder(message);
            sb.Replace("{{fullname}}", userDto.FullName);
            sb.Replace("{{username}}", userDto.UserName);
            sb.Replace("{{email}}", userDto.Email);
            sb.Replace("{{mobilenumber}}", userDto.PhoneNumber);
            sb.Replace("{{password}}", userDto.Password);
            sb.Replace("{{dateofbirth}}", Helper.Helpers.FormatDate(userDto.DateOfBirth));
            sb.Replace("{{occupation}}", userDto.Occupation);
            return sb.ToString();
        }
        public string PrepareEmailAppointmentArtist(AppointmentDto appointmentDto, string message)
        {
            StringBuilder sb = new StringBuilder(message);
            sb.Replace("{{status}}", appointmentDto.Status);
            sb.Replace("{{artistname}}", appointmentDto.ArtistAssigned);
            sb.Replace("{{clientname}}", appointmentDto.ClientName);
            sb.Replace("{{clientphonenumber}}", appointmentDto.ClientPhoneNumber);
            sb.Replace("{{appointmentdate}}", Helper.Helpers.FormatDate(appointmentDto.AppointmentDate));
            sb.Replace("{{outletname}}", appointmentDto.Outlet);
            sb.Replace("{{tattooDesign}}", appointmentDto.TattooDesign);
            sb.Replace("{{placement}}", appointmentDto.Placement);
            sb.Replace("{{inkcolorpreference}}", appointmentDto.InkColorPreferance);
            sb.Replace("{{totalhours}}", appointmentDto.TotalHours.ToString());
            sb.Replace("{{deposit}}", appointmentDto.Deposit.ToString());
            sb.Replace("{{totalcost}}", appointmentDto.TotalCost.ToString());
            if (appointmentDto.TipAmount > 0)
            {
                sb.Replace("###",$"Tip Amount: {appointmentDto.TipAmount.ToString()}");
            }
            else
            {
                sb.Replace("###", "");
            }
            return sb.ToString();
        }
        public string PrepareEmailAppointmentClient(AppointmentDto appointmentDto, string message)
        {
            StringBuilder sb = new StringBuilder(message);
            sb.Replace("{{status}}", appointmentDto.Status);
            sb.Replace("{{artistname}}", appointmentDto.ArtistAssigned);
            sb.Replace("{{clientname}}", appointmentDto.ClientName);
            sb.Replace("{{clientphonenumber}}", appointmentDto.ClientPhoneNumber);
            sb.Replace("{{appointmentdate}}", Helper.Helpers.FormatDate(appointmentDto.AppointmentDate));
            sb.Replace("{{outletname}}", appointmentDto.Outlet);
            sb.Replace("{{artistphonenumber}}", _unitOfWork.Users.GetById(appointmentDto.UserId).Data.PhoneNumber);
            sb.Replace("{{tattooDesign}}", appointmentDto.TattooDesign);
            sb.Replace("{{placement}}", appointmentDto.Placement);
            sb.Replace("{{inkcolorpreference}}", appointmentDto.InkColorPreferance);
            sb.Replace("{{totalhours}}", appointmentDto.TotalHours.ToString());
            sb.Replace("{{deposit}}", appointmentDto.Deposit.ToString());
            sb.Replace("{{totalcost}}", appointmentDto.TotalCost.ToString());
            if (appointmentDto.TipAmount > 0)
            {
                sb.Replace("###", $"Tip Amount: {appointmentDto.TipAmount.ToString()}");
            }
            else
            {
                sb.Replace("###", "");
            }

            return sb.ToString();
        }
        public string PrepareEmailPaymentSettlement(PaymentTipSettlementDto paymentTipSettlementDto, string message)
        {
            StringBuilder sb = new StringBuilder(message);
            sb.Replace("{{artistname}}", paymentTipSettlementDto.ArtistName);
            sb.Replace("{{startdate}}", Helper.Helpers.FormatDate(paymentTipSettlementDto.StartDate));
            sb.Replace("{{enddate}}", Helper.Helpers.FormatDate(paymentTipSettlementDto.EndDate));
            sb.Replace("{{totalTips}}", paymentTipSettlementDto.TotalTips.ToString());
            sb.Replace("{{totalPayment}}", paymentTipSettlementDto.TotalPayments.ToString());
            sb.Replace("{{totalAdvancePayment}}", paymentTipSettlementDto.TotalAdvancePayments.ToString());
            sb.Replace("{{grandTotal}}", paymentTipSettlementDto.GrandTotal.ToString());
            return sb.ToString();
        }
        public string PrepareEmailAdvanceSettlement(AdvancePayment advancePayment, string message, string type)
        {
            advancePayment.User = _unitOfWork.Users.GetById(advancePayment.UserId).Data;
            StringBuilder sb = new StringBuilder(message);
            if (type == "msgsuperadmin")
            {
                sb.Replace("{{artistname}}", advancePayment.User.FullName);
                sb.Replace("{{advanceamount}}", advancePayment.Amount.ToString());
            }
            else if(type=="msgartist")
            {
                sb.Replace("{{artistname}}", advancePayment.User.FullName);
                sb.Replace("{{advanceamount}}", advancePayment.Amount.ToString());
                sb.Replace("{{paymentmethod}}", advancePayment.PaymentMethod);
                sb.Replace("{{status}}", advancePayment.Status==true?$"approved and transferred to you via {advancePayment.PaymentMethod}":"rejected");
            }
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
