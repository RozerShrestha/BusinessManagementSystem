using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using BusinessManagementSystem.Utility;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System;

namespace BusinessManagementSystem.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDBContext _db;
        private readonly IUnitOfWork _unitOfWork;
        public DbInitializer(ApplicationDBContext db, IUnitOfWork unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
        public async void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {
                throw;
            }

            #region Role
            var isRoleExist = _db.Roles.Any();
            if (!isRoleExist)
            {
                List<Role> roles =
                [
                    new Role { Id = 1, CreatedBy="System", UpdatedBy="System", Name = SD.Role_Superadmin },
                    new Role { Id = 10, CreatedBy = "System", UpdatedBy = "System", Name = SD.Role_TattooAdmin },
                    //new Role { Id = 20, CreatedBy="System",UpdatedBy="System", Name = SD.Role_KaffeAdmin },
                    //new Role { Id = 30, CreatedBy="System",UpdatedBy="System", Name = SD.Role_ApartmentAdmin },
                    new Role { Id = 11, CreatedBy = "System", UpdatedBy = "System", Name = SD.Role_TattooEmployee },
                    //new Role { Id = 21, CreatedBy = "System", UpdatedBy = "System", Name = SD.Role_KaffeEmployee },
                    //new Role { Id = 31, CreatedBy = "System", UpdatedBy = "System", Name = SD.Role_ApartmentEmployee }
                ];
                _db.AddRange(roles);
                _db.SaveChanges();
            }
            #endregion

            #region Referal
            var isReferalExist = _db.Referals.Any();
            if (!isReferalExist)
            {
                Referal referal = new Referal
                {
                    FullName = "No Referal",
                    ReferalAppointDate = DateOnly.FromDateTime(DateTime.Now),
                    Commission = 10,
                    Status = true,
                };
                _db.Add(referal);
                _db.SaveChanges();
                
            }
            #endregion


            #region User
            var isUserCreated = _db.Users.Any();
            if (!isUserCreated)
            {
                UserRole ur = new UserRole();
                User u = new User
                {
                    Guid = Helper.Helpers.GenerateGUID(),
                    UserName = "rozer.shrestha",
                    Email = "rozer.shresthatest@gmail.com",
                    HashPassword = "vNWsg9wG82FOVlKYm4fJNkTSysuUuGoeuCNL/oYbwn4=", //12345678
                    Salt = "x9MC6J+9dMJ06q0OP/T4/w==",
                    FullName = "Rozer Shrestha",
                    Address = "Bhimsensthan",
                    DateOfBirth = DateOnly.Parse("1991/03/01"),
                    PhoneNumber = "9818136462",
                    Gender = "Male",
                    Occupation = "Chief Operating Officer",
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    DefaultTips=false,
                    Status=true,
                    Percentage=0,
                };

                ur.User = u;
                ur.RoleId = 1;
                _db.Add(ur);
                _db.SaveChanges();
            }
            #endregion

            #region BasicConfiguration
            var isBasicConfiguration = _db.BasicConfigurations.Any();
            if (!isBasicConfiguration)
            {
                BasicConfiguration basicConfiguration = new BasicConfiguration
                {
                    EmailAlias = "Freak Street Tattoo",
                    Email = "freakstreettattoo@gmail.com",
                    Password = "vqtmjohmhkekhuhh",
                    HostName = "smtp.gmail.com",
                    Port = 587,
                    ApplicationTitle = "Freak Street Empire",
                    EmployerName = "Freak Street Tattoo",
                    EmployerEmailAddress = "freakstreettattoo@gmail.com",
                    EmployerAddress = "Freak Street",
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    TattooPrice = 6000,
                    PiercingPrice = "{'Piercings':{'Eyebrow':1000,'Lips':1000,'Nose':1500,'Smiley':1500,'Belly':1500,'Tongue':1500,'Nipple':2000,'Dermal':4000}}",
                    EarPiercingPrice= "{'EarPiercings':{'Forward Helix':1000,'Helix':1000,'Industrial':1500,'Inner Conch':1500,'Rook':1500,'Daith':1500,'Tragus':1500,'Snug':1500,'Anti-Tragus':1500,'Orbital':1000,'Outer Conch':1000,'Standard Lobe':750,'Inner Lobe':750,'Transverse Lobe':1500}}\r\n",
                    DreadLockPrice = 1500,
                    GoogleFormLink= "https://docs.google.com/forms/d/e/1FAIpQLSdZnnksa5BDXC4KnPWrd5ahQsTPyPDLba057DhaclYJsrSNVw/viewform?usp=pp_url&entry.940419200={{fullname}}&entry.675187046={{phonenumber}}&entry.171791695={{address}}&entry.1171733062={{dob}}&entry.1789549336={{gender}}&entry.434569851={{placement}}",
                    AppointmentUpdateTemplateArtist = "<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Appointment Reschedule Notification</title>\r\n</head>\r\n<body>\r\n    <div>\r\n        <h1>Freak Street Tattoo</h1>\r\n        <p>Dear {{artistname}},</p>\r\n        <p>We wanted to inform you that an appointment previously assigned to you has been rescheduled. Here are the updated details:</p>\r\n        <p><strong>Client Name:</strong> {{clientname}}</p>\r\n        <p><strong>New Date:</strong> {{appointmentdate}}</p>\r\n        <p><strong>Location:</strong> Freak Street Tattoo, {{outletname}}</p>\r\n        <p>Please review the updated schedule and prepare accordingly. If there are any specific design ideas, references, or special requests from the client, we will share them with you in advance.</p>\r\n        <p>If you have any questions or need further assistance, feel free to reach out to us.</p>\r\n        <p>Thank you for your flexibility and continued dedication. We’re confident you’ll provide an outstanding experience for the client!</p>\r\n        <p>Warm regards,<br>Freak Street Tattoo</p>\r\n    </div>\r\n</body>\r\n</html>",
                    AppointmentUpdateTemplateClient = "<html lang=\"en\">\r\n<body>\r\n        <h1>Freak Street Tattoo</h1>\r\n        <p>Dear {{clientname}},</p>\r\n        <p>We wanted to inform you that your appointment has been rescheduled. Here are the updated details of your appointment:</p>\r\n        <p><strong>New Date:</strong> {{appointmentdate}}</p>\r\n        <p><strong>Artist:</strong> {{artistname}}</p>\r\n        <p><strong>Location:</strong> Freak Street Tattoo, {{outletname}}</p>\r\n        <p>We apologize for any inconvenience this change may cause and greatly appreciate your understanding. If you have any specific design ideas, references, or special requests, feel free to email us or bring them along to your appointment.</p>\r\n        <p>To ensure a smooth experience, kindly arrive at least 10 minutes before your scheduled time. Also, please let us know at least 24 hours in advance if you need to reschedule or cancel your appointment again.</p>\r\n        <p>We look forward to seeing you soon!</p>\r\n        <p>Warm regards,<br>Freak Street Tattoo</p>\r\n        <p>&copy; {{year}} Freak Street Tattoo. All rights reserved.</p>\r\n</body>\r\n</html>",
                    NewAppointmentTemplateArtist = "<html>\r\n<body>\r\n        <p>Dear {{artistname}},</p>\r\n        <p>We’re excited to inform you that a new appointment has been assigned to you. Here are the details of the appointment:</p>\r\n        <p><strong>Client Name:</strong> {{clientname}}</p>\r\n        <p><strong>Client Phone Number:</strong> {{clientphonenumber}}</p>\r\n        <p><strong>Date:</strong> {{appointmentdate}}</p>\r\n        <p><strong>Location:</strong> Freak Street Tattoo, {{outletname}}</p>\r\n        <p>Please ensure you review the client's preferences and prepare accordingly. If there are any specific design ideas, references, or special requests from the client, we will share them with you in \t\tadvance.</p>\r\n        <p>If you have any questions or need further assistance, feel free to reach out to us.</p>\r\n        <p>Thank you for your dedication and talent. We’re confident you’ll deliver an exceptional experience!</p>\r\n        <p>Warm regards,<br>Freak Street Tattoo</p>\r\n</body>\r\n</html>",
                    NewAppointmentTemplateClient = "<html lang=\"en\">\r\n<body>\r\n        <h1>Freak Street Tattoo</h1>\r\n        <p>Dear {{clientname}},</p>\r\n        <p>Thank you for choosing Freak Street Tattoo! We’re excited to be a part of your tattoo journey. Here are the details of your appointment:</p>\r\n        <p>Date: {{appointmentdate}}</p>\r\n        <p>Artist: {{artistname}}</p>\r\n        <p>Location: Freak Street Tattoo, {{outletname}}</p>\r\n        <p>If you have any specific design ideas, references, or special requests, feel free to email us or bring them along to your appointment.</p>\r\n        <p>To ensure a smooth experience, kindly arrive at least 10 minutes before your scheduled time. Also, please let us know at least 24 hours in advance if you need to reschedule or cancel your appointment.</p>\r\n        <p>We look forward to seeing you soon!</p>\r\n        <p>Warm regards,<br>Freak Street Tattoo</p>\r\n        <p>&copy; {{year}} Freak Street Tattoo. All rights reserved.</p>\r\n</body>\r\n</html>",
                    NewUserEmailTemplate = "<html>\r\n<body>\r\n    <p>Hi <b>{{fullname}}</b>,</p>\r\n    <p>Welcome to <b>Freak Street Tattoo Family</b>.</p>\r\n    <p>Your user account has been created, and below are the details:</p>\r\n    <p>\r\n        User Name: <b>{{username}}</b><br>\r\n        Email: <b>{{email}}</b><br>\r\n        Mobile Number: <b>{{mobilenumber}}</b><br>\r\n        Password: <b>{{password}}</b><br>\r\n        Date of Birth: <b>{{dateofbirth}}</b><br>\r\n        Occupation: <b>{{occupation}}</b>\r\n    </p>\r\n    <p><a href=\"http://202.51.74.51:1111/login\" target=\"_blank\">Click here to log in</a></p>\r\n    <p>Thank you.</p>\r\n    <p>Regards,<br>\r\n    <b>Freak Street Tattoo</b></p>\r\n</body>\r\n</html>",
                    PaymentSettlementTemplateArtist = "<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Payment Settlement Notification</title>\r\n</head>\r\n<body>\r\n    <div>\r\n        <h1>Freak Street Tattoo</h1>\r\n        <p>Dear {{artistname}},</p>\r\n        <p>We hope this message finds you well. We’re writing to inform you about the settlement of your payment for the work completed during the following dates:</p>\r\n        <p><strong>Start Date:</strong> {{startdate}}</p>\r\n        <p><strong>End Date:</strong> {{enddate}}</p>\r\n\t<p><strong>Payment From Tips:</strong> {{totalTips}}</p>\r\n\t<p><strong>Payment From Tattoo:</strong> {{totalPayment}}</p>\r\n        <p><strong>Amount Settled:</strong> {{grandTotal}}</p>\r\n        <p>If you have any questions or require further details regarding the payment, feel free to reach out to us. We’re happy to assist!</p>\r\n        <p>Thank you for your excellent work and dedication. We look forward to continuing our collaboration!</p>\r\n        <p>Warm regards,<br>Freak Street Tattoo</p>\r\n    </div>\r\n</body>\r\n</html>\r\n",
                    AppointmentCompletedArtist = "<html lang=\"en\">\r\n<body>\r\n    <p>Dear <b>{{artistname}}</b>,</p>\r\n    <p>We are pleased to inform you that the appointment with <b>{{clientname}}</b> has been successfully completed. Below are the details of the completed appointment:</p>\r\n    <p>Client Name: {{clientname}}</p>\r\n    <p>Client Phone Number: {{clientphonenumber}}</p>\r\n    <p>Appointment Date: {{appointmentdate}}</p>\r\n    <p>Tattoo Design: {{tattooDesign}}</p>\r\n    <p>Placement: {{placement}}</p>\r\n    <p>Ink Color: {{inkcolorpreference}}</p>\r\n    <p>Total Hour: {{totalhours}}</p>\r\n     <p>Deposit: {{deposit}}</p>\r\n     <p>Total Cost: {{totalcost}}</p>\r\n     ###\r\n    <p>Location: <span style=\"color:red; font-weight:bold \">Freak Street Tattoo, Branch: {{outletname}}</span></p>\r\n    <p>We hope the client is satisfied with the results. If you have any feedback or need further assistance, please don’t hesitate to let us know.</p>\r\n    <p>We would also appreciate it if you could provide the client with any aftercare instructions, and let us know if you have any touch-up recommendations.</p>\r\n    <p>Thank you for your excellent work and dedication. We look forward to your continued success with future appointments!</p>\r\n    <p>Warm regards,<br>Freak Street Tattoo</p>\r\n</body>\r\n</html>",
                    AppointmentCompletedClient= "<html lang=\"en\">\r\n<body>\r\n        <p>Dear <b>{{clientname}}</b>,</p>\r\n        <p>We are pleased to inform you that your tattoo appointment has been successfully completed. Below are the details of your appointment:</p>\r\n        <p><strong>Status: </strong><span style=\"color:blue; font-weight:bold\"> Completed</span></p>\r\n        <p><strong>Appointment Date:</strong> {{appointmentdate}}</p>\r\n        <p><strong>Artist:</strong> {{artistname}}</p>\r\n        <p>Artist Phone Number: {{artistphonenumber}}</p>\r\n        <p>Tattoo Design: {{tattooDesign}}</p>\r\n        <p>Placement: {{placement}}</p>\r\n        <p>Ink Color: {{inkcolorpreference}}</p>\r\n        <p>Total Hour: {{totalhours}}</p>\r\n        <p>Deposit: {{deposit}}</p>\r\n        <p>Total Cost: {{totalcost}}</p>\r\n         ###\r\n        <p><strong>Location:</strong> Freak Street Tattoo, {{outletname}}</p>\r\n        <p>We hope you are happy with your new tattoo! If you have any questions or need any aftercare advice, please don’t hesitate to reach out to us.</p>\r\n        <p>Please be sure to follow the aftercare instructions provided to ensure your tattoo heals properly. If you need touch-ups or have any concerns, feel free to contact us.</p>\r\n        <p>We look forward to seeing you again for your next tattoo or any future appointments!</p>\r\n        <p>Thank you for choosing Freak Street Tattoo.</p>\r\n        <p>Warm regards,<br>Freak Street Tattoo</p>\r\n</body>\r\n</html>",
                    AdvancePaymentArtistTemplate= "<html lang=\"en\">\r\n<body>\r\n    <p>Dear <b>{{artistname}}</b>,</p>\r\n    <p>You have requested for an advance payment of Rs {{advanceamount}}, which has been {{status}}. </p>\r\n    <p>If you have any questions or require further details regarding the payment, feel free to reach out to us. We’re happy to assist!</p>\r\n    <p>Thank you for your excellent work and dedication. We look forward to continuing our collaboration!</p>\r\n    <p>Warm regards,<br>Freak Street Tattoo</p>\r\n</body>\r\n</html>",
                    AdvancePaymentSuperadminTemplate= "<html lang=\"en\">\r\n<body>\r\n    <p>Dear <b>concern</b>,</p>\r\n    <p>{{artistname}} has requested for an advance payment of Rs {{advanceamount}}.</p>\r\n    <p>Warm regards,<br>Freak Street Tattoo</p>\r\n</body>\r\n</html>"



                };
                _db.Add(basicConfiguration);
                _db.SaveChanges();
            }
            #endregion

            #region Menu and MenuRole
            var isMenuCreated = _db.Menus.Any();
            if (!isMenuCreated)
            {
                List<Menu> menus = new List<Menu>
                {
                    new Menu { Parent = 0, Name = "Configurations", Url = "#", Sort = 1, Status = true, Icon = "fas fa-cogs", CreatedBy = "System", UpdatedBy = "System" },
                    new Menu { Parent = 1, Name = "Basic Configuration", Url = "/BasicConfiguration", Sort = 1, Status = true, Icon = "bi bi-gear", CreatedBy = "System", UpdatedBy = "System" },
                    new Menu { Parent = 1, Name = "Menu", Url = "/Menu", Sort = 2, Status = true, Icon = "bi bi-menu-app", CreatedBy = "System", UpdatedBy = "System" },
                    new Menu { Parent = 1, Name = "Role", Url = "/Role/Index", Sort = 3, Status = true, Icon = "fa", CreatedBy = "System", UpdatedBy = "rozer.shrestha" },
                    new Menu { Parent = 0, Name = "Users", Url = "#", Sort = 2, Status = true, Icon = "fas fa-user-friends", CreatedBy = "System", UpdatedBy = "System" },
                    new Menu { Parent = 5, Name = "Create User", Url = "/Users/Create", Sort = 2, Status = true, Icon = "fa", CreatedBy = "System", UpdatedBy = "System" },
                    new Menu { Parent = 5, Name = "All Users", Url = "/Users/Index", Sort = 1, Status = true, Icon = "fa", CreatedBy = "System", UpdatedBy = "System" },
                    new Menu { Parent = 5, Name = "My Profile", Url = "/Users/MyProfile", Sort = 3, Status = true, Icon = "fa", CreatedBy = "System", UpdatedBy = "System" },
                    new Menu { Parent = 0, Name = "Payment", Url = "#", Sort = 4, Status = true, Icon = "fas fa-rupee-sign", CreatedBy = "System", UpdatedBy = "System" },
                    new Menu { Parent = 9, Name = "All Payments", Url = "/Payment/AllPayments", Sort = 1, Status = true, Icon = "fa", CreatedBy = "System", UpdatedBy = "System" },
                    new Menu { Parent = 9, Name = "My Payments", Url = "/Payment/MyPayments", Sort = 2, Status = true, Icon = "fa", CreatedBy = "System", UpdatedBy = "System" },
                    new Menu { Parent = 9, Name = "All Tips", Url = "/Tip/Index", Sort = 3, Status = true, Icon = "fa", CreatedBy = "System", UpdatedBy = "System" },
                    new Menu { Parent = 9, Name = "My Tips", Url = "/Tip/MyTips", Sort = 4, Status = true, Icon = "fa", CreatedBy = "System", UpdatedBy = "System" },
                    new Menu { Parent = 9, Name = "Payment Settlement", Url = "/Payment/PaymentSettlement", Sort = 5, Status = true, Icon = "fa", CreatedBy = "System", UpdatedBy = "System" },
                    new Menu { Parent = 0, Name = "Appointment", Url = "#", Sort = 3, Status = true, Icon = "fas fa-calendar-check", CreatedBy = "System", UpdatedBy = "System" },
                    new Menu { Parent = 15, Name = "All Appointments", Url = "/Appointment", Sort = 1, Status = true, Icon = "fa", CreatedBy = "System", UpdatedBy = "System" },
                    new Menu { Parent = 15, Name = "Create Appointment", Url = "/Appointment/Create", Sort = 2, Status = true, Icon = "fa", CreatedBy = "System", UpdatedBy = "rozer.shrestha" },
                    new Menu { Parent = 15, Name = "My Appointments", Url = "/Appointment/MyAppointments", Sort = 3, Status = true, Icon = "fa", CreatedBy = "System", UpdatedBy = "System" },
                    new Menu { Parent = 9, Name = "Payment History", Url = "/Payment/PaymentHistory", Sort = 7, Status = true, Icon = "fa", CreatedBy = "rozer.shrestha", UpdatedBy = "rozer.shrestha" },
                    new Menu { Parent = 0, Name = "Advance Payment", Url = "#", Sort = 5, Status = true, Icon = "fas fa-money-check", CreatedBy = "rozer.shrestha", UpdatedBy = "rozer.shrestha" },
                    new Menu { Parent = 20, Name = "All Advance Payment", Url = "/Advancepayment/AllAdvancePayment", Sort = 1, Status = true, Icon = "fa", CreatedBy = "rozer.shrestha", UpdatedBy = "rozer.shrestha" },
                    new Menu { Parent = 20, Name = "My Advance Payment", Url = "/AdvancePayment/MyAdvancePayment", Sort = 2, Status = true, Icon = "fa", CreatedBy = "rozer.shrestha", UpdatedBy = "rozer.shrestha" },
                    new Menu { Parent = 20, Name = "Create Advance Payment", Url = "/AdvancePayment/Create", Sort = 3, Status = true, Icon = "fa", CreatedBy = "rozer.shrestha", UpdatedBy = "rozer.shrestha" }
                };

                var roles = _db.Roles.ToList();

                _db.Database.BeginTransaction();
                foreach (var role in roles)
                {
                    foreach (var menu in menus)
                    {
                        MenuRole mr = new()
                        {
                            Role = role,
                            Menu = menu
                        };
                        _db.MenuRoles.Add(mr);
                    }
                }
                _db.SaveChanges();
                _db.Database.CommitTransaction();
            }
            #endregion
        }
    }
}
