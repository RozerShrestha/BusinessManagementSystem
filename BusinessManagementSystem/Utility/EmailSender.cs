using TattooAppointmentSystem.Controllers;
using TattooAppointmentSystem.Dto;
using TattooAppointmentSystem.Models;
using TattooAppointmentSystem.Services;
using TattooAppointmentSystem.Utility;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using System.Text;
using System.Text.RegularExpressions;

namespace TattooAppointmentSystem.Utility
{
    public class EmailSender : IEmailSender
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ILogger<EmailSender> _logger;
        private string _emailAlias = "";
        private string _emailAddress = "";
        private string _password = "";
        private string _hostName = "";
        private int _port = 0;
        public EmailSender(IUnitOfWork unitOfWork, ILogger<EmailSender> logger)
        {
            _unitOfWork= unitOfWork;
            _logger= logger;
             GetEmailDetail();

        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            if (htmlMessage.Contains("completed"))
            {
                subject = "Regarding Appointment Completion";
            }

            var alias = _emailAlias;
            var fromAddress = _emailAddress;
            var pwd = _password;
            var host = _hostName;
            var smtpPort = _port;

            _ = Task.Run(() => //I don't care about the result or don't want to store anywhere, just execute it. i.e. _ refer to that
            {
                try
                {
                    var emailToSend = new MimeMessage();
                    emailToSend.From.Add(new MailboxAddress(alias, fromAddress));
                    emailToSend.To.Add(MailboxAddress.Parse(email));
                    emailToSend.Subject = subject;
                    emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };


                    //send email
                    using var emailClient = new SmtpClient();
                    emailClient.Connect(host, smtpPort, MailKit.Security.SecureSocketOptions.Auto);
                    emailClient.Authenticate(fromAddress, pwd);
                    emailClient.Send(emailToSend);
                    emailClient.Disconnect(true);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send email to {Email}", email);
                }
            });
            _logger.LogInformation($"## {this.GetType().Name} Email Send to {email} Message: {htmlMessage}");
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

        public string PrepareEmailForOtp(string userName, string message)
        {
            StringBuilder sb = new StringBuilder(message);
            sb.Replace("{fullname}", userName);
            return sb.ToString();
        }

        private void GetEmailDetail()
        {
            var basicInfo = _unitOfWork.BasicConfiguration.GetSingleOrDefault().Data;
            _emailAlias = basicInfo.EmailAlias;
            _emailAddress = basicInfo.Email;
            _password = basicInfo.Password;
            _hostName = basicInfo.HostName;
            _port = basicInfo.Port;
        }

        public string PrepareEmailForConcentForm(AppointmentDto appointmentDto, string message)
        {
            StringBuilder sb=new StringBuilder(message);
            sb.Replace("{{fullname}}", appointmentDto.ClientName.Replace(" ","+"));
            sb.Replace("{{phonenumber}}",appointmentDto.ClientPhoneNumber);
            sb.Replace("{{address}}",appointmentDto.Address.Replace(" ", "+"));
            sb.Replace("{{dob}}", appointmentDto.DateOfBirth==null?"": appointmentDto.DateOfBirth.Value.ToString("yyyy-MM-dd"));
            sb.Replace("{{gender}}",appointmentDto.Gender.Replace(" ", "+"));
            sb.Replace("{{placement}}",appointmentDto.Placement.Replace(" ", "+"));
            return sb.ToString();
        }
    }
}

