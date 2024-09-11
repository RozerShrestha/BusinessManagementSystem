using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using BusinessManagementSystem.Utility;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementSystem.Data.DbInitializer
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
        public void Initialize()
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
            

            //create role if not created
            var isRoleExist=_db.Roles.Any();
            if(!isRoleExist)
            {
                _db.Roles.Add(new Role { Id = 0, Name = SD.Role_Superadmin });
                _db.Roles.Add(new Role { Id = 10, Name = SD.Role_TattooAdmin });
                _db.Roles.Add(new Role { Id = 20, Name = SD.Role_KaffeAdmin });
                _db.Roles.Add(new Role { Id = 30, Name = SD.Role_ApartmentAdmin });

                _db.Roles.Add(new Role { Id = 11, Name = SD.Role_TattooEmployee });
                _db.Roles.Add(new Role { Id = 21, Name = SD.Role_KaffeEmployee });
                _db.Roles.Add(new Role { Id = 31, Name = SD.Role_ApartmentEmployee });
                _db.SaveChanges();
            }
           
            //create user if not created
            var isUserCreated = _db.Users.Any();
            if (!isUserCreated)
            {
                UserRole ur = new UserRole();
                User u = new User
                {
                    Guid = Helper.Helpers.GenerateGUID(),
                    UserName = "rozer.shrestha",
                    Email = "rozer.shrestha611@gmail.com",
                    HashPassword = "r3HosmeZh/NLjq53Zt64n8NO/LLIVFyg3Pn3ped+xb4=", //P@ssworD
                    Salt = "t03jRt/EuumF6epoZbgY+A==",
                    FullName = "Rozer Shrestha",
                    Address = "Bhimsensthan",
                    DateOfBirth = DateOnly.Parse("1991/03/01"),
                    PhoneNumber = "9818136462",
                    Gender = "Male",
                    Occupation = "Technical Manager",
                    CreatedBy = "System Generated",
                    UpdatedBy = "System Generated",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                ur.User = u;
                ur.RoleId = 1;
                _db.Add(ur);
                _db.SaveChanges();
            }

            //create BasicConfiguration
            var isBasicConfiguration=_db.BasicConfigurations.Any();
            if(!isBasicConfiguration)
            {
                BasicConfiguration basicConfiguration = new BasicConfiguration
                {
                    EmailAlias= "Cotiviti_Noreply",
                    Email = "NoReply@Cotiviti.com",
                    Password = "Not Required",
                    HostName= "unauth-ndc.smtp.cotiviti.com",
                    Port=25,
                    ApplicationTitle = "Insurance Claim",
                    InsuranceCompanyName = "Himalayan Everest Insurance Co. Ltd",
                    GroupPolicyNumber = "DRB/HIP/11/22/23100043",
                    EmployerEmailAddress = "HR_Nepal@cotiviti.com",
                    EmployerAddress = "HATTISAR KTM 01-44356625",
                    ShowProductWalkThrough= true,
                    EmailTemplateCreate = "<p>Dear, <b>{{fullName}}</b></p>\r\n<p>You just submitted your Claim. Please contact admin staff to submit the original bill and signature in the claim form so that your claim will be ready to proceed.</p>\r\n<p><b>Patient Name:</b> {{patientName}}</p>\r\n<div>Thank you.</div>\r\n<div>Regards,</div>\r\n<div>Cotiviti Nepal</div>",
                    EmailTemplateUpdate = "<p>Dear <b>{{fullName}}</b></p>\r\n<p>Your Claim Request has been updated. Please contact Admin or HR staff for any kind of inconvenience.</p>\r\n<div><b>Claim Id: </b>{{id}}</div>\r\n<div><b>Patient Name: </b>{{patientName}}</div>\r\n<div><b>Status: </b>{{status}}</div>\r\n<div><b>Remark: </b>{{remark}}</div></br>\r\n<div>Thank you.</div>\r\n<div>Regards,</div>\r\n<div>Cotiviti Nepal</div>",
                    EmailTemplateInsurancePlanChanged= "<p>Dear, <b>Concern,</b></p>\r\n<p>{{fullName}}({{inumber}}) has changed the insurance plan from <b>{{oldplan}}</b> to <b>{{newplan}}</b>. The updated family details need to be verified and approved by someone from HR.</p>\r\n<p><a href=\"https://localhost:44360/Users/edit?id={{id}}\">Click Here</a></p>\r\n<div>Thank you.</div>\r\n<div>Regards,</div>\r\n<div>Cotiviti Nepal</div>",
                    EmailTemplateFamilyUpdated= "<p>Dear, <b>Concern,</b></p>\r\n<p>{{fullName}}({{inumber}}) has updated the family information. The updated family details need to be verified and approved by someone from HR.</p>\r\n<p><a href=\"https://localhost:44360/Users/edit?id={{id}}\">Click Here</a></p>\r\n<div>Thank you.</div>\r\n<div>Regards,</div>\r\n<div>Cotiviti Nepal</div>",
                    HrApproveTemplate= "<p>Dear, <b>{{fullName}}</b></p>\r\n<p>Family member you added has been approved by HR.</p>\r\n<div>Thank you.</div>\r\n<div>Regards,</div>\r\n<div>Cotiviti Nepal</div>"

                };
                _db.Add(basicConfiguration);
                _db.SaveChanges();
            }

            //create Menu and menurole
            var isMenuCreated = _db.Menus.Any();
            if (!isMenuCreated)
            {
                List<Menu> menus = new List<Menu> {
                new Menu {  Parent = 0,     Name = "Configurations",          Url = "#",                                        Sort = 1, Status = true,        Icon = "fas fa-cogs" },
                new Menu {  Parent = 1,     Name = "Basic Configuration",     Url = "/BasicConfiguration",                      Sort = 1, Status = true,        Icon = "bi bi-gear" },
                new Menu {  Parent = 1,     Name = "Menu",                    Url = "/Menus",                                   Sort = 2, Status = true,        Icon = "bi bi-menu-app" },
                new Menu {  Parent = 1,     Name = "Role",                    Url = "/Role",                                    Sort = 3, Status = false,       Icon = "bi bi-menu-app" },
                new Menu {  Parent = 1,     Name = "MenuRole",                Url = "/MenuRole",                                Sort = 4, Status = false,       Icon = "bi bi-menu-app" },
                new Menu {  Parent = 0,     Name = "EmployeeDetail",          Url = "#",                                        Sort = 2, Status = true,       Icon = "fas fa-users" },
                new Menu {  Parent = 6,     Name = "All Profile",             Url= "/Users",                                    Sort = 1, Status = true,       Icon = "bi bi-menu-app" },
                new Menu {  Parent = 6,     Name = "My Profile",              Url = "/Users/MyProfile",                         Sort = 2, Status = true,       Icon = "bi bi-menu-app" },
                new Menu {  Parent = 6,     Name = "My Family",               Url = "/family",                                  Sort = 4, Status = true,       Icon = "bi bi-menu-app" },
                new Menu {  Parent = 6,     Name = "My Bank Detail",          Url = "/bankdetail",                              Sort = 6, Status = true,       Icon = "bi bi-menu-app" },
                new Menu {  Parent = 0,     Name = "ClaimDetail",             Url = "#",                                        Sort = 3, Status = true,       Icon = "fas fa-file-alt" },
                new Menu {  Parent = 11,    Name = "New Claim",               Url = "/MedicalInsuranceForms/create",            Sort = 3, Status = true,        Icon = "bi bi-menu-app" },
                new Menu {  Parent = 11,    Name = "My Claims",               Url= "/MedicalInsuranceForms/",                   Sort = 2, Status = true,        Icon = "bi bi-menu-app" },
                new Menu {  Parent = 11,    Name = "All Claims",              Url = "/MedicalInsuranceForms/AllClaims",         Sort = 1, Status = true,        Icon = "bi bi-menu-app" },
                new Menu {  Parent = 0,     Name = "Uploads",                 Url = "#",                                        Sort = 4, Status = true,       Icon = "fas fa-upload" },
                new Menu {  Parent = 15,    Name = "Insurance Excel Upload",  Url= "/InsuranceExcelUpload",                     Sort = 1, Status = true,        Icon = "bi" },
                new Menu {  Parent = 15,    Name = "User Detail Upload",      Url= "/InsuranceExcelUpload/UserDetailUpload",    Sort = 2, Status = true,        Icon = "bi" },
                new Menu {  Parent = 0,     Name = "Reports",                 Url = "#",                                        Sort = 4, Status = true,      Icon = "fas fa-file-invoice" },
                new Menu {  Parent = 18,     Name = "Insurance Sent Report",  Url = "/Reports/InsuranceSentReport",             Sort = 4, Status = true,      Icon = "fas fa-file-invoice" },
                new Menu {  Parent = 0,     Name = "Log",                     Url= "#",                                         Sort = 5, Status = true,       Icon = "fas fa-list-ul" },
                new Menu {  Parent = 20,    Name = "Log View",                Url= "/logview",                                  Sort = 1, Status = true,        Icon = "bi" },
                new Menu {  Parent = 1,    Name = "Insurance Plan",           Url= "/insurancePlans",                           Sort = 3, Status = true,        Icon = "bi" },
                new Menu {  Parent = 6,    Name = "All Family",               Url= "/family/AllFamily",                         Sort = 3, Status = true,        Icon = "bi" },
                new Menu {  Parent = 6,    Name = "All Bank Details",         Url= "/bankdetail/AllBankDetails\t",              Sort = 5, Status = true,        Icon = "bi" }
            };

                var roles = _unitOfWork.Role.GetAllAsync();
                //foreach(var menu in menus)
                //{
                //    if (menu.VisibleToAll==true)
                //    {
                //        foreach (var role in roles.Result.Datas)
                //        {
                //            MenuRole mr = new MenuRole();
                //            mr.Role = role;
                //            mr.Menu = menu;
                //            _db.MenuRoles.Add(mr);
                //        }
                //    }
                //    // only to admin which is role 1
                //    else
                //    {
                //        MenuRole mr = new(){
                //            RoleId = 1,
                //            Menu = menu
                //        };
                //        _db.MenuRoles.Add(mr);

                //        MenuRole mr1 = new(){
                //            RoleId = 3,
                //            Menu = menu
                //        };
                //        _db.MenuRoles.Add(mr1);
                //    }
                //    _db.SaveChanges();
                //}
            }
        }
    }
}
